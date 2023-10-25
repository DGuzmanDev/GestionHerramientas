﻿
namespace GestionHerramientas.Properties
{
    public class PropiedadesBD
    {
        //Propiedades generales
        public static readonly string _Servidor = "127.0.0.1";
        public static readonly string _Puerto = "1433";
        public static readonly string _BaseDeDatos = "GestionHerramientas";
        public static readonly string _Esquema = "GestionHerramientas";
        public static readonly string _UsuarioBaseDeDatos = "Dguzman";
        public static readonly string _PwdBaseDeDatos = "Admin@SQLServer03101";

        //Propiedades especificas
        public static readonly string _TablaColaboradores = "colaborador";
        public static readonly string _TablaHerramientas = "herramienta";

        public static string ObtenerStringDeConexion()
        {
            return @"Server=" + _Servidor + "," + _Puerto + ";Database=" + _BaseDeDatos
                + ";User Id=" + _UsuarioBaseDeDatos + ";Password=" + _PwdBaseDeDatos + ";TrustServerCertificate=true";
        }

        public static class Colaborador
        {
            //Propiedades estaticas asocias con la Base de Datos
            public static readonly string _Nombre = "colaborador";
            public static readonly string _ColumnaId = "id";
            public static readonly string _ColumnaIdentificacion = "identificacion";
            public static readonly string _ColumnaNombre = "nombre";
            public static readonly string _ColumnaApellidos = "apellidos";
            public static readonly string _ColumnaEstado = "estado";
            public static readonly string _ColumnaFechaRegistro = "fecha_registro";
        }

        public static class Herramienta
        {
            //Propiedades estaticas asocias con la Base de Datos
            public static readonly string _Nombre = "herramienta";
            public static readonly string _ColumnaId = "id";
            public static readonly string _ColumnaCodigo = "codigo";
            public static readonly string _ColumnaNombre = "nombre";
            public static readonly string _ColumnaDescripcion = "descripcion";
            public static readonly string _ColumnaColaboradorId = "colaborador_id";
            public static readonly string _ColumnaFechaRegistro = "fecha_registro";
            public static readonly string _ColumnaFechaPrestamo = "fecha_prestamo";
            public static readonly string _ColumnaFechaDevolucion = "fecha_devolucion";
        }
    }
}

