using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Quizzes.Queries.GetQuizzes;

public class GetQuizzesQuery : IRequest<IList<QuizDto>>
{
    public QuizFilter QuizFilter { get; set; }
    public class GetQuizzesQueryHandler : IRequestHandler<GetQuizzesQuery, IList<QuizDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public GetQuizzesQueryHandler(IApplicationDbContext context, IMapper mapper, ICurrentUserService currentUserService)
        {
            _context = context;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<IList<QuizDto>> Handle(GetQuizzesQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Quiz> quizzes = _context.Quizzes;

            quizzes = request.QuizFilter switch
            {
                QuizFilter.None => quizzes,
                QuizFilter.CurrentUser => quizzes.Where(quiz => quiz.CreatedBy == _currentUserService.UserId),
                QuizFilter.PublicAndApproved => quizzes.Where(quiz => quiz.IsPublic == true && quiz.Approved == true),
                _ => throw new Exception()
            };

            return await quizzes.ProjectTo<QuizDto>(_mapper.ConfigurationProvider).ToListAsync();
        }
    }
}

