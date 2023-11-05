using GestionHerramientas.Interfaces;
using GestionHerramientas.Models;
using GestionHerramientas.Service;
using GestionHerramientas.Util;
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
            Colaborador respuesta = new();

            try
            {
                _logger.LogInformation("Ejecutando endpoint de registro de nuevo colaborador");

                if (colaborador != null)
                {
                    respuesta = ServicioColaborador.Guardar(colaborador);
                }
                else
                {
                    throw new BadHttpRequestException("El cuerpo de la solicitud no es válido");
                }
            }
            catch (Exception exception)
            {
                TopLevelErrorHandler.ManejarError(exception, nameof(ColaboradorController), nameof(PostColaborador), _logger);
            }

            return respuesta;
        }

        [HttpGet]
        [Route("buscar/identificacion")]
        public Colaborador GetColaboradorPorId([FromQuery(Name = "identificacion")] string identificacion)
        {
            Colaborador colaborador = new();

            try
            {
                if (!StringUtils.IsEmpty(identificacion))
                {
                    colaborador = ServicioColaborador.BuscarPorIdentificacion(identificacion);
                }
                else
                {
                    throw new HttpError.BadRequest("Indentificacion invalida");
                }
            }
            catch (Exception exception)
            {
                TopLevelErrorHandler.ManejarError(exception, nameof(ColaboradorController), nameof(GetColaboradorPorId), _logger);
            }

            return colaborador;
        }
    }
}

