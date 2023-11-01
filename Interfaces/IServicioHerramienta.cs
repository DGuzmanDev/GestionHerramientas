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
        /// <c cref="GestionHerramientas.Models.Herramienta">Herramienta</c> con los datos a insertar en la base de datos
        /// </param>
        /// <returns>
        /// <paramref cref="GestionHerramientas.Models.Herramienta">Herramienta</paramref> insertada en la base de datos
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Se lanza cuando el parametro herramienta es nulo
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Se lanza si las propiedades esperadas del parametro herramienta no son validas para satisfacer las necesidades
        /// del flujo de trabajo
        /// </exception>
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
        /// <c cref="GestionHerramientas.Models.Herramienta">Herramienta</c> con los datos a actualizar en la base de datos
        /// </param>
        /// <returns>
        /// <paramref cref="GestionHerramientas.Models.Herramienta">Herramienta</paramref> actualizada en la base de datos
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Se lanza cuando el parametro herramienta es nulo
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Se lanza si las propiedades esperadas del parametro herramienta no son validas para satisfacer las necesidades
        /// del flujo de trabajo
        /// </exception>
        Herramienta Actualizar(Herramienta herramienta);
    }
}

