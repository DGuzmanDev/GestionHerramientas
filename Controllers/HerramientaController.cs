using GestionHerramientas.Interfaces;
using GestionHerramientas.Models;
using GestionHerramientas.Service;
using Microsoft.AspNetCore.Mvc;

namespace GestionHerramientas.Controllers
{
    [Route("api/[controller]")]
    public class HerramientaController : Controller
    {
        private readonly ILogger<HerramientaController> _logger;

        private readonly IServicioHerramienta ServicioHerramienta;

        public HerramientaController(ILogger<HerramientaController> logger)
        {
            _logger = logger;
            ServicioHerramienta = new ServicioHerramienta();
        }


        [HttpPost]
        [Route("registrar")]
        public Herramienta PostHerramienta([FromBody] Herramienta herramienta)
        {
            Herramienta respuesta = new();

            try
            {
                _logger.LogInformation("Ejecutando endpoint de registro de nueva Herramienta");
                if (herramienta != null)
                {
                    respuesta = ServicioHerramienta.Guardar(herramienta);
                }
                else
                {
                    throw new BadHttpRequestException("El cuerpo de la solicitud no es válido");
                }
            }
            catch (Exception exception)
            {
                TopLevelErrorHandler.ManejarError(exception, nameof(HerramientaController), nameof(PostHerramienta), _logger);
            }

            return respuesta;
        }

        [HttpPut]
        [Route("prestar")]
        public Herramienta PutHerramienta([FromBody] Herramienta herramienta)
        {
            Herramienta respuesta = new();

            try
            {
                _logger.LogInformation("Ejecutando endpoint de actualicacion de Herramienta");
                if (herramienta != null)
                {
                    return ServicioHerramienta.Actualizar(herramienta);
                }
                else
                {
                    throw new BadHttpRequestException("El cuerpo de la solicitud no es válido");
                }
            }
            catch (Exception exception)
            {
                TopLevelErrorHandler.ManejarError(exception, nameof(HerramientaController), nameof(PutHerramienta), _logger);
            }

            return respuesta;
        }
    }
}