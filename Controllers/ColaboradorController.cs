using GestionHerramientas.Interfaces;
using GestionHerramientas.Models;
using GestionHerramientas.Service;
using Microsoft.AspNetCore.Mvc;

namespace GestionHerramientas.Controllers
{
    [Route("api/[controller]")]
    public class ColaboradorController : Controller
    {
        private readonly ILogger<ColaboradorController> _logger;


        private readonly IServicioColaborador ServicioColaborador;

        public ColaboradorController(ILogger<ColaboradorController> logger)
        {
            _logger = logger;
            ServicioColaborador = new ServicioColaborador();
        }

        [HttpPost]
        [Route("registrar")]
        public Colaborador PostColaborador([FromBody] Colaborador colaborador)
        {
            try
            {
                _logger.LogInformation("Ejecutando endpoint de registro de nuevo colaborador");

                if (colaborador != null)
                {
                    return ServicioColaborador.Guardar(colaborador);
                }
                else
                {
                    throw new BadHttpRequestException("El cuerpo de la solicitud no es válido");
                }
            }
            catch (Exception exception)
            {
                //TODO: Mejorar el mensaje, el manejo de errores top level y el tipo de Ex
                Console.WriteLine("Error del controller: " + exception.Message);
                throw;
            }
        }
    }
}

