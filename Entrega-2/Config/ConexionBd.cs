using System.Data.SQLite;

namespace Entrega_2.Config
{
    
    public class Database : IConexionBd
    {
        private static Database? _instance;
        private readonly SQLiteConnection _connection;

        private Database()
        {
            _connection = new SQLiteConnection("Data Source=miBaseDeDatos.db");
            _connection.Open();
        }

        public static Database Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Database();
                }
                return _instance;
            }
        }

        public void OpenConnection()
        {
            if (_connection.State != System.Data.ConnectionState.Open)
            {
                _connection.Open();
            }
        }
        public void CloseConnection()
        {
            if (_connection.State == System.Data.ConnectionState.Open)
            {
                _connection.Close();
            }
        }
        public SQLiteConnection GetConnection()
        {
            return _connection;
        }        
    }
}
