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
                    throw new HttpError.BadRequest("Herramienta invalida");
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("Error guardando nueva Herramienta. Razon: " + error.Message);
                throw;
            }
        }

        public Herramienta Actualizar(Herramienta herramienta)
        {
            try
            {
                if (ValidarIntegridadHerramienta(herramienta) && ValidarCondicionesPrestamo(herramienta))
                {
                    return ConectorDeDatos.ActualizarHerramienta(herramienta);
                }
                else
                {
                    throw new HttpError.BadRequest("Herramienta invalida");
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("Error actualizando nueva Herramienta. Razon: " + error.Message);
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

        private bool ValidarCondicionesPrestamo(Herramienta herramienta)
        {
            bool valido = false;

            if (herramienta.ColaboradorId != null && herramienta.FechaPrestamo != null
                        && herramienta.FechaDevolucion != null)
            {
                int diasPrestamo = (herramienta.FechaDevolucion.Value.Date - herramienta.FechaPrestamo.Value.Date).Days;
                valido = diasPrestamo > 0 && diasPrestamo <= 5;
            }

            // aqui todavia hace falta validar si el colaborador se le puede prestar mas herramientas
            // solo se le puede prestar un maximo de 5 herramientas. so ya tien las 5 no se le puede prestar mas
            // entonces tengo que mejorar los mensajes de error de este endpoint para que mande mensajes mas elaborados y especificos al FE

            return valido;
        }
    }
}

