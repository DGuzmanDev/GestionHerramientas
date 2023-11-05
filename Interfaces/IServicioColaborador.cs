using System;
using GestionHerramientas.Models;

namespace GestionHerramientas.Interfaces
{
    public interface IServicioColaborador
    {
        // TODO DOCS
        Colaborador Guardar(Colaborador colaborador);

        // TODO Docs
        Colaborador BuscarPorIdentificacion(string identificacion);
    }
}

