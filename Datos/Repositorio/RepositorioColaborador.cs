using System.Data;
using System.Transactions;
using GestionHerramientas.Exceptions;
using GestionHerramientas.Interfaces;
using GestionHerramientas.Models;
using GestionHerramientas.Properties;
using Microsoft.Data.SqlClient;

namespace GestionHerramientas.Datos.Repositorio
{
    public class RepositorioColaborador : IRepositorioColaborador
    {

        private static readonly string INSERT_COLABORADOR_DML = "INSERT INTO " + PropiedadesBD._BaseDeDatos + "."
                    + PropiedadesBD._Esquema + "."
                    + PropiedadesBD._TablaColaboradores + "("
                    + PropiedadesBD.Colaborador._ColumnaIdentificacion + ", "
                    + PropiedadesBD.Colaborador._ColumnaNombre + ", "
                    + PropiedadesBD.Colaborador._ColumnaApellidos + ", "
                    + PropiedadesBD.Colaborador._ColumnaEstado
                    + ") VALUES (@identificacion, @nombre, @apellidos, @estado)";

        /// <inheritdoc />
        public void Guardar(Colaborador colaborador, SqlConnection connection, TransactionScope tx)
        {
            string dml = INSERT_COLABORADOR_DML;
            SqlCommand insert = new(dml, connection);
            insert.Parameters.Add("@identificacion", SqlDbType.VarChar).Value = colaborador.Identificacion;
            insert.Parameters.Add("@nombre", SqlDbType.VarChar).Value = colaborador.Nombre;
            insert.Parameters.Add("@apellidos", SqlDbType.VarChar).Value = colaborador.Apellidos;
            insert.Parameters.Add("@estado", SqlDbType.Bit).Value = colaborador.Estado;

            int rowsAffected = insert.ExecuteNonQuery();
            if (rowsAffected == 1)
            {
                tx.Complete();//Commit INSERT
            }
            else
            {
                throw new DataBaseError.ErrorDeConsistenciaDeDatos("No se pudo guardar el Colaborador");
            }
        }

        /// <inheritdoc />
        public Colaborador SelecionarPorId(int id, SqlConnection connection)
        {
            string query = "SELECT * FROM " + PropiedadesBD._BaseDeDatos + "."
                         + PropiedadesBD._Esquema + "."
                         + PropiedadesBD._TablaColaboradores + " "
                         + "WHERE " + PropiedadesBD.Colaborador._ColumnaId + " = @id ";

            SqlCommand select = new(query, connection);
            select.Parameters.Add("@id", SqlDbType.Int).Value = id;

            SqlDataReader sqlDataReader = select.ExecuteReader();

            Colaborador nuevoColaborador = new();
            while (sqlDataReader.Read())
            {
                string identificacion = (string)sqlDataReader[PropiedadesBD.Colaborador._ColumnaIdentificacion];
                string nombre = (string)sqlDataReader[PropiedadesBD.Colaborador._ColumnaNombre];
                string apellidos = (string)sqlDataReader[PropiedadesBD.Colaborador._ColumnaApellidos];
                bool estado = (bool)sqlDataReader[PropiedadesBD.Colaborador._ColumnaEstado];
                DateTime fechaRegistro = (DateTime)sqlDataReader[PropiedadesBD.Colaborador._ColumnaFechaRegistro];
                DateTime fechaActualizacion = (DateTime)sqlDataReader[PropiedadesBD.Colaborador._ColumnaFechaActualizacion];
                nuevoColaborador = new(id, identificacion, nombre, apellidos, estado, fechaRegistro, fechaActualizacion);
            }

            sqlDataReader.Close();
            return nuevoColaborador;
        }

        /// <inheritdoc />
        public Colaborador SelecionarPorIdentificacion(string identificacion, SqlConnection connection)
        {
            string query = "SELECT * FROM " + PropiedadesBD._BaseDeDatos + "."
                         + PropiedadesBD._Esquema + "."
                         + PropiedadesBD._TablaColaboradores + " "
                         + "WHERE " + PropiedadesBD.Colaborador._ColumnaIdentificacion + " = @identificacion "
                         + "AND " + PropiedadesBD.Colaborador._ColumnaEstado + " = @estado";

            SqlCommand select = new(query, connection);
            select.Parameters.Add("@identificacion", SqlDbType.VarChar).Value = identificacion;
            select.Parameters.Add("@estado", SqlDbType.Bit).Value = true;

            SqlDataReader sqlDataReader = select.ExecuteReader();

            Colaborador nuevoColaborador = new();
            while (sqlDataReader.Read())
            {
                int id = (int)sqlDataReader[PropiedadesBD.Colaborador._ColumnaId];
                string nombre = (string)sqlDataReader[PropiedadesBD.Colaborador._ColumnaNombre];
                string apellidos = (string)sqlDataReader[PropiedadesBD.Colaborador._ColumnaApellidos];
                bool estado = (bool)sqlDataReader[PropiedadesBD.Colaborador._ColumnaEstado];
                DateTime fechaRegistro = (DateTime)sqlDataReader[PropiedadesBD.Colaborador._ColumnaFechaRegistro];
                DateTime fechaActualizacion = (DateTime)sqlDataReader[PropiedadesBD.Colaborador._ColumnaFechaActualizacion];
                nuevoColaborador = new(id, identificacion, nombre, apellidos, estado, fechaRegistro, fechaActualizacion);
            }

            sqlDataReader.Close();
            return nuevoColaborador;
        }
    }
}