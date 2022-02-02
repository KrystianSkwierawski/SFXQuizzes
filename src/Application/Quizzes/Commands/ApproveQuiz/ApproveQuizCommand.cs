using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Quizzes.Commands.ApproveQuiz;


public class ApproveQuizCommand : IRequest
{
    public string Id { get; set; }

    public class ApproveQuizCommandHandler : IRequestHandler<ApproveQuizCommand>
    {
        private readonly IApplicationDbContext _context;

        public ApproveQuizCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(ApproveQuizCommand request, CancellationToken cancellationToken)
        {
            Quiz entity = await _context.Quizzes.FindAsync(request.Id);

            if (entity is null)
                throw new NotFoundException(nameof(Quiz), request.Id);

            entity.Approved = true;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}


