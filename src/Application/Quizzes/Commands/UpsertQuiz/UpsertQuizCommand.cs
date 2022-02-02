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
        private readonly IIdentityService _identityService;

        public UpsertQuizCommandHandler(IApplicationDbContext context, ISFXFileBuilder SFXFileBulider, ICurrentUserService currentUserService, IIdentityService identityService)
        {
            _context = context;
            _SFXFileBulider = SFXFileBulider;
            _currentUserService = currentUserService;
            _identityService = identityService;
        }

        public async Task<string> Handle(UpsertQuizCommand request, CancellationToken cancellationToken)
        {
            Quiz entity = new();

            IList<SFX> SFXs = new List<SFX>();

            foreach (var file in request.UpsertQuizVm.Files)
            {
                SFXs.Add(new SFX
                {
                    Name = file.FileName
                });
            };


            bool isInRoleAdmin = await _identityService.IsInRoleAsync(_currentUserService.UserId, "Administrator");

            if (request.UpsertQuizVm.Id is null)
                entity = await CreateAsync(request.UpsertQuizVm, SFXs);

            if (request.UpsertQuizVm.Id is not null)
                entity = await UpdateAsync(request.UpsertQuizVm, SFXs, isInRoleAdmin);


            if (isInRoleAdmin)
                entity.Approved = request.UpsertQuizVm.Approved;

            await _context.SaveChangesAsync(cancellationToken);

            await _SFXFileBulider.SaveSFXs(request.UpsertQuizVm.Files, entity.Id);

            return entity.Id;
        }

        public async Task<Quiz> CreateAsync(UpsertQuizVm upsertQuizVm, IList<SFX> SFXs)
        {
            Quiz entity = new()
            {
                Id = Guid.NewGuid().ToString(),
                Title = upsertQuizVm.Title,
                IsPublic = upsertQuizVm.IsPublic,
                Author = _currentUserService.UserName,
                SFXs = SFXs
            };

            await _context.Quizzes.AddAsync(entity);

            return entity;
        }

        public async Task<Quiz> UpdateAsync(UpsertQuizVm upsertQuizVm, IList<SFX> SFXs, bool isInRoleAdmin)
        {
            Quiz entity = entity = await _context.Quizzes.FindAsync(upsertQuizVm.Id);

            if (entity is null)
                throw new NotFoundException(nameof(Quiz), upsertQuizVm.Id);

            if (!(entity.CreatedBy == _currentUserService.UserId) && !isInRoleAdmin)
                throw new ForbiddenAccessException();

            entity.Title = upsertQuizVm.Title;
            entity.IsPublic = upsertQuizVm.IsPublic;
            entity.SFXs = SFXs;

            await _SFXFileBulider.RemoveSFXs(upsertQuizVm.Id);

            return entity;
        }
    }
}



