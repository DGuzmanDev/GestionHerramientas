using System.Data;
using System.Data.SqlTypes;
using System.Transactions;
using GestionHerramientas.Exceptions;
using GestionHerramientas.Interfaces;
using GestionHerramientas.Models;
using GestionHerramientas.Properties;
using GestionHerramientas.Util;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.IdentityModel.Tokens;

namespace GestionHerramientas.Datos.Repositorio
{
    public class RepositorioHerramienta : IRepositorioHerramienta
    {

        private static readonly string INSERT_HERRAMIENTA_DML = "INSERT INTO " + PropiedadesBD._BaseDeDatos + "."
                    + PropiedadesBD._Esquema + "."
                    + PropiedadesBD._TablaHerramientas + "("
                    + PropiedadesBD.Herramienta._ColumnaCodigo + ", "
                    + PropiedadesBD.Herramienta._ColumnaNombre + ", "
                    + PropiedadesBD.Herramienta._ColumnaDescripcion
                    + ") VALUES (@codigo, @nombre, @descripcion)";

        private static readonly string UPDATE_HERRAMIENTA_DML = "UPDATE " + PropiedadesBD._BaseDeDatos + "."
            + PropiedadesBD._Esquema + "."
            + PropiedadesBD._TablaHerramientas + " SET "
            + PropiedadesBD.Herramienta._ColumnaColaboradorId + " = @colaboradorId, "
            + PropiedadesBD.Herramienta._ColumnaFechaPrestamo + " = @fechaPrestamo, "
            + PropiedadesBD.Herramienta._ColumnaFechaDevolucion + " = @fechaDevolucion, "
            + PropiedadesBD.Herramienta._ColumnaFechaActualizacion + " = @fechaActualizacion "
            + "WHERE id = @id";

        public RepositorioHerramienta() { }

        /// <inheritdoc />
        public void Guardar(Herramienta herramienta, SqlConnection connection, TransactionScope tx)
        {
            if (herramienta != null && herramienta.Codigo != null
                && herramienta.Nombre != null && herramienta.Descripcion != null)
            {
                string dml = INSERT_HERRAMIENTA_DML;
                SqlCommand insert = new(dml, connection);
                insert.Parameters.Add("@codigo", SqlDbType.VarChar).Value = herramienta.Codigo;
                insert.Parameters.Add("@nombre", SqlDbType.VarChar).Value = herramienta.Nombre;
                insert.Parameters.Add("@descripcion", SqlDbType.VarChar).Value = herramienta.Descripcion;

                int rowsAffected = insert.ExecuteNonQuery();
                if (rowsAffected == 1)
                {
                    tx.Complete();//Commit INSERT
                }
                else
                {
                    throw new DataBaseError.ErrorDeConsistenciaDeDatos("No se pudo guardar la Herramienta");
                }
            }
            else
            {
                throw new ArgumentNullException(nameof(herramienta), "El objeto Herramienta es invalido");
            }
        }

        /// <inheritdoc />
        public void Actualizar(Herramienta herramienta, SqlConnection connection, TransactionScope tx)
        {
            if (herramienta != null && herramienta.Id != null)
            {
                string dml = UPDATE_HERRAMIENTA_DML;
                SqlCommand update = new(dml, connection);
                update.Parameters.Add("@id", SqlDbType.Int).Value = herramienta.Id;
                update.Parameters.Add("@colaboradorId", SqlDbType.Int).Value = herramienta.ColaboradorId;
                update.Parameters.Add("@fechaPrestamo", SqlDbType.DateTime2).Value = herramienta.FechaPrestamo;
                update.Parameters.Add("@fechaDevolucion", SqlDbType.DateTime2).Value = herramienta.FechaDevolucion;
                update.Parameters.Add("@fechaActualizacion", SqlDbType.DateTime2).Value = DateTime.Now;

                int rowsAffected = update.ExecuteNonQuery();
                if (rowsAffected == 1)
                {
                    tx.Complete();//Commit UPDATE
                }
                else
                {
                    throw new DataBaseError.ErrorDeConsistenciaDeDatos("No se pudo actualizar la Herramienta");
                }
            }
            else
            {
                throw new ArgumentNullException(nameof(herramienta), "El objeto Herramienta es invalido");
            }
        }

