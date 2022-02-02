using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.ValueObjects;
using MediatR;

namespace Application.Quizzes.Commands.UpsertQuiz;


public class UpsertQuizCommand : IRequest<string>
{
    public UpsertQuizVm UpsertQuizVm { get; set; }

    public class UpsertQuizCommandHandler : IRequestHandler<UpsertQuizCommand, string>
    {
        private readonly IApplicationDbContext _context;
        private readonly ISFXFileBuilder _SFXFileBulider;
        private readonly ICurrentUserService _currentUserService;

        public UpsertQuizCommandHandler(IApplicationDbContext context, ISFXFileBuilder SFXFileBulider, ICurrentUserService currentUserService)
        {
            _context = context;
            _SFXFileBulider = SFXFileBulider;
            _currentUserService = currentUserService;
        }

        public async Task<string> Handle(UpsertQuizCommand request, CancellationToken cancellationToken)
        {
            string quizId = string.Empty;

            IList<SFX> SFXs = new List<SFX>();

            foreach (var file in request.UpsertQuizVm.Files)
            {
                SFXs.Add(new SFX
                {
                    Name = file.FileName
                });
            };


            if (request.UpsertQuizVm.Id is null)
                quizId = await CreateAsync(request.UpsertQuizVm, SFXs);

            if (request.UpsertQuizVm.Id is not null)
                quizId = await UpdateAsync(request.UpsertQuizVm, SFXs);


            await _context.SaveChangesAsync(cancellationToken);

            await _SFXFileBulider.SaveSFXs(request.UpsertQuizVm.Files, quizId);

            return quizId;
        }

        public async Task<string> CreateAsync(UpsertQuizVm upsertQuizVm, IList<SFX> SFXs)
        {
            Quiz entity = new()
            {
                Id = Guid.NewGuid().ToString(),
                Title = upsertQuizVm.Title,
                IsPublic = upsertQuizVm.IsPublic,
                Approved = upsertQuizVm.Approved, //set if is in role admin
                Author = _currentUserService.UserName,
                SFXs = SFXs
            };

            await _context.Quizzes.AddAsync(entity);

            return entity.Id;
        }

        public async Task<string> UpdateAsync(UpsertQuizVm upsertQuizVm, IList<SFX> SFXs)
        {
            Quiz entity = entity = await _context.Quizzes.FindAsync(upsertQuizVm.Id);

            if (entity is null)
                throw new NotFoundException(nameof(Quiz), upsertQuizVm.Id);

            entity.Title = upsertQuizVm.Title;
            entity.IsPublic = upsertQuizVm.IsPublic;
            entity.Approved = upsertQuizVm.Approved; //set if is in role admin
            entity.SFXs = SFXs;

            await _SFXFileBulider.RemoveSFXs(upsertQuizVm.Id);

            return entity.Id;
        }
    }
}



