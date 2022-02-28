using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Quizzes.Commands.UpdateQuizzesAuthor;

public class UpdateQuizzesAuthorCommand : IRequest
{
    public string UserName { get; set; }

    public class UpdateQuizzesAuthorCommandHandler : IRequestHandler<UpdateQuizzesAuthorCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public UpdateQuizzesAuthorCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<Unit> Handle(UpdateQuizzesAuthorCommand request, CancellationToken cancellationToken)
        {
            IQueryable<Quiz> quizzes = _context.Quizzes.Where(quiz => quiz.CreatedBy == _currentUserService.UserId);

            if (quizzes is null || quizzes.Count() == 0)
                return Unit.Value;         

            foreach (var quiz in quizzes)
            {
                quiz.Author = request.UserName;
            }

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}



