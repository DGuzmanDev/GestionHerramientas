using System;
using GestionHerramientas.Models;

namespace GestionHerramientas.Interfaces
{
    public interface IServicioColaborador
    {
        /// <summary>
        /// Inserta en la base de datos un nuevo registro de Colaborador en base al parametro 
        /// del <paramref name="colaborador"/> dado.
        /// </summary>
        /// <param name="colaborador">
        /// La <see cref="GestionHerramientas.Models.Colaborador">Colaborador</see> con los datos a insertar en la base de datos
        /// </param>
        /// <param name="connection">
        /// <see cref="Microsoft.Data.SqlClient.SqlConnection"/> con una conexion valida a la base de datos 
        /// </param>
        /// <param name="tx">
        /// <see cref="System.Transactions.TransactionScope"/> con el contexto de la transaccion dentro de la cual se ejecuta la sentencia 
        /// </param>
        /// <returns>
        /// El registro de <see cref="GestionHerramientas.Models.Colaborador">Colaborador</see> insertado en la base de datos
        /// </returns>
        Colaborador Guardar(Colaborador colaborador);

        /// <summary>
        /// Consulta la base de datos por el registro de Colaborador asociado a la <paramref name="identificacion"/> dada
        /// </summary>
        /// <param name="identificacion">
        /// La identificacion del colaborador a consultar
        /// </param>
        /// <param name="connection">
        /// <see cref="Microsoft.Data.SqlClient.SqlConnection"/> con una conexion valida a la base de datos 
        /// </param>
        /// <returns>
        /// El registro de <see cref="GestionHerramientas.Models.Colaborador">Colaborador</see> asociado al <paramref name="identificacion"/> dado
        /// </returns>
        Colaborador BuscarPorIdentificacion(string identificacion);
    }
}

