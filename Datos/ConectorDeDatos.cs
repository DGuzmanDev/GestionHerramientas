using System;
using GestionHerramientas.Models;
using Microsoft.Data.SqlClient;
using System.Transactions;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Runtime.InteropServices.JavaScript.JSType;
using GestionHerramientas.Properties;

namespace GestionHerramientas.Datos
{
    public class ConectorDeDatos
    {
        private ConexionSQLServer ConexionSQLServer;

        public ConectorDeDatos()
        {
            this.ConexionSQLServer = new ConexionSQLServer();
        }

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
                    string dml = "INSERT INTO colaborador(identificacion, nombre, apellidos, estado) VALUES "
                        + "(@identificacion, @nombre, @apellidos, @estado)";

                    SqlCommand command = new SqlCommand(dml, connection);
                    command.Parameters.Add("@identificacion", SqlDbType.VarChar).Value = colaborador.Identificacion;
                    command.Parameters.Add("@nombre", SqlDbType.VarChar).Value = colaborador.Nombre;
                    command.Parameters.Add("@apellidos", SqlDbType.VarChar).Value = colaborador.Apellidos;
                    command.Parameters.Add("@estado", SqlDbType.Bit).Value = colaborador.Estado;

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected == 1)
                    {
                        tx.Complete();//Commit INSERT

                        string query = "SELECT id, identificacion, nombre, apeliidos, estado, fecha_registro " +
                            "FROM " + PropiedadesBaseDeDatos._BaseDeDatos + "."
                            + PropiedadesBaseDeDatos._Esquema + "."
                            + PropiedadesBaseDeDatos._TablaColaboradores + " " +
                            "WHERE qwse1q";

                        SqlCommand comand = new SqlCommand(query, connection);
                        SqlDataReader sqlDataReader = command.ExecuteReader();

                        Colaborador nuevoColaborador = new Colaborador();

                        while (sqlDataReader.Read())
                        {
                            int id = (int)sqlDataReader["id"];
                            string identificacion = (string)sqlDataReader["identificacion"];
                            string nombre = (string)sqlDataReader["nombre"];
                            string apellidos = (string)sqlDataReader["apellidos"];
                            bool estado = (bool)sqlDataReader["estado"];
                            DateTime fechaRegistro = sqlDataReader.GetDateTime(4);

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

