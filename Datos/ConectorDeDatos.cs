using GestionHerramientas.Models;
using Microsoft.Data.SqlClient;
using System.Transactions;
using System.Data;
using GestionHerramientas.Properties;

namespace GestionHerramientas.Datos
{
    public class ConectorDeDatos
    {

        private static readonly string INSERT_COLABORADOR_DML = "INSERT INTO " + PropiedadesBD._BaseDeDatos + "."
                            + PropiedadesBD._Esquema + "."
                            + PropiedadesBD._TablaColaboradores + "("
                            + PropiedadesBD.Colaborador._ColumnaIdentificacion + ", "
                            + PropiedadesBD.Colaborador._ColumnaNombre + ", "
                            + PropiedadesBD.Colaborador._ColumnaApellidos + ", "
                            + PropiedadesBD.Colaborador._ColumnaEstado
                            + ") VALUES (@identificacion, @nombre, @apellidos, @estado)";

        private static readonly string INSERT_HERRAMIENTA_DML = "INSERT INTO " + PropiedadesBD._BaseDeDatos + "."
                    + PropiedadesBD._Esquema + "."
                    + PropiedadesBD._TablaHerramientas + "("
                    + PropiedadesBD.Herramienta._ColumnaCodigo + ", "
                    + PropiedadesBD.Herramienta._ColumnaNombre + ", "
                    + PropiedadesBD.Herramienta._ColumnaDescripcion
                    + ") VALUES (@codigo, @nombre, @descripcion)";

        public ConectorDeDatos()
        { }

        /**
         * DOCS
         */
        public Colaborador GuardarColaborador(Colaborador colaborador)
        {

            using (TransactionScope tx = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                SqlConnection connection = ConexionSQLServer.ObenerConexion();

                try
                {
                    //TODO: Reducir este metodo, tien que haber algo que pueda centralizarse
                    connection.Open();
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

                        string query = "SELECT * FROM " + PropiedadesBD._BaseDeDatos + "."
                            + PropiedadesBD._Esquema + "."
                            + PropiedadesBD._TablaColaboradores + " " +
                            "WHERE " + PropiedadesBD.Colaborador._ColumnaIdentificacion + " = @identificacion";

                        SqlCommand select = new(query, connection);
                        select.Parameters.Add("@identificacion", SqlDbType.VarChar).Value = colaborador.Identificacion;

                        SqlDataReader sqlDataReader = select.ExecuteReader();

                        Colaborador nuevoColaborador = new();
                        while (sqlDataReader.Read())
                        {
                            int id = (int)sqlDataReader[PropiedadesBD.Colaborador._ColumnaId];
                            string identificacion = (string)sqlDataReader[PropiedadesBD.Colaborador._ColumnaIdentificacion];
                            string nombre = (string)sqlDataReader[PropiedadesBD.Colaborador._ColumnaNombre];
                            string apellidos = (string)sqlDataReader[PropiedadesBD.Colaborador._ColumnaApellidos];
                            bool estado = (bool)sqlDataReader[PropiedadesBD.Colaborador._ColumnaEstado];
                            DateTime fechaRegistro = sqlDataReader.GetDateTime(5);

                            nuevoColaborador = new(id, identificacion, nombre, apellidos, estado, fechaRegistro);
                        }

                        return nuevoColaborador;
                    }
                    else
                    {
                        //TODO: Agregar un mejor mensaje de error y un exception type mas apropiado
                        throw new Exception("No se pudo guardar el Colaborador");
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine("Error guardando nuevo Colaborador. Razon: " + exception.Message);
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }
        }


        /**
        * DOCS
        */
        public Herramienta GuardarHerramienta(Herramienta herramienta)
        {

            using (TransactionScope tx = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                SqlConnection connection = ConexionSQLServer.ObenerConexion();

                try
                {
                    //TODO: Reducir este metodo, tien que haber algo que pueda centralizarse
                    connection.Open();
                    string dml = INSERT_HERRAMIENTA_DML;

                    SqlCommand insert = new(dml, connection);
                    insert.Parameters.Add("@codigo", SqlDbType.VarChar).Value = herramienta.Codigo;
                    insert.Parameters.Add("@nombre", SqlDbType.VarChar).Value = herramienta.Nombre;
                    insert.Parameters.Add("@descripcion", SqlDbType.VarChar).Value = herramienta.Descripcion;

                    int rowsAffected = insert.ExecuteNonQuery();
                    if (rowsAffected == 1)
                    {
                        tx.Complete();//Commit INSERT

                        string query = "SELECT * FROM " + PropiedadesBD._BaseDeDatos + "."
                            + PropiedadesBD._Esquema + "."
                            + PropiedadesBD._TablaHerramientas + " " +
                            "WHERE " + PropiedadesBD.Herramienta._ColumnaCodigo + " = @codigo";

                        SqlCommand select = new(query, connection);
                        select.Parameters.Add("@codigo", SqlDbType.VarChar).Value = herramienta.Codigo;

                        SqlDataReader sqlDataReader = select.ExecuteReader();

                        Herramienta nuevaHerramienta = new();
                        while (sqlDataReader.Read())
                        {
                            int id = (int)sqlDataReader[PropiedadesBD.Herramienta._ColumnaId];
                            string codigo = (string)sqlDataReader[PropiedadesBD.Herramienta._ColumnaCodigo];
                            string nombre = (string)sqlDataReader[PropiedadesBD.Herramienta._ColumnaNombre];
                            string descripcion = (string)sqlDataReader[PropiedadesBD.Herramienta._ColumnaDescripcion];
                            DateTime fechaRegistro = (DateTime)sqlDataReader[PropiedadesBD.Herramienta._ColumnaFechaRegistro];

                            nuevaHerramienta = new(id, codigo, nombre, descripcion, fechaRegistro);
                        }

                        return nuevaHerramienta;
                    }
                    else
                    {
                        //TODO: Agregar un mejor mensaje de error y un exception type mas apropiado
                        throw new Exception("No se pudo guardar la Herramienta");
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine("Error guardando nueva Herramienta. Razon: " + exception.Message);
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}

