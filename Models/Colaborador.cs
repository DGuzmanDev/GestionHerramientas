using System;
namespace GestionHerramientas.Models
{
    public class Colaborador
    {
        public int? Id { get; set; }
        public String? Identificacion { get; set; }
        public String? Nombre { get; set; }
        public String? Apellidos { get; set; }
        public bool? Estado { get; set; }
        public DateTime? FechaRegistro { get; set; }

        public Colaborador() { }

        public Colaborador(int id, string identificacion, string nombre, string apellidos,
            bool estado, DateTime fechaRegistro)
        {
            Id = id;
            Identificacion = identificacion;
            Nombre = nombre;
            Apellidos = apellidos;
            FechaRegistro = fechaRegistro;
            Estado = estado;
        }
    }
}

