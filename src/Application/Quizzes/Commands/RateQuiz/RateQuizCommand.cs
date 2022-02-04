using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.ValueObjects;
using MediatR;

namespace Application.Quizzes.Commands.RateQuiz;

public class RateQuizCommand : IRequest
{
    public string Id { get; set; }
    public double RateValue { get; set; }

    public class RateQuizCommandHandler : IRequestHandler<RateQuizCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public RateQuizCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<Unit> Handle(RateQuizCommand request, CancellationToken cancellationToken)
        {
            Quiz entity = _context.Quizzes
             .FirstOrDefault(x => x.Id == request.Id);

            if (entity is null)
                throw new NotFoundException(nameof(Quiz), request.Id);

            Rate userRate = entity.Rates.FirstOrDefault(rate => rate.RatedBy == _currentUserService.UserId);

            if (userRate is null)
                await AddNewUserRate(entity, request.RateValue);

            if (userRate is not null)
                await ReplacePreviousUserRate(userRate, request.RateValue);


            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

        private async Task AddNewUserRate(Quiz entity, double rateValue)
        {
            entity.Rates.Add(new Rate
            {
                Value = rateValue,
                RatedBy = _currentUserService.UserId
            });
        }

        private async Task ReplacePreviousUserRate(Rate userRate, double rateValue)
        {
            userRate.Value = rateValue;
        }
    }
}
