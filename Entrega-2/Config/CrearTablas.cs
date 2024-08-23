using System.Data.SQLite;

namespace Entrega_2.Config
{
    public class CrearTablas
    {
        private readonly SQLiteConnection _connection;

        public CrearTablas(IConexionBd conexionBd)
        {
            _connection = conexionBd.GetConnection();
        }

        public void Tablas()
        {
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = @"
                CREATE TABLE IF NOT EXISTS Persona (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Nombre TEXT NOT NULL,
                    Edad INTEGER NOT NULL
                );
                
                CREATE TABLE IF NOT EXISTS Empleado (
                    Id INTEGER PRIMARY KEY,
                    Cargo TEXT NOT NULL,
                    Sueldo REAL NOT NULL,
                    FOREIGN KEY(Id) REFERENCES Persona(Id)
                );
                
                CREATE TABLE IF NOT EXISTS Estudiante (
                    Id INTEGER PRIMARY KEY,
                    Curso TEXT NOT NULL,
                    FOREIGN KEY(Id) REFERENCES Persona(Id)
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
}
