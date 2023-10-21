
namespace GestionHerramientas.Properties
{
    public class PropiedadesBaseDeDatos
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

        public static string ObtenerStringDeConexion()
        {
            return @"Server=" + _Servidor + "," + _Puerto + ";Database=" + _BaseDeDatos
                + ";User Id=" + _UsuarioBaseDeDatos + ";Password=" + _PwdBaseDeDatos + ";TrustServerCertificate=true";
        }
    }
}

