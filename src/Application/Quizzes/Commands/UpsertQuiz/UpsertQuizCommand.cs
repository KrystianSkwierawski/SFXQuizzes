using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.ValueObjects;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Text;

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
        private readonly ILogger<UpsertQuizCommandHandler> _logger;


        public UpsertQuizCommandHandler(IApplicationDbContext context, ISFXFileBuilder SFXFileBulider, ICurrentUserService currentUserService, IIdentityService identityService, ILogger<UpsertQuizCommandHandler> logger)
        {
            _context = context;
            _SFXFileBulider = SFXFileBulider;
            _currentUserService = currentUserService;
            _identityService = identityService;
            _logger = logger;
        }

        public async Task<string> Handle(UpsertQuizCommand request, CancellationToken cancellationToken)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                Quiz entity = new();

                IList<SFX> SFXs = new List<SFX>();

                if (request.UpsertQuizVm.Files is not null)
                {
                    foreach (var file in request.UpsertQuizVm.Files)
                    {
                        string nameWithoutExtension = Path.GetFileNameWithoutExtension(file.FileName);
                        string extension = Path.GetExtension(file.FileName);
                        string encodedNameWithExtension = Convert.ToBase64String(Encoding.UTF8.GetBytes(nameWithoutExtension)) + extension;

                        SFXs.Add(new SFX
                        {
                            Name = nameWithoutExtension,
                            EncodedNameWithExtension = encodedNameWithExtension
                        });
                    };
                }

                bool isInRoleAdmin = await _identityService.IsInRoleAsync(_currentUserService.UserId, "Administrator");

                if (request.UpsertQuizVm.Id is null)
                    entity = await CreateAsync(request.UpsertQuizVm, SFXs);

                if (request.UpsertQuizVm.Id is not null)
                    entity = await UpdateAsync(request.UpsertQuizVm, SFXs, isInRoleAdmin);


                if (isInRoleAdmin)
                    entity.Approved = request.UpsertQuizVm.Approved;


                await _context.SaveChangesAsync(cancellationToken);

                transaction.Commit();

                return entity.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating or editing quiz.");

                transaction.Rollback();
                _SFXFileBulider.RemoveSFXs(request.UpsertQuizVm.Id);

                throw ex;
            }
        }

        private async Task<Quiz> CreateAsync(UpsertQuizVm upsertQuizVm, IList<SFX> SFXs)
        {
            Quiz entity = new()
            {
                Id = Guid.NewGuid().ToString(),
                Title = upsertQuizVm.Title,
                IsPublic = upsertQuizVm.IsPublic,
                Author = _currentUserService.UserName
            };

            if (SFXs.Count > 0)
            {
                entity.SFXs = SFXs;
                await _SFXFileBulider.SaveSFXs(upsertQuizVm.Files, entity.Id);
            }

            await _context.Quizzes.AddAsync(entity);

            return entity;
        }

        private async Task<Quiz> UpdateAsync(UpsertQuizVm upsertQuizVm, IList<SFX> SFXs, bool isInRoleAdmin)
        {
            Quiz entity = entity = await _context.Quizzes.FindAsync(upsertQuizVm.Id);

            if (entity is null)
                throw new NotFoundException(nameof(Quiz), upsertQuizVm.Id);

            if (!(entity.CreatedBy == _currentUserService.UserId) && !isInRoleAdmin)
                throw new ForbiddenAccessException();

            entity.Title = upsertQuizVm.Title;
            entity.IsPublic = upsertQuizVm.IsPublic;

            if (SFXs.Count() > 0)
            {
                entity.SFXs = SFXs;
                await _SFXFileBulider.RemoveSFXs(upsertQuizVm.Id);
                await _SFXFileBulider.SaveSFXs(upsertQuizVm.Files, entity.Id);
            }

            return entity;
        }
    }
}



