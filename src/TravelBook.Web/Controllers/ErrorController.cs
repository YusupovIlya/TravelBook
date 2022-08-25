using Microsoft.AspNetCore.Mvc;

namespace TravelBook.Web.Controllers;

[Route("error")]
public class ErrorController : Controller
{
    private readonly ILogger<ErrorController> _logger;

    public ErrorController(ILogger<ErrorController> logger)
    {
        _logger = logger;
    }

    [Route("404")]
    public IActionResult PageNotFound()
    {
        _logger.LogError($"Error PageNotFound");
        return View();
    }

    [Route("403")]
    public IActionResult PageAccessDenied()
    {
        Response.StatusCode = 403;
        _logger.LogError($"Error PageAccessDenied");
        return View();
    }
}
