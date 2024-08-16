using System.Data.SQLite;
using System.Collections.Generic;

public class EmpleadoDAO
{
    private readonly SQLiteConnection _connection;

    // Constructor que obtiene la conexión desde la clase Database (Singleton)
    public EmpleadoDAO(SQLiteConnection connection)
    {
        _connection = connection;
    }

    // Crear un nuevo empleado en la base de datos
    public void CrearEmpleado(Empleado empleado)
    {
        using (var command = _connection.CreateCommand())
        {
            command.CommandText = @"
                INSERT INTO Empleados (Nombre, Edad, Cargo, Sueldo)
                VALUES ($nombre, $edad, $cargo, $sueldo)";

            command.Parameters.AddWithValue("$nombre", empleado.Name);
            command.Parameters.AddWithValue("$edad", empleado.Age);
            command.Parameters.AddWithValue("$cargo", empleado.Cargo);
            command.Parameters.AddWithValue("$sueldo", empleado.Sueldo);

            command.ExecuteNonQuery();
        }
    }

    // Leer un empleado por su ID
    public Empleado? LeerEmpleadoPorId(int id)
    {
        using (var command = _connection.CreateCommand())
        {
            command.CommandText = @"
                SELECT Id, Nombre, Edad, Cargo, Sueldo
                FROM Empleados
                WHERE Id = $id";

            command.Parameters.AddWithValue("$id", id);

            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new Empleado(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetInt32(2),
                        reader.GetString(3),
                        reader.GetDouble(4)
                    );
                }
            }
        }
        return null; 
    }

    // Leer todos los empleados
    public List<Empleado> LeerTodosLosEmpleados()
    {
        var empleados = new List<Empleado>();

        using (var command = _connection.CreateCommand())
        {
            command.CommandText = "SELECT Id, Nombre, Edad, Cargo, Sueldo FROM Empleados";

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    empleados.Add(new Empleado(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetInt32(2),
                        reader.GetString(3),
                        reader.GetDouble(4)
                    ));
                }
            }
        }

        return empleados;
    }

    // Actualizar la información de un empleado existente
    public void ActualizarEmpleado(Empleado empleado)
    {
        using (var command = _connection.CreateCommand())
        {
            command.CommandText = @"
                UPDATE Empleados
                SET Nombre = $nombre, Edad = $edad, Cargo = $cargo, Sueldo = $sueldo
                WHERE Id = $id";

            command.Parameters.AddWithValue("$nombre", empleado.Name);
            command.Parameters.AddWithValue("$edad", empleado.Age);
            command.Parameters.AddWithValue("$cargo", empleado.Cargo);
            command.Parameters.AddWithValue("$sueldo", empleado.Sueldo);
            command.Parameters.AddWithValue("$id", empleado.getId());

            command.ExecuteNonQuery();
        }
    }

    // Eliminar un empleado por su ID
    public void EliminarEmpleado(int id)
    {
        using (var command = _connection.CreateCommand())
        {
            command.CommandText = "DELETE FROM Empleados WHERE Id = $id";
            command.Parameters.AddWithValue("$id", id);

            command.ExecuteNonQuery();
        }
    }
}
