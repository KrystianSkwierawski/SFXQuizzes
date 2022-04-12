using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.ValueObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Quizzes.Queries.GetQuiz;


public class GetQuizQuery : IRequest<QuizDto>
{
    public string Id { get; set; }
    public class GetQuizQueryHandler : IRequestHandler<GetQuizQuery, QuizDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public GetQuizQueryHandler(IApplicationDbContext context, IMapper mapper, ICurrentUserService currentUserService)
        {
            _context = context;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<QuizDto> Handle(GetQuizQuery request, CancellationToken cancellationToken)
        {
            Quiz entity = _context.Quizzes
                .AsNoTracking()
                .FirstOrDefault(x => x.Id == request.Id);

            if (entity is null)
                throw new NotFoundException(nameof(Quiz), request.Id);


            QuizDto quizDto = _mapper.Map<QuizDto>(entity);

            Rate userRate = entity.Rates.FirstOrDefault(rate => rate.RatedBy == _currentUserService.UserId);
            if (userRate is not null)
                quizDto.UserRate = userRate.Value;

            return quizDto;
        }
    }
}


