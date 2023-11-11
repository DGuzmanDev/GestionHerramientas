using GestionHerramientas.Datos;
using GestionHerramientas.Interfaces;
using GestionHerramientas.Models;
using GestionHerramientas.Util;

namespace GestionHerramientas.Service
{
    public class ServicioColaborador : IServicioColaborador
    {
        private readonly IConectorDeDatos ConectorDeDatos;

        public ServicioColaborador()
        {
            ConectorDeDatos = new ConectorDeDatos();
        }

        /// <inheritdoc />
        public Colaborador Guardar(Colaborador colaborador)
        {
            try
            {
                if (ValidarIntegridadColaborador(colaborador))
                {
                    return ConectorDeDatos.GuardarColaborador(colaborador);
                }
                else
                {
                    throw new ArgumentNullException(nameof(colaborador), "Colaborador invalido");
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("Error guardando nuevo Colaborador. Razon: " + error.Message);
                throw;
            }
        }

        /// <inheritdoc />
        public Colaborador BuscarPorIdentificacion(string identificacion)
        {
            try
            {
                if (!StringUtils.IsEmpty(identificacion))
                {
                    return ConectorDeDatos.BuscarColaboradorPorIdentificacion(identificacion);
                }
                else
                {
                    throw new ArgumentNullException(nameof(identificacion), "Identificacion invalida");
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("Error buscando Colaborador por Identificacion. Razon: " + error.Message);
                throw;
            }
        }

        /// <summary>
        /// Valida la integrdidad del <paramref name="colaborador"/> dado.
        /// </summary>
        /// <param name="colaborador"></param>
        /// <returns>
        /// <see langword="true"/> si y solo si el <paramref name="colaborador"/> cumple con todas las condiciones
        /// de integridad esperadas para el flujo de registro de un nuevo Colaborador
        /// </returns>
        private bool ValidarIntegridadColaborador(Colaborador colaborador)
        {
            return colaborador != null && !StringUtils.IsEmpty(colaborador.Identificacion)
                     && !StringUtils.IsEmpty(colaborador.Nombre)
                     && !StringUtils.IsEmpty(colaborador.Apellidos);
        }
    }
}

