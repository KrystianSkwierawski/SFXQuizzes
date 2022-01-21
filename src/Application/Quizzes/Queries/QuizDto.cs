using Application.Common.Mappings;
using Domain.Entities;
using Domain.ValueObjects;

namespace Application.Quizzes.Queries;

public class QuizDto : IMapFrom<Quiz>
{
    public QuizDto()
    {
        SFXNames = new List<SFXName>();
    }

    public string Id { get; set; }
    public string Title { get; set; }
    public IList<SFXName> SFXNames { get; set; }
}

