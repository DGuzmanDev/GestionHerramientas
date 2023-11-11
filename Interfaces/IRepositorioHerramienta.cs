using System.Transactions;
using GestionHerramientas.Models;
using Microsoft.Data.SqlClient;

namespace GestionHerramientas.Interfaces
{
    public interface IRepositorioHerramienta
    {
        /// <summary>
        /// Inserta en la base de datos un nuevo registro de Herramienta en base al parametro 
        /// del <paramref name="herramienta"/> dado.
        /// </summary>
        /// <param name="herramienta">
        /// La <see cref="GestionHerramientas.Models.Herramienta">Herramienta</see> con los datos a insertar en la base de datos
        /// </param>
        /// <param name="connection">
        /// <see cref="Microsoft.Data.SqlClient.SqlConnection"/> con una conexion valida a la base de datos 
        /// </param>
        /// <param name="tx">
        /// <see cref="System.Transactions.TransactionScope"/> con el contexto de la transaccion dentro de la cual se ejecuta la sentencia 
        /// </param>
        void Guardar(Herramienta herramienta, SqlConnection connection, TransactionScope tx);

        /// <summary>
        /// Actualiza el registro de Herramienta con los datos provistos por la <paramref name="herramienta"/> dada
        /// </summary>
        /// <remarks>
        /// Los columnas actualizadas corresponden solamente a los datos esperados del flujo de trabajo para una prestar una herramienta:
        /// colarborador_id, fecha_prestamo, fecha_devolucion, fecha_actualizacion (auto-generada). Los valores puden ser nulos.
        /// </remarks>
        /// <param name="herramienta">
        /// La <see cref="GestionHerramientas.Models.Herramienta">Herramienta</see> con los datos a actualizar en la base de datos
        /// </param>
        /// <param name="connection">
        /// <see cref="Microsoft.Data.SqlClient.SqlConnection"/> con una conexion valida a la base de datos 
        /// </param>
        /// <param name="tx">
        /// <see cref="System.Transactions.TransactionScope"/> con el contexto de la transaccion dentro de la cual se ejecuta la sentencia 
        /// </param>
        void Actualizar(Herramienta herramienta, SqlConnection connection, TransactionScope tx);

        /// <summary>
        /// Actualiza los registros de Herramienta con los datos provistos por las <paramref name="herramientas"/> dadas
        /// </summary>
        /// <remarks>
        /// Los columnas actualizadas corresponden solamente a los datos esperados del flujo de trabajo para una prestar una herramienta:
        /// colarborador_id, fecha_prestamo, fecha_devolucion, fecha_actualizacion (auto-generada). Los valores puden ser nulos.
        /// </remarks>
        /// <param name="herramienta">
        /// La <see cref="GestionHerramientas.Models.Herramienta">Herramienta</see> con los datos a actualizar en la base de datos
        /// </param>
        /// <param name="connection">
        /// <see cref="Microsoft.Data.SqlClient.SqlConnection"/> con una conexion valida a la base de datos 
        /// </param>
        /// <param name="tx">
        /// <see cref="System.Transactions.TransactionScope"/> con el contexto de la transaccion dentro de la cual se ejecuta la sentencia 
        /// </param>
        void Actualizar(List<Herramienta> herramientas, SqlConnection connection, TransactionScope tx);

        /// <summary>
        /// Consulta la base de datos por el registro de Herramienta asociado al <paramref name="id"/> dado (PK)
        /// </summary>
        /// <param name="id">
        /// El ID (PK) de la herramienta a consultar
        /// </param>
        /// <param name="connection">
        /// <see cref="Microsoft.Data.SqlClient.SqlConnection"/> con una conexion valida a la base de datos 
        /// </param>
        /// <returns>
        /// El registro de <see cref="GestionHerramientas.Models.Herramienta">Herramienta</see> asociado al <paramref name="id"/> dado
        /// </returns>
        Herramienta SeleccionarPorId(int id, SqlConnection connection);

        /// <summary>
        /// Consulta la base de datos por el registro de Herramienta asociado al <paramref name="codigo"/> dado (PK)
        /// </summary>
        /// <param name="codigo">
        /// El codigo de la herramienta a consultar
        /// </param>
        /// <param name="connection">
        /// <see cref="Microsoft.Data.SqlClient.SqlConnection"/> con una conexion valida a la base de datos 
        /// </param>
        /// <returns>
        /// El registro de <see cref="GestionHerramientas.Models.Herramienta">Herramienta</see> asociado al <paramref name="codigo"/> dado
        /// </returns>
        Herramienta SeleccionarPorCodigo(string codigo, SqlConnection connection);

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
        List<Herramienta> SelecionarPorCodigoONombreSimilar(string filtro, SqlConnection connection);

        /// <summary>
        /// Consulta la base de datos por el registro de Herramienta asociado al colaborador <paramref name="id"/> dado (FK)
        /// </summary>
        /// <param name="id">
        /// El colaborador ID (FK) de la herramienta a consultar
        /// </param>
        /// <param name="connection">
        /// <see cref="Microsoft.Data.SqlClient.SqlConnection"/> con una conexion valida a la base de datos 
        /// </param>
        /// <returns>
        /// El registro de <see cref="GestionHerramientas.Models.Herramienta">Herramienta</see> asociado al colaborador <paramref name="id"/> dado
        /// </returns>
        List<Herramienta> SelecionarPorColaboradorId(int id, SqlConnection connection);

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
        int ContarHerramientasPrestadasPorColaboradorId(int id, SqlConnection connection);
    }
}