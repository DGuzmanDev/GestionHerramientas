using GestionHerramientas.Datos;
using GestionHerramientas.Interfaces;
using GestionHerramientas.Models;
using GestionHerramientas.Util;

namespace GestionHerramientas.Service
{
    public class ServicioColaborador : IServicioColaborador
    {
        private readonly ConectorDeDatos ConectorDeDatos;

        public ServicioColaborador()
        {
            ConectorDeDatos = new ConectorDeDatos();
        }

        /**
         * DOCS
         */
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
                    throw new HttpError.BadRequest("Colaborador invalido");
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("Error guardando nuevo Colaborador. Razon: " + error.Message);
                throw;
            }
        }

        /**
         * DOCS
         */
        private bool ValidarIntegridadColaborador(Colaborador colaborador)
        {
            return colaborador != null && !StringUtils.IsEmpty(colaborador.Identificacion)
                     && !StringUtils.IsEmpty(colaborador.Nombre)
                     && !StringUtils.IsEmpty(colaborador.Apellidos);
        }
    }
}

