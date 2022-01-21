using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Quizzes.Commands.CreateQuiz;


public class CreateQuizCommand : IRequest
{
    public CreateQuizVm CreateQuizVm { get; set; }

    public class CreateQuizCommandHandler : IRequestHandler<CreateQuizCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly ISFXFileBulider _SFXFileBulider;

        public CreateQuizCommandHandler(IApplicationDbContext context, ISFXFileBulider SFXFileBulider)
        {
            _context = context;
            _SFXFileBulider = SFXFileBulider;
        }

        public async Task<Unit> Handle(CreateQuizCommand request, CancellationToken cancellationToken)
        {
            IList<string> SFXNames = new List<string>();

            foreach (var file in request.CreateQuizVm.Files)
            {
                SFXNames.Add(file.FileName);
            };

            Quiz entity = new()
            {
                Id = Guid.NewGuid().ToString(),
                Title = request.CreateQuizVm.Title,
                IsPublic = request.CreateQuizVm.IsPublic,
                SFXNames = SFXNames
            };

            await _context.Quizzes.AddAsync(entity);

            await _context.SaveChangesAsync(cancellationToken);

            _SFXFileBulider.SaveSFXs(request.CreateQuizVm.Files, entity.Id);

            return Unit.Value;
        }
    }
}


