using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public Herramienta PostColaborador([FromBody] Herramienta herramienta)
        {
            try
            {
                _logger.LogInformation("Ejecutando endpoint de registro de nueva Herramienta");

                if (herramienta != null)
                {
                    return ServicioHerramienta.Guardar(herramienta);
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

