using System;
using GestionHerramientas.Properties;
using Microsoft.Data.SqlClient;

namespace GestionHerramientas.Datos
{
public class ConexionSQLServer
    {

        private static readonly string _connStr = PropiedadesBD.ObtenerStringDeConexion();

        /**
         * DOCS
         */
        public static SqlConnection ObenerConexion() {
            return new SqlConnection(_connStr);
        }
    }
}

