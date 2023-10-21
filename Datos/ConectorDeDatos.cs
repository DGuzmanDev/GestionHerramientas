using GestionHerramientas.Models;
using Microsoft.Data.SqlClient;
using System.Transactions;
using System.Data;
using GestionHerramientas.Properties;

namespace GestionHerramientas.Datos
{
    public class ConectorDeDatos
    {

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
                    string dml = "INSERT INTO " + PropiedadesBaseDeDatos._BaseDeDatos + "."
                            + PropiedadesBaseDeDatos._Esquema + "."
                            + PropiedadesBaseDeDatos._TablaColaboradores
                            + "(" + Colaborador.ColumnaIdentificacion + ", " + Colaborador.ColumnaNombre
                            + ", " + Colaborador.ColumnaApellidos + ", " + Colaborador.ColumnaEstado + ") VALUES "
                        + "(@identificacion, @nombre, @apellidos, @estado)";

                    SqlCommand insert = new SqlCommand(dml, connection);
                    insert.Parameters.Add("@identificacion", SqlDbType.VarChar).Value = colaborador.Identificacion;
                    insert.Parameters.Add("@nombre", SqlDbType.VarChar).Value = colaborador.Nombre;
                    insert.Parameters.Add("@apellidos", SqlDbType.VarChar).Value = colaborador.Apellidos;
                    insert.Parameters.Add("@estado", SqlDbType.Bit).Value = colaborador.Estado;

                    int rowsAffected = insert.ExecuteNonQuery();
                    if (rowsAffected == 1)
                    {
                        tx.Complete();//Commit INSERT

                        string query = "SELECT * FROM " + PropiedadesBaseDeDatos._BaseDeDatos + "."
                            + PropiedadesBaseDeDatos._Esquema + "."
                            + PropiedadesBaseDeDatos._TablaColaboradores + " " +
                            "WHERE " + Colaborador.ColumnaIdentificacion + " = @identificacion";

                        SqlCommand select = new SqlCommand(query, connection);
                        select.Parameters.Add("@identificacion", SqlDbType.VarChar).Value = colaborador.Identificacion;

                        SqlDataReader sqlDataReader = select.ExecuteReader();

                        Colaborador nuevoColaborador = new Colaborador();

                        while (sqlDataReader.Read())
                        {
                            int id = (int)sqlDataReader[Colaborador.ColumnaId];
                            string identificacion = (string)sqlDataReader[Colaborador.ColumnaIdentificacion];
                            string nombre = (string)sqlDataReader[Colaborador.ColumnaNombre];
                            string apellidos = (string)sqlDataReader[Colaborador.ColumnaApellidos];
                            bool estado = (bool)sqlDataReader[Colaborador.ColumnaEstado];
                            DateTime fechaRegistro = sqlDataReader.GetDateTime(5);

                            nuevoColaborador = new Colaborador(id, identificacion, nombre, apellidos, estado, fechaRegistro);
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
    }
}

