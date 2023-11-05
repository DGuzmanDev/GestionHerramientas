using GestionHerramientas.Models;
using Microsoft.Data.SqlClient;
using System.Transactions;
using GestionHerramientas.Datos.Repositorio;
using GestionHerramientas.Interfaces;

namespace GestionHerramientas.Datos
{
    public class ConectorDeDatos : IConectorDeDatos
    {
        private readonly IRepositorioColaborador RepositorioColaborador;
        private readonly IRepositorioHerramienta RepositorioHerramienta;

        public ConectorDeDatos()
        {
            RepositorioHerramienta = new RepositorioHerramienta();
            RepositorioColaborador = new RepositorioColaborador();
        }

        /// <inheritdoc />
        public Colaborador GuardarColaborador(Colaborador colaborador)
        {
            if (colaborador != null && colaborador.Identificacion != null)
            {
                using (TransactionScope tx = new(TransactionScopeOption.RequiresNew))
                {
                    SqlConnection connection = ConexionSQLServer.ObenerConexion();

                    try
                    {
                        connection.Open();
                        RepositorioColaborador.Guardar(colaborador, connection, tx);
                        return RepositorioColaborador.SelecionarPorIdentificacion(colaborador.Identificacion, connection);
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
            else
            {
                throw new ArgumentNullException(nameof(colaborador), "El objeto Colaborador es invalido");
            }
        }

        /// <inheritdoc />
       public Colaborador BuscarColaboradorPorIdentificacion(string identificacion)
        {
            SqlConnection connection = ConexionSQLServer.ObenerConexion();

            try
            {
                connection.Open();
                return RepositorioColaborador.SelecionarPorIdentificacion(identificacion, connection);
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error buscando contando Colaborador por identificacion. Razon: " + exception.Message);
                throw;
            }
            finally
            {
                connection.Close();
            }
        }


        public Herramienta GuardarHerramienta(Herramienta herramienta)
        {
            if (herramienta != null && herramienta.Codigo != null)
            {
                using (TransactionScope tx = new(TransactionScopeOption.RequiresNew))
                {
                    SqlConnection connection = ConexionSQLServer.ObenerConexion();

                    try
                    {
                        connection.Open();
                        RepositorioHerramienta.Guardar(herramienta, connection, tx);
                        return RepositorioHerramienta.SeleccionarPorCodigo(herramienta.Codigo, connection);
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
            else
            {
                throw new ArgumentNullException(nameof(herramienta), "El objeto Herramienta es invalido");
            }
        }

        /// <inheritdoc />
        public Herramienta ActualizarHerramienta(Herramienta herramienta)
        {
            if (herramienta != null && herramienta.Id != null)
            {
                using (TransactionScope tx = new(TransactionScopeOption.RequiresNew))
                {
                    SqlConnection connection = ConexionSQLServer.ObenerConexion();

                    try
                    {
                        connection.Open();
                        RepositorioHerramienta.Actualizar(herramienta, connection, tx);
                        return RepositorioHerramienta.SeleccionarPorId(herramienta.Id.Value, connection);
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine("Error actualizando Herramienta. Razon: " + exception.Message);
                        throw;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
            else
            {
                throw new ArgumentNullException(nameof(herramienta), "El objeto Herramienta es invalido");
            }
        }

        /// <inheritdoc />
        public int ContarHerramientasPrestadasPorColaboradorId(int colaboradorId)
        {
            SqlConnection connection = ConexionSQLServer.ObenerConexion();

            try
            {
                connection.Open();
                return RepositorioHerramienta.ContarHerramientasPrestadasPorColaboradorId(colaboradorId, connection);
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error guardando contando herramientas prestadas. Razon: " + exception.Message);
                throw;
            }
            finally
            {
                connection.Close();
            }
        }

    }
}

