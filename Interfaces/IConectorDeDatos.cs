using GestionHerramientas.Models;

namespace GestionHerramientas.Interfaces
{
    public interface IConectorDeDatos
    {

        // TODO: Docks
        Colaborador GuardarColaborador(Colaborador colaborador);

        // TODO: Docs
        Herramienta GuardarHerramienta(Herramienta herramienta);

        // TODO: Docs
        Herramienta ActualizarHerramienta(Herramienta herramienta);
    }
}