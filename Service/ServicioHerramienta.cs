using System;
using GestionHerramientas.Datos;
using GestionHerramientas.Interfaces;
using GestionHerramientas.Models;
using GestionHerramientas.Util;

namespace GestionHerramientas.Service
{
    public class ServicioHerramienta : IServicioHerramienta
    {
        private readonly ConectorDeDatos ConectorDeDatos;

        public ServicioHerramienta()
        {
            ConectorDeDatos = new ConectorDeDatos();
        }

        public Herramienta Guardar(Herramienta herramienta)
        {
            try
            {
                if (ValidarIntegridadHerramienta(herramienta))
                {
                    return ConectorDeDatos.GuardarHerramienta(herramienta);
                }
                else
                {
                    //TDOD: Mejorar el mensaje y el tipo de EX
                    throw new Exception("Herramienta invalida");
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("Error guardando nueva Herramienta. Razon: " + error.Message);
                throw;
            }
        }

        /**
        * DOCS
        */
        private bool ValidarIntegridadHerramienta(Herramienta herramienta)
        {
            return herramienta != null && !StringUtils.IsEmpty(herramienta.Nombre)
                     && !StringUtils.IsEmpty(herramienta.Descripcion);
        }
    }
}

