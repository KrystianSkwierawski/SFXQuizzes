using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Quizzes.Queries.GetQuizzes;

public class GetQuizzesQuery : IRequest<IList<QuizDto>>
{
    public class GetQuizzesQueryHandler : IRequestHandler<GetQuizzesQuery, IList<QuizDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetQuizzesQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IList<QuizDto>> Handle(GetQuizzesQuery request, CancellationToken cancellationToken)
        {
            IList<QuizDto> quizzes = await _context.Quizzes.ProjectTo<QuizDto>(_mapper.ConfigurationProvider).ToListAsync();

            return quizzes;
        }
    }
}

