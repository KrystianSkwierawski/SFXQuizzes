using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Quizzes.Commands.DeleteQuiz;

public class DeleteQuizCommand : IRequest
{
    public string Id { get; set; }

    public class DeleteQuizCommandHandler : IRequestHandler<DeleteQuizCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly ISFXFileBuilder _SFXFileBulider;
        private readonly ICurrentUserService _currentUserService;
        private readonly IIdentityService _identityService;

        public DeleteQuizCommandHandler(IApplicationDbContext context, ISFXFileBuilder sFXFileBulider, ICurrentUserService currentUserService, IIdentityService identityService)
        {
            _context = context;
            _SFXFileBulider = sFXFileBulider;
            _currentUserService = currentUserService;
            _identityService = identityService;
        }

        public async Task<Unit> Handle(DeleteQuizCommand request, CancellationToken cancellationToken)
        {
            Quiz entity = await _context.Quizzes.FindAsync(request.Id);

            if (entity is null)
                throw new NotFoundException(nameof(Quiz), request.Id);


            bool isInRoleAdmin = await _identityService.IsInRoleAsync(_currentUserService.UserId, "Administrator");

            if (!(entity.CreatedBy == _currentUserService.UserId) && !isInRoleAdmin)
                throw new ForbiddenAccessException();

            _context.Quizzes.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            await _SFXFileBulider.RemoveSFXs(entity.Id);

            return Unit.Value;
        }
    }
}


