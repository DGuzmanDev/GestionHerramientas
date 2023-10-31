using System.Data;
using System.Transactions;
using GestionHerramientas.Interfaces;
using GestionHerramientas.Models;
using GestionHerramientas.Properties;
using Microsoft.Data.SqlClient;

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
                    //TODO: Agregar un mejor mensaje de error y un exception type mas apropiado
                    throw new Exception("No se pudo guardar la Herramienta");
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
                    //TODO: Agregar un mejor mensaje de error y un exception type mas apropiado
                    throw new Exception("No se pudo actualizar la Herramienta");
                }
            }
            else
            {
                throw new ArgumentNullException(nameof(herramienta), "El objeto Herramienta es invalido");
            }
        }

        /// <inheritdoc />
        public Herramienta SeleccionarPorId(int id, SqlConnection connection)
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
                string codigo = (string)sqlDataReader[PropiedadesBD.Herramienta._ColumnaCodigo];
                string nombre = (string)sqlDataReader[PropiedadesBD.Herramienta._ColumnaNombre];
                string descripcion = (string)sqlDataReader[PropiedadesBD.Herramienta._ColumnaDescripcion];
                int colaboradorId = (int)sqlDataReader[PropiedadesBD.Herramienta._ColumnaColaboradorId];
                DateTime fechaRegistro = (DateTime)sqlDataReader[PropiedadesBD.Herramienta._ColumnaFechaRegistro];
                DateTime fechaActualizacion = (DateTime)sqlDataReader[PropiedadesBD.Herramienta._ColumnaFechaActualizacion];
                DateTime fechaPrestamo = (DateTime)sqlDataReader[PropiedadesBD.Herramienta._ColumnaFechaPrestamo];
                DateTime fechaDevolucion = (DateTime)sqlDataReader[PropiedadesBD.Herramienta._ColumnaFechaDevolucion];
                nuevaHerramienta = new(id, codigo, nombre, descripcion, colaboradorId, new Colaborador(),
                fechaRegistro, fechaActualizacion, fechaPrestamo, fechaDevolucion);
            }

            return nuevaHerramienta;
        }

        /// <inheritdoc />
        public Herramienta SeleccionarPorCodigo(string codigo, SqlConnection connection)
        {
            string query = "SELECT * FROM " + PropiedadesBD._BaseDeDatos + "."
                            + PropiedadesBD._Esquema + "."
                            + PropiedadesBD._TablaHerramientas + " " +
                            "WHERE " + PropiedadesBD.Herramienta._ColumnaCodigo + " = @codigo";

            SqlCommand select = new(query, connection);
            select.Parameters.Add("@codigo", SqlDbType.VarChar).Value = codigo;

            SqlDataReader sqlDataReader = select.ExecuteReader();

            Herramienta nuevaHerramienta = new();
            while (sqlDataReader.Read())
            {
                int id = (int)sqlDataReader[PropiedadesBD.Herramienta._ColumnaId];
                string nombre = (string)sqlDataReader[PropiedadesBD.Herramienta._ColumnaNombre];
                string descripcion = (string)sqlDataReader[PropiedadesBD.Herramienta._ColumnaDescripcion];
                DateTime fechaRegistro = (DateTime)sqlDataReader[PropiedadesBD.Herramienta._ColumnaFechaRegistro];
                nuevaHerramienta = new(id, codigo, nombre, descripcion, fechaRegistro);
            }

            return nuevaHerramienta;
        }
    }
}