using Microsoft.AspNetCore.Http;

namespace Application.Quizzes.Commands.CreateQuiz;

public class CreateQuizVm
{
    public string Title { get; set; }

    public bool IsPublic { get; set; }

    public IFormFileCollection Files { get; set; }
}

