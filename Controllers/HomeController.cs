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

    public IActionResult RegistroColaborador()
    {
        return View("/Views/Colaboradores/RegistroColaborador.cshtml");
    }

    public IActionResult PrestamoHerramienta()
    {
        return View("/Views/Herramientas/PrestamoHerramienta.cshtml");
    }

    public IActionResult DevolucionHerramienta()
    {
        return View("/Views/Herramientas/DevolucionHerramienta.cshtml");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public async void ErrorHandler()
    {
        _logger.LogError("Ejecutando filtro de manejo de errores de los controladores");
        int statusCode = (int)System.Net.HttpStatusCode.InternalServerError;
        string responseBody = "Error inesperado del servidor";

        var exceptionHandlerPathFeature =
            HttpContext.Features.Get<IExceptionHandlerPathFeature>();

        if (exceptionHandlerPathFeature?.Error != null)
        {
            if (exceptionHandlerPathFeature?.Error is HttpError httpError)
            {
                statusCode = (int)httpError.StatusCode;
                responseBody = httpError.ReasonPrase;
            }
            else
            {
                _logger.LogError($"Excepcion desconocida:{exceptionHandlerPathFeature?.Error.GetType().FullName}");
            }
        }

        HttpContext.Response.StatusCode = statusCode;
        await HttpContext.Response.WriteAsJsonAsync(responseBody);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        _logger.LogError("Redireccionamiento a la pagina de error");
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