        /// <inheritdoc />
        public void Actualizar(List<Herramienta> herramientas, SqlConnection connection, TransactionScope tx)
        {
            if (!herramientas.IsNullOrEmpty())
            {
                int rowsAffected = 0;
                string dml = UPDATE_HERRAMIENTA_DML;

                herramientas.ForEach(herramienta =>
                {
                    SqlCommand update = new(dml, connection);
                    update.Parameters.Add("@id", SqlDbType.Int).Value = herramienta.Id;
                    update.Parameters.Add("@colaboradorId", SqlDbType.Int).Value = herramienta.ColaboradorId != null ? herramienta.ColaboradorId : DBNull.Value;
                    update.Parameters.Add("@fechaPrestamo", SqlDbType.DateTime2).Value = herramienta.FechaPrestamo != null ? herramienta.FechaPrestamo : DBNull.Value;
                    update.Parameters.Add("@fechaDevolucion", SqlDbType.DateTime2).Value = herramienta.FechaDevolucion != null ? herramienta.FechaDevolucion : DBNull.Value;
                    update.Parameters.Add("@fechaActualizacion", SqlDbType.DateTime2).Value = DateTime.Now;
                    rowsAffected += update.ExecuteNonQuery();
                });

                if (rowsAffected == herramientas.Count)
                {
                    tx.Complete();//Commit UPDATE
                }
                else
                {
                    throw new DataBaseError.ErrorDeConsistenciaDeDatos("No se pudo actualizar todas las Herramienta");
                }
            }
            else
            {
                throw new ArgumentNullException(nameof(herramientas), "La lista de Herramientas es invalida");
            }
        }

        /// <inheritdoc />
        public Herramienta SeleccionarPorId(int id, SqlConnection connection)
        {
            if (id > 0)
            {
                string query = "SELECT * FROM " + PropiedadesBD._BaseDeDatos + "."
                                            + PropiedadesBD._Esquema + "."
                                            + PropiedadesBD._TablaHerramientas + " " +
                                            "WHERE " + PropiedadesBD.Herramienta._ColumnaId + " = @id";

                SqlCommand select = new(query, connection);
                select.Parameters.Add("@id", SqlDbType.Int).Value = id;

                SqlDataReader sqlDataReader = select.ExecuteReader();

                Herramienta nuevaHerramienta = new();
                while (sqlDataReader.Read())
                {
                    nuevaHerramienta = LeerRegistro(sqlDataReader);
                }
                sqlDataReader.Close();
                return nuevaHerramienta;
            }
            else
            {
                throw new ArgumentException("El ID es invalido");
            }
        }

        /// <inheritdoc />
        public Herramienta SeleccionarPorCodigo(string codigo, SqlConnection connection)
        {
            if (!StringUtils.IsEmpty(codigo))
            {
                string query = "SELECT * FROM " + PropiedadesBD._BaseDeDatos + "."
                                + PropiedadesBD._Esquema + "."
                                + PropiedadesBD._TablaHerramientas + " " +
                                "WHERE " + PropiedadesBD.Herramienta._ColumnaCodigo + " = @codigo";

                SqlCommand select = new(query, connection);
                select.Parameters.Add("@codigo", SqlDbType.VarChar).Value = codigo;

                SqlDataReader sqlDataReader = select.ExecuteReader();

                Herramienta herramienta = new();
                if (sqlDataReader.HasRows)
                {
                    sqlDataReader.Read();
                    herramienta = LeerRegistro(sqlDataReader);
                }
                sqlDataReader.Close();
                return herramienta;
            }
            else
            {
                throw new ArgumentException("El codigo provisto no es valido");
            }
        }

        /// <inheritdoc />
        public int ContarHerramientasPrestadasPorColaboradorId(int id, SqlConnection connection)
        {
            if (id > 0)
            {
                string query = "SELECT COUNT(id) FROM " + PropiedadesBD._BaseDeDatos + "."
                                + PropiedadesBD._Esquema + "."
                                + PropiedadesBD._TablaHerramientas + " " +
                                "WHERE " + PropiedadesBD.Herramienta._ColumnaColaboradorId + " = @id";

                SqlCommand count = new(query, connection);
                count.Parameters.Add("@id", SqlDbType.Int).Value = id;

                return (int)count.ExecuteScalar();
            }
            else
            {
                throw new ArgumentException("El ID es invalido");
            }
        }

