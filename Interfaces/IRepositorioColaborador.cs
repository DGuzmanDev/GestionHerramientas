using System.Transactions;
using GestionHerramientas.Models;
using Microsoft.Data.SqlClient;

namespace GestionHerramientas.Interfaces
{
    public interface IRepositorioColaborador
    {
        /// <summary>
        /// Inserta en la base de datos un nuevo registro de Colaborador en base al parametro 
        /// del <paramref name="colaborador"/> dado.
        /// </summary>
        /// <param name="colaborador">
        /// El <see cref="GestionHerramientas.Models.Colaborador">Colaborador</see> con los datos a insertar en la base de datos
        /// </param>
        /// <param name="connection">
        /// <see cref="Microsoft.Data.SqlClient.SqlConnection"/> con una conexion valida a la base de datos 
        /// </param>
        /// <param name="tx">
        /// <see cref="System.Transactions.TransactionScope"/> con el contexto de la transaccion dentro de la cual se ejecuta la sentencia 
        /// </param>
        /// <returns>
        /// El <see cref="GestionHerramientas.Models.Colaborador">Colaborador</see> insertado en la base de datos
        /// </returns>
        void Guardar(Colaborador colaborador, SqlConnection connection, TransactionScope tx);

        /// <summary>
        /// Consulta la base de datos por el registro de Colaborador asociado al <paramref name="id"/> (PK) dado
        /// </summary>
        /// <param name="id">
        /// El ID (PK) del colaborador para usar como filtro
        /// <param name="connection">
        /// <see cref="Microsoft.Data.SqlClient.SqlConnection"/> con una conexion valida a la base de datos 
        /// </param>
        /// <returns>
        /// El <see cref="GestionHerramientas.Models.Colaborador">Colaborador</see> asociado al <paramref name="id"/> dado
        /// </returns>
        Colaborador SelecionarPorId(int id, SqlConnection connection);

        /// <summary>
        /// Consulta la base de datos por el registro de Colaborador asociado al la <paramref name="identificacion"/> dada
        /// </summary>
        /// <param name="identificacion">
        /// La identificacion del colaborador para usar como filtro
        /// <param name="connection">
        /// <see cref="Microsoft.Data.SqlClient.SqlConnection"/> con una conexion valida a la base de datos 
        /// </param>
        /// <returns>
        /// El <see cref="GestionHerramientas.Models.Colaborador">Colaborador</see> asociado a la <paramref name="identificacion"/> dada
        /// </returns>
        Colaborador SelecionarPorIdentificacion(string identificacion, SqlConnection connection);
    }
}