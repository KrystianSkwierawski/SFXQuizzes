using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Quizzes.Queries;


public class GetQuizQuery : IRequest<QuizDto>
{
    public string Id { get; set; }
    public class GetQuizQueryHandler : IRequestHandler<GetQuizQuery, QuizDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetQuizQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<QuizDto> Handle(GetQuizQuery request, CancellationToken cancellationToken)
        {
            Quiz entity = _context.Quizzes
                .FirstOrDefault(x => x.Id == request.Id);

            if (entity is null)
                throw new NotFoundException(nameof(Quiz), request.Id);

            return _mapper.Map<QuizDto>(entity);
        }
    }
}


