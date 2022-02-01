using Application.Quizzes.Commands.CreateQuiz;
using Application.Quizzes.Commands.DeleteQuiz;
using Application.Quizzes.Queries.GetQuiz;
using Application.Quizzes.Queries.GetQuizzes;
using Application.Quizzes.Queries.GetUsersQuizzes;
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
        Application.Quizzes.Queries.GetQuiz.QuizDto quizDto = await Mediator.Send(new GetQuizQuery { Id = id });

        return View(quizDto);
    }

    [HttpGet]
    [Route("explore")]
    public async Task<IActionResult> Explore()
    {
        IList<Application.Quizzes.Queries.GetQuizzes.QuizDto> quizzes = await Mediator.Send(new GetQuizzesQuery());

        return View(quizzes);
    }

    [HttpGet]
    [Authorize]
    [Route("yourquizzes")]
    public async Task<IActionResult> YourQuizzes()
    {
        IList<Application.Quizzes.Queries.GetUsersQuizzes.QuizDto> quizzes = await Mediator.Send(new GetUsersQuizzesQuery());

        return View(quizzes);
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

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Delete(string id)
    {
        await Mediator.Send(new DeleteQuizCommand { Id = id });

        return RedirectToAction("yourquizzes", "quiz");
    }
}

