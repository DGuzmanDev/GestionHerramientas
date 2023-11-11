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

        /// <summary>
        /// Valida las condiciones de integridad de la <paramref name="herramienta"/> dada
        /// </summary>
        /// <param name="herramienta">
        /// La <see cref="Herramienta"/> a validar
        /// </param>
        /// <returns>
        /// <see langword="true"/> si y solo si la <paramref name="herramienta"/> dada cumple con las condiciones
        /// de integridad esperadas para el registro de una nueva herramienta.
        /// </returns>
        private bool ValidarIntegridadHerramienta(Herramienta herramienta)
        {
            return herramienta != null && !StringUtils.IsEmpty(herramienta.Nombre)
                     && !StringUtils.IsEmpty(herramienta.Descripcion);
        }

        /// <summary>
        /// Valida las condiciones de prestamo de las <paramref name="herramientas"/> dadas.
        /// </summary>
        /// <remarks>
        /// En caso de que alguna de las condiciones no se cumpla, se lanza un nuevo <see cref="ArgumentException"/>
        /// con los detalles respectivos al error.
        /// </remarks>
        /// <param name="herramientas">
        /// La lista de <see cref="Herramienta"/> a validar
        /// </param>
        /// <exception cref="ArgumentException">
        /// En caso de que alguna de las herramientas o el colaborador no cumpla con las condiciones esperadas del flujo de trabajo
        /// </exception>
        private void ValidarCondicionesDePrestamo(List<Herramienta> herramientas)
        {
            int? colaboradorId = herramientas.ElementAt(0).ColaboradorId;
            ValidarCantidadDeHerramientasPrestadas(colaboradorId, herramientas.Count);
            herramientas.ForEach(ValidarFechaDePrestamo);
        }

        /// <summary>
        /// Valida que la fecha de devolucion de la <paramref name="herramienta"/> dada no sea mayor a 5 dias
        /// a partir del momento de la validacion en que se ejcuta este metodo.
        /// </summary>
        /// <remarks>
        /// Este metodo configura la fecha de prestamo al momento actual en que se ejcuta el procedimiento (DateTime.Now),
        /// la cual sera la fecha registrada en la base de datos si las validaciones son correctas.
        /// Adicionalmente tambiens se valida que la fecha de devolucion no sea menor a la fecha de prestamo (pasado)
        /// </remarks>
        /// <param name="herramienta">
        /// La <see cref="Herramienta"/> con la informacion de la fecha de devolucion
        /// </param>
        /// <exception cref="ArgumentException">
        /// Si la fecha de devolucion es <see langword="null"/>
        /// Si la fecha de devolucion excede los 5 dias a partir del momento de ejecucion del metodo
        /// Si la fecha de devolucion esta en el pasado a partir del momento de ejecucion del metodo
        /// </exception>
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

        /// <summary>
        /// Valida que la cantidad de herramientas prestadas a un colaborador dado no exceda las 5 herramientas
        /// al considerar la sumatoria de herramientas prestadas segun los registros de la base de datos mas
        /// la cantidad de herramientas que se pretende solicitar.
        /// </summary>
        /// <param name="colaboradorId">
        /// El ID (PK) del colaborador a consultar
        /// </param>
        /// <param name="cantidadSolicitud">
        /// Cantidad de herramientas que se pretende solicitar para prestamo
        /// </param>
        /// <exception cref="ArgumentException">
        /// Si el <paramref name="colaboradorId"/> es <see langword="null"/>
        /// Si la cantidad de herramientas prestadas ya suma 5 segun los registros de la base de datos
        /// Si la sumatoria de herramientas en posesion mas la cantidad de herramientas solicitadas es mayor a 5
        /// </exception>
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

