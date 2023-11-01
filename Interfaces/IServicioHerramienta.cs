using System;
using GestionHerramientas.Models;

namespace GestionHerramientas.Interfaces
{
    public interface IServicioHerramienta
    {
        /// <summary>
        /// Inserta los datos esperados para un nuevo registro de herramienta en la base de datos.
        /// </summary>
        /// <remarks>
        /// Los datos insertandos en la base de datos son solamente aquellos que se esperan del flujo
        /// de registro de una nueva herramienta según la lógica de negocios, a saber: id (PK auto-generado),
        /// nombre, descripción y fecha_registro (auto-generada)
        /// </remarks>
        /// <param name="herramienta">
        /// <see cref="GestionHerramientas.Models.Herramienta">Herramienta</see>
        /// </param>
        Herramienta Guardar(Herramienta herramienta);

        /// <summary>
        /// Actualiza los datos esperados para el préstamo y devolución de herramienta en la base de datos.
        /// </summary>
        /// <remarks>
        /// Los datos actualizados en la base de datos son solamente aquellos que se esperan del flujo
        /// de préstamo y devolución de una nueva herramienta según la lógica de negocios, a saber: colborador_id (FK),
        /// fecha_prestamp, fecha_devolucion y fecha_actualizacion (auto-generada)
        /// </remarks>
        /// <param name="herramienta">
        /// <see cref="GestionHerramientas.Models.Herramienta">Herramienta</see>
        /// </param>
        Herramienta Actualizar(Herramienta herramienta);
    }
}

