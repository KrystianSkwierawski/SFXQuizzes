using Application.Quizzes.Queries;
using Microsoft.AspNetCore.Mvc;
using WebUI.Areas.User.Controllers;

namespace WebUI.Areas.App.Controllers;

public class QuizController : BaseController
{
    public async Task<IActionResult> Index(string id)
    {
        QuizDto quizDto = await Mediator.Send(new GetQuizQuery { Id = id });

        return View(quizDto);
    }
}

