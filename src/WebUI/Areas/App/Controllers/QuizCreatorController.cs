using Microsoft.AspNetCore.Mvc;

namespace WebUI.Areas.User.Controllers;

public class QuizCreatorController : BaseController
{
    public IActionResult Index()
    {
        return View();
    }
}

