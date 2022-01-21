using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Areas.User.Controllers;

[Area("App")]
public abstract class BaseController : Controller
{
    private ISender _mediator;

    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetService<ISender>();
}

