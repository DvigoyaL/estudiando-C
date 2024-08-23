using Entrega_2.Builder;
using Entrega_2.Config;
using Entrega_2.Dominio;
using Entrega_2.Factory;
using System.Data.SQLite;


namespace Entrega_2.DAO
{
    public class EmpleadoDAO
    {
        private readonly SQLiteConnection _connection;
              
        public EmpleadoDAO(IConexionBd conexionBd)
        {
            _connection = conexionBd.GetConnection();
        }

        // Crear un nuevo empleado en la base de datos
        public void CrearEmpleado(Empleado empleado)
        {
            using (var transaction = _connection.BeginTransaction())
            {
                try
                {
                    // Insertar primero en la tabla Persona
                    using (var commandPersona = _connection.CreateCommand())
                    {
                        commandPersona.CommandText = @"
                    INSERT INTO Persona (Nombre, Edad)
                    VALUES ($nombre, $edad);
                    SELECT last_insert_rowid();"; // Esta línea recupera el último ID insertado

                        commandPersona.Parameters.AddWithValue("$nombre", empleado.Nombre);
                        commandPersona.Parameters.AddWithValue("$edad", empleado.Edad);

                        // Ejecutar el comando y obtener el Id generado
                        var personaId = (long)commandPersona.ExecuteScalar(); // last_insert_rowid() devuelve un long

                        // Insertar después en la tabla Empleado
                        using (var commandEmpleado = _connection.CreateCommand())
                        {
                            commandEmpleado.CommandText = @"
                        INSERT INTO Empleado (Id, Cargo, Sueldo)
                        VALUES ($id, $cargo, $sueldo)";

                            commandEmpleado.Parameters.AddWithValue("$id", personaId); // Usar el Id de Persona como Id de Empleado
                            commandEmpleado.Parameters.AddWithValue("$cargo", empleado.Cargo);
                            commandEmpleado.Parameters.AddWithValue("$sueldo", empleado.Sueldo);

                            commandEmpleado.ExecuteNonQuery();
                        }
                    }

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        // Leer un empleado por su ID
        public Empleado? LeerEmpleadoPorId(int id)
        {
            PersonaDirector director = new PersonaDirector();
            IPersonaFactory empleadoFactory = new EmpleadoFactory();
            IPersonaBuilder empleadoBuilder = empleadoFactory.CreateBuilder();
            director.Builder = empleadoBuilder;
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = @"
                SELECT e.Id AS EmpleadoId, p.Nombre, p.Edad, e.Cargo, e.Sueldo FROM Empleado e JOIN Persona p ON e.Id = p.Id
                WHERE Id = $id";

                command.Parameters.AddWithValue("$id", id);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        director.BuildEmpleado(reader.GetInt32(0),    // Id
                                               reader.GetString(1),   // Nombre
                                               reader.GetInt32(2),    // Edad
                                               reader.GetString(3),   // Cargo
                                               reader.GetDouble(4));  // Sueldo

                        return (Empleado)empleadoBuilder.GetPersona();
                    }
                }
            }
            Console.Error.WriteLine("Empleado no encontrado");
            return null;
        }

        // Leer todos los empleados
        public List<Empleado> LeerTodosLosEmpleados()
        {
            PersonaDirector director = new PersonaDirector();
            IPersonaFactory empleadoFactory = new EmpleadoFactory();
            IPersonaBuilder empleadoBuilder = empleadoFactory.CreateBuilder();
            director.Builder = empleadoBuilder;
            var empleados = new List<Empleado>();

            using (var command = _connection.CreateCommand())
            {
                command.CommandText = "SELECT e.Id AS EmpleadoId, p.Nombre, p.Edad, e.Cargo, e.Sueldo FROM Empleado e JOIN Persona p ON e.Id = p.Id;"; 

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        director.BuildEmpleado(reader.GetInt32(0),    // Id
                                               reader.GetString(1),   // Nombre
                                               reader.GetInt32(2),    // Edad
                                               reader.GetString(3),   // Cargo
                                               reader.GetDouble(4));  // Sueldo

                        empleados.Add((Empleado)empleadoBuilder.GetPersona());
                    }
                }
            }

            return empleados;
        }


        // Actualizar la información de un empleado existente
        public void ActualizarEmpleado(Empleado empleado)
        {
            using (var transaction = _connection.BeginTransaction())
            {
                try
                {
                    // Actualizar primero en la tabla Persona
                    using (var commandPersona = _connection.CreateCommand())
                    {
                        commandPersona.CommandText = @"
                    UPDATE Persona
                    SET Nombre = $nombre, Edad = $edad
                    WHERE Id = $id";

                        commandPersona.Parameters.AddWithValue("$nombre", empleado.Nombre);
                        commandPersona.Parameters.AddWithValue("$edad", empleado.Edad);
                        commandPersona.Parameters.AddWithValue("$id", empleado.Id);

                        commandPersona.ExecuteNonQuery();
                    }

                    // Actualizar en la tabla Empleado
                    using (var commandEmpleado = _connection.CreateCommand())
                    {
                        commandEmpleado.CommandText = @"
                    UPDATE Empleado
                    SET Cargo = $cargo, Sueldo = $sueldo
                    WHERE Id = $id";

                        commandEmpleado.Parameters.AddWithValue("$cargo", empleado.Cargo);
                        commandEmpleado.Parameters.AddWithValue("$sueldo", empleado.Sueldo);
                        commandEmpleado.Parameters.AddWithValue("$id", empleado.Id);

                        commandEmpleado.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }


        // Eliminar un empleado por su ID
        public void EliminarEmpleado(int id)
        {
            using (var transaction = _connection.BeginTransaction())
            {
                try
                {
                    // Eliminar primero en la tabla Empleado
                    using (var commandEmpleado = _connection.CreateCommand())
                    {
                        commandEmpleado.CommandText = "DELETE FROM Empleado WHERE Id = $id";
                        commandEmpleado.Parameters.AddWithValue("$id", id);

                        commandEmpleado.ExecuteNonQuery();
                    }

                    // Eliminar en la tabla Persona
                    using (var commandPersona = _connection.CreateCommand())
                    {
                        commandPersona.CommandText = "DELETE FROM Persona WHERE Id = $id";
                        commandPersona.Parameters.AddWithValue("$id", id);

                        commandPersona.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

    }
}
