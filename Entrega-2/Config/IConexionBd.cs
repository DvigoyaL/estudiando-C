using System.Data.SQLite;

namespace Entrega_2.Config
{
    public interface IConexionBd
    {
        void OpenConnection();
        void CloseConnection();
        SQLiteConnection GetConnection();
    }
}