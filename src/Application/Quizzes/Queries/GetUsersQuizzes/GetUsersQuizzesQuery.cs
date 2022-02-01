using Application.Common.Interfaces;
using Application.Quizzes.Queries.GetQuizzes;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Quizzes.Queries.GetUsersQuizzes;

public class GetUsersQuizzesQuery : IRequest<IList<QuizDto>>
{
    public class GetUsersQuizzesQueryHandler : IRequestHandler<GetUsersQuizzesQuery, IList<QuizDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public GetUsersQuizzesQueryHandler(IApplicationDbContext context, IMapper mapper, ICurrentUserService currentUserService)
        {
            _context = context;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<IList<QuizDto>> Handle(GetUsersQuizzesQuery request, CancellationToken cancellationToken)
        {
            IList<QuizDto> quizzes = await _context.Quizzes.Where(quiz => quiz.CreatedBy == _currentUserService.UserId)
                .ProjectTo<QuizDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return quizzes;
        }
    }
}

