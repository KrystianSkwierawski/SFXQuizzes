using Application.Common.Mappings;
using Domain.Entities;
using Domain.ValueObjects;

namespace Application.Quizzes.Queries;

public class QuizDto : IMapFrom<Quiz>
{
    public QuizDto()
    {
        SFXs = new List<SFX>();
    }

    public string Id { get; set; }
    public string Title { get; set; }
    public IList<SFX> SFXs { get; set; }
}

