using Microsoft.AspNetCore.Mvc;

namespace WebUI.Areas.User.Controllers;

public class HomeController : BaseController
{
    public IActionResult Index()
    {
        return View();
    }
}

