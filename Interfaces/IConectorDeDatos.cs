using GestionHerramientas.Models;

namespace GestionHerramientas.Interfaces
{
    public interface IConectorDeDatos
    {

        /// <summary>
        /// Inserta los datos esperados para un nuevo registro de Colaborador en la base de datos.
        /// </summary>
        /// <param name="colaborador">
        /// El <see cref="GestionHerramientas.Models.Colaborador">Colaborador</see> a insertar en la base de datos
        /// </param>
        /// <returns>
        /// <see cref="GestionHerramientas.Models.Colaborador">Colaborador</see> insertado en la base de datos
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Se lanza cuando el parametro <paramref name="colaborador"/> es nulo o si las propiedades esperadas del 
        /// parametro <paramref name="colaborador"/> no son validas para satisfacer las necesidades del flujo de trabajo
        /// </exception>
        /// <exception cref="DataBaseError.ViolacionDeLlaveUnica">
        /// Se lanza cuando ya existe un registro de Colaborador con la indentificacion dada.
        /// </exception>
        Colaborador GuardarColaborador(Colaborador colaborador);

        /// <summary>
        /// Consulta la base de datos por el registro de Colaborador donde la identificacion cumple con el criterio dado
        /// </summary>
        /// <remarks>
        /// Si no se encuentra un registro donde la identificacion del colaborador sea igual al parametro dado, se retorna un objeto vacio
        /// </remarks>
        /// <param name="identificacion">
        /// La identificacion del Colaborador a utilizar como filtro de busqueda
        /// </param>
        /// <returns>
        /// El registro de <see cref="GestionHerramientas.Models.Colaborador">Colaborador</see> que cumpla con el criterio de busqueda
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Se lanza cuando el parametro <paramref name="identificacion"/> es invalido
        /// </exception>
        Colaborador BuscarColaboradorPorIdentificacion(string identificacion);

        /// <summary>
        /// Consulta la base de datos para contar la cantidad de registros de Herramientas donde la llave foranea de colaborador_id no sea NULL
        /// </summary>
        /// <param name="colaboradorId">
        /// El colaboradorId a utilizar como filtro de busqueda
        /// </param>
        /// <returns>
        /// La suma de registros de herramientas donde colabororador_id (FK) no es NULL
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Se lanza cuando el parametro <paramref name="colaboradorId"/> es invalido (menor o igual a 0)
        /// </exception>
        int ContarHerramientasPrestadasPorColaboradorId(int colaboradorId);

        /// <summary>
        /// Inserta en la base de datos un nuevo registro de Herramienta utilizando los datos la <paramref name="herramienta"/> dada
        /// </summary>
        /// <remarks>
        /// Los columnas insertados corresponden solamente a los datos esperados del flujo de trabajo para una nueva herramienta:
        /// id (PK autogenerado), fecha_registro (autogenerada), nombre, descripcion y codigo.
        /// </remarks>
        /// <param name="herramienta">
        /// La <see cref="GestionHerramientas.Models.Herramienta">Herramienta</see> a insertar en la base de datos
        /// </param>
        /// <returns>
        /// El registro de <see cref="GestionHerramientas.Models.Herramienta">Herramienta</see> insertado en la base datos
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Se lanza cuando el parametro <paramref name="herramienta"/> es invalido 
        /// </exception>
        /// <exception cref="DataBaseError.ViolacionDeLlaveUnica">
        /// Se lanza cuando ya existe un registro de Herramienta con el codigo dado.
        /// </exception>
        Herramienta GuardarHerramienta(Herramienta herramienta);

        /// <summary>
        /// Actualiza el registro de la base de datos asociado con el ID de la <paramref name="herramienta"/> dada
        /// </summary>
        /// <remarks>
        /// Los columnas actualizadas corresponden solamente a los datos esperados del flujo de trabajo para una prestar una herramienta:
        /// colarborador_id, fecha_prestamo, fecha_devolucion, fecha_actualizacion (auto-generada)
        /// </remarks>
        /// <param name="herramienta">
        /// La <see cref="GestionHerramientas.Models.Herramienta">Herramienta</see> a actualizar en la base de datos
        /// </param>
        /// <returns>
        /// El registro de <see cref="GestionHerramientas.Models.Herramienta">Herramienta</see> actualizado en la base datos
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Se lanza cuando el parametro <paramref name="herramienta"/> es invalido 
        /// </exception>
        Herramienta ActualizarHerramienta(Herramienta herramienta);

        /// <summary>
        /// Actualiza los registros de la base de datos asociado con el ID de las <paramref name="herramientas"/> dadas
        /// </summary>
        /// <remarks>
        /// Los columnas actualizadas corresponden solamente a los datos esperados del flujo de trabajo para una prestar una herramienta:
        /// colarborador_id, fecha_prestamo, fecha_devolucion, fecha_actualizacion (auto-generada). Estos datos son extraidos de cada elemento
        /// <see cref="GestionHerramientas.Models.Herramienta">Herramienta</see> en la lista.
        /// </remarks>
        /// <param name="herramientas">
        /// La lista de <see cref="GestionHerramientas.Models.Herramienta">Herramienta</see> a actualizar en la base de datos
        /// </param>
        /// <returns>
        /// El registro de <see cref="GestionHerramientas.Models.Herramienta">Herramienta</see> actualizado en la base datos
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Se lanza cuando el parametro <paramref name="herramienta"/> es invalido 
        /// </exception>
        List<Herramienta> ActualizarHerramientas(List<Herramienta> herramientas);

        /// <summary>
        /// Consulta la base de datos por los registros de Herramienta donde el nombre o el codigo sean similares
        /// al <paramref name="filtro"/> dado
        /// </summary>
        /// <remarks>
        /// La busqueda se lleva a cabo mediante el operador de similitud LIKE con conincidencias en cualquier parte de la cadena de caracteres
        /// </remark>
        /// <param name="filtro">
        /// El filtro de busqueda para la consulta de la base datos
        /// </param>
        /// <returns>
        /// La lista de <see cref="GestionHerramientas.Models.Herramienta">Herramienta</see> que coniciden con el filtro de busqueda
        /// </returns>
        List<Herramienta> BuscarHerramientasPorCodigoONombreSimilar(string filtro);

        /// <summary>
        /// Consulta la base de datos por los registros de Herramienta donde el colaborador ID (FK) coincida con el
        /// <param name="id"> dado
        /// </summary>
        /// <param name="id">
        /// El colaborador ID para la consulta de la base datos
        /// </param>
        /// <returns>
        /// La lista de <see cref="GestionHerramientas.Models.Herramienta">Herramienta</see> que coniciden con el filtro de busqueda
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Se lanza cuando el parametro <paramref name="id"/> es invalido 
        /// </exception>
        List<Herramienta> BuscarHerramientasPorColaboradorId(int id);
    }
}