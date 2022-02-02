using Application.Quizzes.Commands.ApproveQuiz;
using Application.Quizzes.Commands.DeleteQuiz;
using Application.Quizzes.Commands.UpsertQuiz;
using Application.Quizzes.Queries.GetQuiz;
using Application.Quizzes.Queries.GetQuizzes;
using Domain.Enums;
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
        GetQuizzesQuery query = new() { QuizFilter = QuizFilter.PublicAndApproved };
        IList<Application.Quizzes.Queries.GetQuizzes.QuizDto> quizzes = await Mediator.Send(query);

        return View(quizzes);
    }

    [HttpGet]
    [Authorize("Administrator")]
    [Route("adminpanel")]
    public async Task<IActionResult> AdminPanel()
    {
        GetQuizzesQuery query = new() { QuizFilter = QuizFilter.None };
        IList<Application.Quizzes.Queries.GetQuizzes.QuizDto> quizzes = await Mediator.Send(query);

        return View(quizzes);
    }

    [HttpGet]
    [Authorize]
    [Route("yourquizzes")]
    public async Task<IActionResult> YourQuizzes()
    {
        GetQuizzesQuery query = new() { QuizFilter = QuizFilter.CurrentUser };

        IList<Application.Quizzes.Queries.GetQuizzes.QuizDto> quizzes = await Mediator.Send(query);

        return View(quizzes);
    }


    [Authorize]
    [HttpGet("/quiz/upsert")]
    public async Task<IActionResult> Upsert(string? id)
    {
        UpsertQuizVm upsertQuizVm = new();

        if (id is not null)
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
    [HttpPost("/quiz/upsert")]
    public async Task<IActionResult> Upsert(UpsertQuizVm upsertQuizVm)
    {
        string id = await Mediator.Send(new UpsertQuizCommand { UpsertQuizVm = upsertQuizVm });
        return RedirectToAction("index", "quiz", new { id });
    }

    [Authorize("Administrator")]
    [HttpPost]
    public async Task<IActionResult> Approve(string id , string returnUrl)
    {
        await Mediator.Send(new ApproveQuizCommand { Id = id });
        return Redirect(returnUrl);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Delete(string id, string returnUrl)
    {
        await Mediator.Send(new DeleteQuizCommand { Id = id });
        return Redirect(returnUrl);
    }
}

