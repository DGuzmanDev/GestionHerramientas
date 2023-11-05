using System.Transactions;
using GestionHerramientas.Models;
using Microsoft.Data.SqlClient;

namespace GestionHerramientas.Interfaces
{
    public interface IRepositorioColaborador
    {
        void Guardar(Colaborador colaborador, SqlConnection connection, TransactionScope tx);

        Colaborador SelecionarPorId(int id, SqlConnection connection);

        Colaborador SelecionarPorIdentificacion(string identificacion, SqlConnection connection);
    }
}