        /// <inheritdoc />
        public List<Herramienta> SelecionarPorCodigoONombreSimilar(string filtro, SqlConnection connection)
        {
            if (!StringUtils.IsEmpty(filtro))
            {
                string query = "SELECT * FROM " + PropiedadesBD._BaseDeDatos + "."
                                + PropiedadesBD._Esquema + "."
                                + PropiedadesBD._TablaHerramientas + " "
                                + "WHERE " + PropiedadesBD.Herramienta._ColumnaCodigo + " LIKE @filtro "
                                + "OR " + PropiedadesBD.Herramienta._ColumnaNombre + " LIKE  @filtro";

                SqlCommand select = new(query, connection);
                select.Parameters.Add("@filtro", SqlDbType.VarChar).Value = $"%{filtro}%";

                SqlDataReader sqlDataReader = select.ExecuteReader();

                List<Herramienta> herramientas = new();
                while (sqlDataReader.Read())
                {
                    herramientas.Add(LeerRegistro(sqlDataReader));
                }
                sqlDataReader.Close();
                return herramientas;
            }
            else
            {
                throw new ArgumentException("El filtro provisto no es valido");
            }
        }

        /// <inheritdoc />

        public List<Herramienta> SelecionarPorColaboradorId(int id, SqlConnection connection)
        {
            if (id > 0)
            {
                string query = "SELECT * FROM " + PropiedadesBD._BaseDeDatos + "."
                                + PropiedadesBD._Esquema + "."
                                + PropiedadesBD._TablaHerramientas + " " +
                                "WHERE " + PropiedadesBD.Herramienta._ColumnaColaboradorId + " = @id";

                SqlCommand select = new(query, connection);
                select.Parameters.Add("@id", SqlDbType.Int).Value = id;

                SqlDataReader sqlDataReader = select.ExecuteReader();

                List<Herramienta> herramientas = new();
                while (sqlDataReader.Read())
                {
                    herramientas.Add(LeerRegistro(sqlDataReader));
                }
                sqlDataReader.Close();
                return herramientas;
            }
            else
            {
                throw new ArgumentException("El colaborador ID provisto no es valido");
            }
        }

        /// <summary>
        /// Extrae la informacion contenida en el fila actual en la que se ubica el SqlDataReader dado
        /// </summary>
        /// <remarks>
        /// El SqlDataReader dado debe tener datos
        /// </remarks>
        /// <param name="sqlDataReader">
        /// El <paramref cref="SqlDataReader" name="sqlDataReader"/> con la informacion de la DB
        /// </param>
        /// <returns>
        /// Nuevo objecto <paramref cref="GestionHerramientas.Models.Herramienta">Herramienta</paramref> con 
        /// la informacion de la fila actual segun los datos del <paramref name="sqlDataReader"/>
        /// </returns>
        private Herramienta LeerRegistro(SqlDataReader sqlDataReader)
        {
            int id = (int)sqlDataReader[PropiedadesBD.Herramienta._ColumnaId];
            int? colaboradorId = ConvertFromDBVal<int>(sqlDataReader[PropiedadesBD.Herramienta._ColumnaColaboradorId]);
            string codigo = (string)sqlDataReader[PropiedadesBD.Herramienta._ColumnaCodigo];
            string nombre = (string)sqlDataReader[PropiedadesBD.Herramienta._ColumnaNombre];
            string descripcion = (string)sqlDataReader[PropiedadesBD.Herramienta._ColumnaDescripcion];
            DateTime fechaRegistro = (DateTime)sqlDataReader[PropiedadesBD.Herramienta._ColumnaFechaRegistro];
            DateTime fechaActualizacion = (DateTime)sqlDataReader[PropiedadesBD.Herramienta._ColumnaFechaActualizacion];
            DateTime? fechaPrestamo = ConvertFromDBVal<DateTime>(sqlDataReader[PropiedadesBD.Herramienta._ColumnaFechaPrestamo]);
            DateTime? fechaDevolucion = ConvertFromDBVal<DateTime>(sqlDataReader[PropiedadesBD.Herramienta._ColumnaFechaDevolucion]);
            return new(id, codigo, nombre, descripcion, colaboradorId, null, fechaRegistro, fechaActualizacion, fechaPrestamo, fechaDevolucion);
        }

        /// <summary>
        /// Metodo auxiliar para convertir las columnas DBNull.Value del SqlDataReader a un tipo de dato default.
        /// Este metodo se utiliza solamente para resolver los valores de columnas que permiten valores NULL
        /// </summary>
        /// <returns>
        /// El valor por defecto del tipo T dado o el valor proviniente de la base de datos si esta presente
        /// </returns>
        private static T ConvertFromDBVal<T>(object obj)
        {
            if (obj == null || obj == DBNull.Value)
            {
                return default(T); // retorna el valor por defecto del tipo de dato del modelo
            }
            else
            {
                return (T)obj;
            }
        }
    }
}