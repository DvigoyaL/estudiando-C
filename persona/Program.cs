// See https://aka.ms/new-console-template for more information
using Microsoft.Data.Sqlite;

Console.WriteLine("Hello, World!");
Empleado empleado = new Empleado(1, "Jorge", 12, "cajero", 700000);
empleado.mostrarInfo();


        // Ruta a la base de datos (se crea automáticamente si no existe)
        string connectionString = "Data Source=miBaseDeDatos.db";

        // Crear la conexión
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();

            // Crear una tabla
            var command = connection.CreateCommand();
            command.CommandText = @"
                CREATE TABLE IF NOT EXISTS Personas (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Nombre TEXT NOT NULL,
                    Edad INTEGER NOT NULL
                );
            ";
            command.ExecuteNonQuery();
        }

        Console.WriteLine("Tabla creada exitosamente.");
  