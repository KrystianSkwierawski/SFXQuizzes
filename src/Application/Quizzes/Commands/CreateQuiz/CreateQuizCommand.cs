using Application.Common.Interfaces;
using Domain.Entities;
using Domain.ValueObjects;
using MediatR;

namespace Application.Quizzes.Commands.CreateQuiz;


public class CreateQuizCommand : IRequest<string>
{
    public CreateQuizVm CreateQuizVm { get; set; }

    public class CreateQuizCommandHandler : IRequestHandler<CreateQuizCommand, string>
    {
        private readonly IApplicationDbContext _context;
        private readonly ISFXFileBuilder _SFXFileBulider;
        private readonly ICurrentUserService _currentUserService;

        public CreateQuizCommandHandler(IApplicationDbContext context, ISFXFileBuilder SFXFileBulider, ICurrentUserService currentUserService)
        {
            _context = context;
            _SFXFileBulider = SFXFileBulider;
            _currentUserService = currentUserService;
        }

        public async Task<string> Handle(CreateQuizCommand request, CancellationToken cancellationToken)
        {
            IList<SFX> SFXs = new List<SFX>();

            foreach (var file in request.CreateQuizVm.Files)
            {
                SFXs.Add(new SFX
                {
                    Name = file.FileName
                });
            };

            Quiz entity = new()
            {
                Id = Guid.NewGuid().ToString(),
                Title = request.CreateQuizVm.Title,
                IsPublic = request.CreateQuizVm.IsPublic,
                Author = _currentUserService.UserName,
                SFXs = SFXs
            };

            await _context.Quizzes.AddAsync(entity);

            await _context.SaveChangesAsync(cancellationToken);

            await _SFXFileBulider.SaveSFXs(request.CreateQuizVm.Files, entity.Id);

            return entity.Id;
        }
    }
}


