using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GestionHerramientas.Models;
using Microsoft.Data.SqlClient;
using GestionHerramientas.Interfaces;
using GestionHerramientas.Service;

namespace GestionHerramientas.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private readonly IServicioColaborador ServicioColaborador;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
        ServicioColaborador = new ServicioColaborador();
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [HttpPost]
    [Route("registrar")]
    public Colaborador PostColaborador([FromBody] Colaborador colaborador)
    {
        try
        {
            if (colaborador != null)
            {
                return ServicioColaborador.Guardar(colaborador);
            }
            else {
                throw new BadHttpRequestException("El cuerpo de la solicitud no es válido");
            }
        }
        catch (Exception exception)
        {
            //TODO: Mejorar el mensaje, el manejo de errores top level y el tipo de Ex
            Console.WriteLine("Error del controller: " + exception.Message);
            throw exception;
        }
    }
}

