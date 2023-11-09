using GestionHerramientas.Models;
using Microsoft.Data.SqlClient;
using System.Transactions;
using GestionHerramientas.Datos.Repositorio;
using GestionHerramientas.Interfaces;
using GestionHerramientas.Util;
using Microsoft.IdentityModel.Tokens;
using GestionHerramientas.Exceptions;

namespace GestionHerramientas.Datos
{
    public class ConectorDeDatos : IConectorDeDatos
    {
        private static readonly string MENSAJE_UNIQ_KEY_VIOLATION = "Violation of UNIQUE KEY constraint";

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

                        if ((exception is SqlException) && exception.Message.Contains(MENSAJE_UNIQ_KEY_VIOLATION))
                        {
                            throw new DataBaseError.ViolacionDeLlaveUnica("Ya existe un colaborador con la identificación dada", exception);
                        }

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

                        if ((exception is SqlException) && exception.Message.Contains(MENSAJE_UNIQ_KEY_VIOLATION))
                        {
                            throw new DataBaseError.ViolacionDeLlaveUnica("Ya existe una herramienta con el código dado", exception);
                        }

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
        public List<Herramienta> ActualizarHerramientas(List<Herramienta> herramientas)
        {
            if (!herramientas.IsNullOrEmpty())
            {
                using (TransactionScope tx = new(TransactionScopeOption.RequiresNew))
                {
                    SqlConnection connection = ConexionSQLServer.ObenerConexion();

                    try
                    {
                        connection.Open();
                        RepositorioHerramienta.Actualizar(herramientas, connection, tx);

                        // Este proceso se debe optimizar con un SELECT IN si me chance
                        List<Herramienta> resultados = new();
                        herramientas.ForEach(herramienta =>
                        {
                            resultados.Add(RepositorioHerramienta.SeleccionarPorId(herramienta.Id.Value, connection));
                        });
                        return resultados;
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
                throw new ArgumentNullException(nameof(herramientas), "La lista de Herramientas es invalida");
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

        /// <inheritdoc />
        public List<Herramienta> BuscarHerramientasPorCodigoONombreSimilar(string filtro)
        {
            SqlConnection connection = ConexionSQLServer.ObenerConexion();

            try
            {
                if (!StringUtils.IsEmpty(filtro))
                {
                    connection.Open();
                    List<Herramienta> herramientas = RepositorioHerramienta.SelecionarPorCodigoONombreSimilar(filtro, connection);

                    // Este proceso se debe optimizar con un SELECT IN si me chance
                    herramientas.ForEach(herramienta =>
                    {
                        if (herramienta.ColaboradorId != null)
                        {
                            herramienta.Colaborador = RepositorioColaborador.SelecionarPorId(herramienta.ColaboradorId.Value, connection);
                        }
                    });

                    return herramientas;
                }
                else
                {
                    throw new ArgumentException("El filtro provisto no es valido", nameof(filtro));
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error consultando contando herramientas. Razon: " + exception.Message);
                throw;
            }
            finally
            {
                connection.Close();
            }
        }

        /// <inheritdoc />
        public List<Herramienta> BuscarHerramientasPorColaboradorId(int id)
        {
            SqlConnection connection = ConexionSQLServer.ObenerConexion();

            try
            {
                if (id > 0)
                {
                    connection.Open();
                    return RepositorioHerramienta.SelecionarPorColaboradorId(id, connection);
                }
                else
                {
                    throw new ArgumentException("El colaborador ID provisto no es valido", nameof(id));
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error consultando contando herramientas. Razon: " + exception.Message);
                throw;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}

