using System;
using GestionHerramientas.Properties;
using Microsoft.Data.SqlClient;

namespace GestionHerramientas.Datos
{
    public class ConexionSQLServer
    {

        private static readonly string _connStr = PropiedadesBD.ObtenerStringDeConexion();

        // TODO Docs
        public static SqlConnection ObenerConexion()
        {
            return new SqlConnection(_connStr);
        }
    }
}

