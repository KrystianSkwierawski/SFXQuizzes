using Application.Quizzes.Commands.CreateQuiz;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace WebUI.Areas.User.Controllers;


public class QuizCreatorController : BaseController
{
    private IWebHostEnvironment _hostEnvironment;

    public QuizCreatorController(IWebHostEnvironment hostEnvironment)
    {
        _hostEnvironment = hostEnvironment;
    }

    public async Task<IActionResult> Index()
    {
        CreateQuizVm createQuizVm = new CreateQuizVm();

        return View(createQuizVm);
    }

    //[Authorize]
    [HttpPost]
    public async Task<IActionResult> Index(CreateQuizVm createQuizVm)
    {
        await Mediator.Send(new CreateQuizCommand { CreateQuizVm = createQuizVm });

        return View();
    }
}

