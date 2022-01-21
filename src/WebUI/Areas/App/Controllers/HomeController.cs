using Microsoft.AspNetCore.Mvc;

namespace WebUI.Areas.User.Controllers;

public class HomeController : BaseController
{
    public async Task<IActionResult> Index()
    {
        return View();
    }
}

