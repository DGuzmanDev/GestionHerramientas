using System.Transactions;
using GestionHerramientas.Models;
using Microsoft.Data.SqlClient;

namespace GestionHerramientas.Interfaces
{
    public interface IRepositorioColaborador
    {
        void Guardar(Colaborador colaborador, SqlConnection connection, TransactionScope tx);

        Colaborador SelecionarPorIdentificacion(string identificacion, SqlConnection connection);
    }
}