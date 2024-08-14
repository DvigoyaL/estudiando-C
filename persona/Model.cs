using Microsoft.Data.Sqlite;

public class Database
{
    // Campo estático para almacenar la única instancia de la clase Database
    private static Database? _instance;

    // Propiedad que devuelve la instancia única de la clase Database
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

    // Conexión SQLite
    private readonly SqliteConnection _connection;

    // Constructor privado para evitar instancias externas
    private Database()
    {
        // Inicializa la conexión
        _connection = new SqliteConnection("Data Source=miBaseDeDatos.db");
        _connection.Open();
    }

    // Método para obtener la conexión
    public SqliteConnection GetConnection()
    {
        return _connection;
    }

    // Método para cerrar la conexión si es necesario
    public void CloseConnection()
    {
        if (_connection.State == System.Data.ConnectionState.Open)
        {
            _connection.Close();
        }
    }
}
