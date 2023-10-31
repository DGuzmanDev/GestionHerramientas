using System;
using GestionHerramientas.Models;

namespace GestionHerramientas.Interfaces
{
    public interface IServicioHerramienta
    {
        /**
        * DOCS
        **/
        Herramienta Guardar(Herramienta herramienta);

        /**
        * DOCS
        **/
        Herramienta Actualizar(Herramienta herramienta);
    }
}

