using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Quizzes.Queries.GetQuizzes;

public class QuizDto : IMapFrom<Quiz>
{
    public string Id { get; set; }
    public string Title { get; set; }
}

