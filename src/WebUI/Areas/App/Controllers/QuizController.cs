using Application.Quizzes.Commands.CreateQuiz;
using Application.Quizzes.Queries.GetQuiz;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebUI.Areas.User.Controllers;

namespace WebUI.Areas.App.Controllers;

public class QuizController : BaseController
{
    [HttpGet]
    [Route("quiz/{id?}")]
    public async Task<IActionResult> Index(string id)
    {
        QuizDto quizDto = await Mediator.Send(new GetQuizQuery { Id = id });

        return View(quizDto);
    }

    [Authorize]
    [Route("quiz/create")]
    public async Task<IActionResult> Create()
    {
        CreateQuizVm createQuizVm = new CreateQuizVm();

        return View(createQuizVm);
    }

    [Authorize]
    [HttpPost]
    [Route("quiz/create")]
    public async Task<IActionResult> Create(CreateQuizVm createQuizVm)
    {
        string id = await Mediator.Send(new CreateQuizCommand { CreateQuizVm = createQuizVm });

        return RedirectToAction("index", "quiz", new { id });
    }
}

