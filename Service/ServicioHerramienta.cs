using System;
using GestionHerramientas.Datos;
using GestionHerramientas.Interfaces;
using GestionHerramientas.Models;
using GestionHerramientas.Util;
using Microsoft.IdentityModel.Tokens;

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
        public Herramienta Prestar(Herramienta herramienta)
        {
            try
            {
                if (herramienta != null)
                {
                    ValidarCondicionesDePrestamo(new List<Herramienta> { herramienta });
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
        public List<Herramienta> Prestar(List<Herramienta> herramientas)
        {
            try
            {
                if (!herramientas.IsNullOrEmpty())
                {
                    ValidarCondicionesDePrestamo(herramientas);
                    return ConectorDeDatos.ActualizarHerramientas(herramientas);
                }
                else
                {
                    throw new ArgumentNullException(nameof(herramientas), "Lista de Herramientas invalida");
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("Error actualizando nueva Herramienta. Razon: " + error.Message);
                throw;
            }
        }

        /// <inheritdoc />
        public List<Herramienta> Devolver(List<Herramienta> herramientas)
        {
            try
            {
                if (!herramientas.IsNullOrEmpty())
                {
                    herramientas.ForEach(herramienta =>
                    {
                        herramienta.FechaPrestamo = null;
                        herramienta.FechaDevolucion = null;
                        herramienta.ColaboradorId = null;
                    });

                    return ConectorDeDatos.ActualizarHerramientas(herramientas);
                }
                else
                {
                    throw new ArgumentNullException(nameof(herramientas), "Lista de Herramientas invalida");
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

        /// <inheritdoc />
        public List<Herramienta> SeleccionarPorColaboradorId(int id)
        {
            if (id > 0)
            {
                return ConectorDeDatos.BuscarHerramientasPorColaboradorId(id);
            }
            else
            {
                throw new ArgumentException("El colaborador no es valido", nameof(id));
            }
        }

        // TODO: Docs
        private bool ValidarIntegridadHerramienta(Herramienta herramienta)
        {
            return herramienta != null && !StringUtils.IsEmpty(herramienta.Nombre)
                     && !StringUtils.IsEmpty(herramienta.Descripcion);
        }

        // TODO: Docs
        private void ValidarCondicionesDePrestamo(List<Herramienta> herramientas)
        {
            int? colaboradorId = herramientas.ElementAt(0).ColaboradorId;
            ValidarCantidadDeHerramientasPrestadas(colaboradorId, herramientas.Count);
            herramientas.ForEach(ValidarFechaDePrestamo);
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
                    throw new ArgumentException("No se puede execeder más de 5 días de préstamo para ninguna herramienta");
                }
            }
            else
            {
                throw new ArgumentException("Fecha de devolución inválida");
            }
        }

        // TODO: Docs
        private void ValidarCantidadDeHerramientasPrestadas(int? colaboradorId, int cantidadSolicitud)
        {
            if (colaboradorId != null)
            {
                int herramientasPrestadas = ConectorDeDatos.ContarHerramientasPrestadasPorColaboradorId(colaboradorId.Value);

                if (herramientasPrestadas == 5)
                {
                    throw new ArgumentException("El colaborador ha alcanzado el limite máximo de herramientas prestadas (5)");
                }
                else
                {
                    if (cantidadSolicitud + herramientasPrestadas > 5)
                    {
                        throw new ArgumentException($"No se puede procesar esta solicitud por que excede el limite máximo de herramientas prestadas (5). " +
                        "Cantidad de herrmientas en posesión: {herramientasPrestadas}");
                    }
                }
            }
            else
            {
                throw new ArgumentException("Colaborador inválido");
            }
        }
    }
}

