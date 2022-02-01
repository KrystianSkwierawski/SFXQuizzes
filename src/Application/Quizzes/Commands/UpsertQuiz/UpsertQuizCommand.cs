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
            Quiz entity = new();

            IList<SFX> SFXs = new List<SFX>();

            foreach (var file in request.UpsertQuizVm.Files)
            {
                SFXs.Add(new SFX
                {
                    Name = file.FileName
                });
            };

            if (request.UpsertQuizVm.Id is null)
            {
                entity.Id = Guid.NewGuid().ToString();
                entity.Title = request.UpsertQuizVm.Title;
                entity.IsPublic = request.UpsertQuizVm.IsPublic;
                entity.Approved = request.UpsertQuizVm.Approved; //set if is in role admin
                entity.Author = _currentUserService.UserName;
                entity.SFXs = SFXs;

                await _context.Quizzes.AddAsync(entity);
            }

            if(request.UpsertQuizVm.Id is not null)
            {
                entity = await _context.Quizzes.FindAsync(request.UpsertQuizVm.Id);

                entity.Title = request.UpsertQuizVm.Title;
                entity.IsPublic = request.UpsertQuizVm.IsPublic;
                entity.Approved = request.UpsertQuizVm.Approved; //set if is in role admin
                entity.SFXs = SFXs;

                await _SFXFileBulider.RemoveSFXs(request.UpsertQuizVm.Id);
            }

            await _context.SaveChangesAsync(cancellationToken);

            await _SFXFileBulider.SaveSFXs(request.UpsertQuizVm.Files, entity.Id);

            return entity.Id;
        }
    }
}


