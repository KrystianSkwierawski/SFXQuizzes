using Application.Quizzes.Queries.GetQuizzes;
using Microsoft.AspNetCore.Mvc;
using WebUI.Areas.User.Controllers;

namespace WebUI.Areas.App.Controllers;

public class ExploreController : BaseController
{
    [HttpGet]
    [Route("explore")]
    public async Task<IActionResult> Index()
    {
        IList<QuizDto> quizzes = await Mediator.Send(new GetQuizzesQuery());

        return View(quizzes);
    }
}

