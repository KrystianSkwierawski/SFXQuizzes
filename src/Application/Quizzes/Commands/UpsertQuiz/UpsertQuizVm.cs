using Microsoft.AspNetCore.Http;

namespace Application.Quizzes.Commands.UpsertQuiz;

public class UpsertQuizVm
{
    public string? Id { get; set; }

    public string Title { get; set; }

    public bool IsPublic { get; set; }

    public bool Approved { get; set; }

    public IList<IFormFile> Files { get; set; }

    public string Captacha { get; set; }
}

