using System;
namespace GestionHerramientas.Models
{
    public class Colaborador
    {
        public Int32 Id { get; set; }
        public String? Identificacion { get; set; }
        public String? Nombre { get; set; }
        public String? Apellidos { get; set; }
        public Boolean Estado { get; set; }
        public DateTime FechaRegistro { get; set; }

        public Colaborador() { }

        public Colaborador(Int32 id, string identificacion, string nombre, string apellidos,
            bool estado, DateTime fechaRegistro)
        {
            this.Id = id;
            this.Identificacion = identificacion;
            this.Nombre = nombre;
            this.Apellidos = apellidos;
            this.FechaRegistro = fechaRegistro;
            this.Estado = estado;
        }
    }
}

