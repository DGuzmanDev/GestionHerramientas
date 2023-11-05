using GestionHerramientas.Models;

namespace GestionHerramientas.Interfaces
{
    public interface IConectorDeDatos
    {

        // TODO: Docks
        Colaborador GuardarColaborador(Colaborador colaborador);

        // TODO Docs
        Colaborador BuscarColaboradorPorIdentificacion(string identificacion);

        // TODO: Docs
        int ContarHerramientasPrestadasPorColaboradorId(int colaboradorId);

        // TODO: Docs
        Herramienta GuardarHerramienta(Herramienta herramienta);

        // TODO: Docs
        Herramienta ActualizarHerramienta(Herramienta herramienta);
    }
}