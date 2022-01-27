using Application.Quizzes.Queries;
using Microsoft.AspNetCore.Mvc;
using WebUI.Areas.User.Controllers;

namespace WebUI.Areas.App.Controllers;

public class ExploreController : BaseController
{
    [HttpGet]
    [Route("explore")]
    public async Task<IActionResult> Index()
    {
        //QuizDto quizDto = await Mediator.Send(new GetQuizzesQuery { Id = id });

        //return View(quizDto);

        return View();
    }
}

