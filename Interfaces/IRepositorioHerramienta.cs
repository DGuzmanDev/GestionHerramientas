using System.Transactions;
using GestionHerramientas.Models;
using Microsoft.Data.SqlClient;

namespace GestionHerramientas.Interfaces
{
    public interface IRepositorioHerramienta
    {
        // TODO: DOCS
        void Guardar(Herramienta herramienta, SqlConnection connection, TransactionScope tx);

        // TODO: DOCS
        void Actualizar(Herramienta herramienta, SqlConnection connection, TransactionScope tx);

        // TODO: DOCS
        Herramienta SeleccionarPorId(int id, SqlConnection connection);

        Herramienta SeleccionarPorCodigo(string codigo, SqlConnection connection);
    }
}