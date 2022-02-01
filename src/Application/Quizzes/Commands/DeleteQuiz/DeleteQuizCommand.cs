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

        public DeleteQuizCommandHandler(IApplicationDbContext context, ISFXFileBuilder sFXFileBulider)
        {
            _context = context;
            _SFXFileBulider = sFXFileBulider;
        }

        public async Task<Unit> Handle(DeleteQuizCommand request, CancellationToken cancellationToken)
        {
            //TODO: delete if quiz owner or is in role admin

            Quiz entity = await _context.Quizzes.FindAsync(request.Id);

            if (entity is null)
                throw new NotFoundException(nameof(Quiz), request.Id);

            _context.Quizzes.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            await _SFXFileBulider.RemoveSFXs(entity.Id);

            return Unit.Value;
        }
    }
}


