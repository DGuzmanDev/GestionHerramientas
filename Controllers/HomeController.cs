using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GestionHerramientas.Models;
using Microsoft.AspNetCore.Diagnostics;

namespace GestionHerramientas.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public string? ExceptionMessage { get; set; }

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult RegistroHerramienta()
    {
        return View("/Views/Herramientas/RegistroHerramienta.cshtml");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public async void Error()
    {
        _logger.LogError("Redireccionamiento a la pagina de error");

        var exceptionHandlerPathFeature =
            HttpContext.Features.Get<IExceptionHandlerPathFeature>();

        if (exceptionHandlerPathFeature?.Error is BadHttpRequestException)
        {
            _logger.LogError("Bad Request Exception");
        }

        HttpContext.Response.StatusCode = 400;
        await HttpContext.Response.WriteAsJsonAsync("el error que yo quiero");

        // if (exceptionHandlerPathFeature?.Path == "/")
        // {
        //     ExceptionMessage ??= string.Empty;
        //     ExceptionMessage += " Page: Home.";
        // }
        // return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }


}

