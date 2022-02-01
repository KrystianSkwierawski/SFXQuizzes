using Application.Quizzes.Commands.DeleteQuiz;
using Application.Quizzes.Commands.UpsertQuiz;
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
    [HttpGet]
    public async Task<IActionResult> Upsert(string? id)
    {
        UpsertQuizVm upsertQuizVm = new();

        if(id is not null)
        {
            Application.Quizzes.Queries.GetQuiz.QuizDto quizDto = await Mediator.Send(new GetQuizQuery { Id = id });

            upsertQuizVm.Id = quizDto.Id;
            upsertQuizVm.Title = quizDto.Title;
            upsertQuizVm.IsPublic = quizDto.IsPublic;
            upsertQuizVm.Approved = quizDto.Approved;
        }

        return View(upsertQuizVm);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Upsert(UpsertQuizVm upsertQuizVm)
    {
        string id = await Mediator.Send(new UpsertQuizCommand { UpsertQuizVm = upsertQuizVm });
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

