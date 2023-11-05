using System;
using GestionHerramientas.Datos;
using GestionHerramientas.Interfaces;
using GestionHerramientas.Models;
using GestionHerramientas.Util;

namespace GestionHerramientas.Service
{
    public class ServicioHerramienta : IServicioHerramienta
    {
        private readonly IConectorDeDatos ConectorDeDatos;

        public ServicioHerramienta()
        {
            ConectorDeDatos = new ConectorDeDatos();
        }

        /// <inheritdoc />
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
                    throw new ArgumentNullException(nameof(herramienta), "Herramienta invalida");
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("Error guardando nueva Herramienta. Razon: " + error.Message);
                throw;
            }
        }

        /// <inheritdoc />
        public Herramienta Actualizar(Herramienta herramienta)
        {
            try
            {
                if (herramienta != null)
                {
                    ValidarCondicionesDePrestamo(herramienta);
                    return ConectorDeDatos.ActualizarHerramienta(herramienta);
                }
                else
                {
                    throw new ArgumentNullException(nameof(herramienta), "Herramienta invalida");
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("Error actualizando nueva Herramienta. Razon: " + error.Message);
                throw;
            }
        }

        /// <inheritdoc />
        public List<Herramienta> SeleccionarPorCodigoONombreSimilar(string filtro)
        {
            if (!StringUtils.IsEmpty(filtro))
            {
                return ConectorDeDatos.BuscarHerramientasPorCodigoONombreSimilar(filtro);
            }
            else
            {
                throw new ArgumentException("El criterio de busqueda no es valido", nameof(filtro));
            }
        }

        // TODO: Docs
        private bool ValidarIntegridadHerramienta(Herramienta herramienta)
        {
            return herramienta != null && !StringUtils.IsEmpty(herramienta.Nombre)
                     && !StringUtils.IsEmpty(herramienta.Descripcion);
        }

        // TODO: Docs
        private void ValidarCondicionesDePrestamo(Herramienta herramienta)
        {
            ValidarFechaDePrestamo(herramienta);
            ValidarCantidadDeHerramientasPrestadas(herramienta);
        }

        // TODO: DOcs
        private void ValidarFechaDePrestamo(Herramienta herramienta)
        {
            herramienta.FechaPrestamo = DateTime.Now;

            if (herramienta.FechaDevolucion != null && herramienta.FechaDevolucion > herramienta.FechaPrestamo)
            {
                int diasPrestamo = (herramienta.FechaDevolucion.Value.Date - herramienta.FechaPrestamo.Value.Date).Days;

                if (diasPrestamo <= 0 || diasPrestamo > 5)
                {
                    throw new ArgumentException("No se puede execeder más de 5 días de préstamo");
                }
            }
            else
            {
                throw new ArgumentException("Fecha de devolución inválida");
            }
        }

        // TODO: Docs
        private void ValidarCantidadDeHerramientasPrestadas(Herramienta herramienta)
        {
            if (herramienta.ColaboradorId != null)
            {
                int herramientasPrestadas = ConectorDeDatos.ContarHerramientasPrestadasPorColaboradorId(herramienta.ColaboradorId.Value);

                if (herramientasPrestadas == 5)
                {
                    throw new ArgumentException("El colaborador ha alcanzado el limite máximo de herramientas prestadas (5)");
                }
            }
            else
            {
                throw new ArgumentException("Colaborador inválido");
            }
        }
    }
}

