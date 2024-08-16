using System.Data.SQLite;

public class Database
{
    private static Database? _instance;
    private readonly SQLiteConnection _connection;

    private Database()
    {
        _connection = new SQLiteConnection("Data Source=miBaseDeDatos.db");
        _connection.Open();
        CrearTablas();
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

    public SQLiteConnection GetConnection()
    {
        return _connection;
    }

    private void CrearTablas()
    {
        using (var command = _connection.CreateCommand())
        {
            command.CommandText = @"
                CREATE TABLE IF NOT EXISTS Personas (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Nombre TEXT NOT NULL,
                    Edad INTEGER NOT NULL
                );

                CREATE TABLE IF NOT EXISTS Empleados (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Nombre TEXT NOT NULL,
                    Edad INTEGER NOT NULL,
                    Cargo TEXT NOT NULL,
                    Sueldo REAL NOT NULL,
                    FOREIGN KEY(Id) REFERENCES Personas(Id)
                );

                CREATE TABLE IF NOT EXISTS Estudiantes (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Nombre TEXT NOT NULL,
                    Edad INTEGER NOT NULL,
                    Curso TEXT NOT NULL,
                    Colegio TEXT NOT NULL,
                    FOREIGN KEY(Id) REFERENCES Personas(Id)
                );
                CREATE TABLE IF NOT EXISTS Archivos (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    NombreArchivo TEXT NOT NULL,
                    TipoArchivo TEXT NOT NULL,
                    Archivo BLOB NOT NULL
                );
            ";
            command.ExecuteNonQuery();
        }
    }
}

// Método para cerrar la conexión si es necesario
/*public void CloseConnection()
{
    if (_connection.State == System.Data.ConnectionState.Open)
    {
        _connection.Close();
    }
}*/