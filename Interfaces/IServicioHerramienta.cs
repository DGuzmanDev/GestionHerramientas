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
        Herramienta Prestar(Herramienta herramienta);

        /// <summary>
        /// Actualiza los datos esperados para el préstamo de las herramienta dada en la base de datos.
        /// </summary>
        /// <remarks>
        /// Los datos actualizados en la base de datos son solamente aquellos que se esperan del flujo
        /// de préstamo y devolución de una nueva herramienta según la lógica de negocios, a saber: colborador_id (FK),
        /// fecha_prestamp, fecha_devolucion y fecha_actualizacion (auto-generada)
        /// </remarks>
        /// Lista de <param name="herramienta">
        /// <c cref="GestionHerramientas.Models.Herramienta">Herramienta</c> con los datos actualizados en la base de datos
        /// </param>
        /// <returns>
        /// Lista de <paramref cref="GestionHerramientas.Models.Herramienta">Herramienta</paramref> actualizada en la base de datos
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Se lanza cuando el parametro herramienta es nulo
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Se lanza si las propiedades esperadas de las herramientas no son validas para satisfacer las necesidades
        /// del flujo de trabajo
        /// </exception>
        List<Herramienta> Prestar(List<Herramienta> herramientas);

        /// <summary>
        /// Actualiza los datos esperados para la devolución de las herramienta dada en la base de datos.
        /// </summary>
        /// <remarks>
        /// Los datos actualizados en la base de datos son solamente aquellos que se esperan del flujo
        /// de préstamo y devolución de una nueva herramienta según la lógica de negocios, a saber: colborador_id (FK),
        /// fecha_prestamp, fecha_devolucion y fecha_actualizacion (auto-generada)
        /// </remarks>
        /// Lista de <param name="herramienta">
        /// <c cref="GestionHerramientas.Models.Herramienta">Herramienta</c> con los datos actualizados en la base de datos
        /// </param>
        /// <returns>
        /// Lista de <paramref cref="GestionHerramientas.Models.Herramienta">Herramienta</paramref> actualizada en la base de datos
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Se lanza cuando el parametro herramienta es nulo
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Se lanza si las propiedades esperadas de las herramientas no son validas para satisfacer las necesidades
        /// del flujo de trabajo
        /// </exception>
        List<Herramienta> Devolver(List<Herramienta> herramientas);

        /// <summary>
        /// Selecciona los registros de Herramienta donde el codigo o el nombre coincide con el criterio (filtro) de busqueda dado.
        /// </summary>
        /// <remarks>
        /// Los registros seleccionados se filtran en base al parametro dado, donde tanto el codigo o el nombre de la herramienta pueden
        /// coincidir con el filtro dado parcial o completamente.
        /// Para cada uno de los registros encontrados se consulta el registro de Colaborador si existe y se asocia al modelo retornado
        /// </remarks>
        /// <param name="filtro">
        /// El filtro con el criterio de busqueda para la consulta de la base de datos
        /// </param>
        /// <returns>
        /// Listado de <paramref cref="GestionHerramientas.Models.Herramienta">Herramienta</paramref> que cumpla con los criterios de busqueda.
        /// En caso de que no se encuentren registros se retorna una lista vacio.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Se lanza cuando el parametro filtro es invalido
        /// </exception>
        List<Herramienta> SeleccionarPorCodigoONombreSimilar(string filtro);


        /// <summary>
        /// Selecciona los registros de Herramienta donde el ID del colaborador (FK) coincide con el ID dado
        /// </summary>
        /// <param name="id">
        /// El ID (FK) de colaborador
        /// </param>
        /// <returns>
        /// Listado de <paramref cref="GestionHerramientas.Models.Herramienta">Herramienta</paramref> que cumpla con el criterio de busqueda.
        /// En caso de que no se encuentren registros se retorna una lista vacio.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Se lanza cuando el parametro filtro es invalido
        /// </exception>
        List<Herramienta> SeleccionarPorColaboradorId(int id);
    }
}

