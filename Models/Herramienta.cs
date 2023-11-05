using System;
namespace GestionHerramientas.Models
{
    public class Herramienta
    {
        public int? Id { get; set; }
        public String? Codigo { get; set; }
        public String? Nombre { get; set; }
        public String? Descripcion { get; set; }
        public int? ColaboradorId { get; set; }
        public Colaborador? Colaborador { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public DateTime? FechaActualizacion { get; set; }
        public DateTime? FechaPrestamo { get; set; }
        public DateTime? FechaDevolucion { get; set; }

        public Herramienta() { }

        public Herramienta(int id, string codigo, string nombre, string descripcion,
            DateTime fechaRegistro)
        {
            Id = id;
            Codigo = codigo;
            Nombre = nombre;
            Descripcion = descripcion;
            FechaRegistro = fechaRegistro;
        }

        public Herramienta(int id, string codigo, string nombre, string descripcion,
            int? colaboradorId, Colaborador? colaborador, DateTime fechaRegistro, DateTime fechaActualizacion, DateTime? fechaPrestamo, DateTime? fechaDevolucion)
        {
            Id = id;
            Codigo = codigo;
            Nombre = nombre;
            Descripcion = descripcion;
            ColaboradorId = colaboradorId;
            Colaborador = colaborador;
            FechaRegistro = fechaRegistro;
            FechaActualizacion = fechaActualizacion;
            FechaPrestamo = fechaPrestamo;
            FechaDevolucion = fechaDevolucion;
        }
    }
}

