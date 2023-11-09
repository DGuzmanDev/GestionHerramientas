using GestionHerramientas.Interfaces;
using GestionHerramientas.Models;
using GestionHerramientas.Service;
using GestionHerramientas.Util;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

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
        public Herramienta PutPrestarHerramienta([FromBody] Herramienta herramienta)
        {
            Herramienta respuesta = new();

            try
            {
                _logger.LogInformation("Ejecutando endpoint de actualicacion de Herramienta");
                if (herramienta != null)
                {
                    return ServicioHerramienta.Prestar(herramienta);
                }
                else
                {
                    throw new BadHttpRequestException("El cuerpo de la solicitud no es válido");
                }
            }
            catch (Exception exception)
            {
                TopLevelErrorHandler.ManejarError(exception, nameof(HerramientaController), nameof(PutPrestarHerramienta), _logger);
            }

            return respuesta;
        }

        [HttpPut]
        [Route("prestar/lista")]
        public List<Herramienta> PutPrestarHerramientas([FromBody] List<Herramienta> herramientas)
        {
            List<Herramienta> respuesta = new();

            try
            {
                _logger.LogInformation("Ejecutando endpoint de prestamos de Herramientas");
                if (!herramientas.IsNullOrEmpty())
                {
                    return ServicioHerramienta.Prestar(herramientas);
                }
                else
                {
                    throw new BadHttpRequestException("El cuerpo de la solicitud no es válido");
                }
            }
            catch (Exception exception)
            {
                TopLevelErrorHandler.ManejarError(exception, nameof(HerramientaController), nameof(PutPrestarHerramientas), _logger);
            }

            return respuesta;
        }

        [HttpPut]
        [Route("devolver/lista")]
        public List<Herramienta> PutDevolverHerramientas([FromBody] List<Herramienta> herramientas)
        {
            List<Herramienta> respuesta = new();

            try
            {
                _logger.LogInformation("Ejecutando endpoint de devolucion de Herramientas");
                if (!herramientas.IsNullOrEmpty())
                {
                    return ServicioHerramienta.Devolver(herramientas);
                }
                else
                {
                    throw new BadHttpRequestException("El cuerpo de la solicitud no es válido");
                }
            }
            catch (Exception exception)
            {
                TopLevelErrorHandler.ManejarError(exception, nameof(HerramientaController), nameof(PutDevolverHerramientas), _logger);
            }

            return respuesta;
        }

        [HttpGet]
        [Route("buscar/nombreOcodigo")]
        public List<Herramienta> GetHerramientasPorCodigoONombre([FromQuery(Name = "filtro")] string filtro)
        {

            List<Herramienta> resultados = new();

            try
            {
                if (!StringUtils.IsEmpty(filtro))
                {
                    resultados = ServicioHerramienta.SeleccionarPorCodigoONombreSimilar(filtro);
                }
                else
                {
                    throw new ArgumentNullException(nameof(filtro), "El criterio de busqueda en invalido");
                }
            }
            catch (Exception exception)
            {
                TopLevelErrorHandler.ManejarError(exception, nameof(HerramientaController), nameof(GetHerramientasPorCodigoONombre), _logger);
            }

            return resultados;
        }

        [HttpGet]
        [Route("buscar/colaboradorId")]
        public List<Herramienta> GetHerramientasPorColaboradorId([FromQuery(Name = "id")] int id)
        {

            List<Herramienta> resultados = new();

            try
            {
                resultados = ServicioHerramienta.SeleccionarPorColaboradorId(id);
            }
            catch (Exception exception)
            {
                TopLevelErrorHandler.ManejarError(exception, nameof(HerramientaController), nameof(GetHerramientasPorCodigoONombre), _logger);
            }

            return resultados;
        }
    }
}