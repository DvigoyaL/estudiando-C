using Entrega_2.Builder;
using Entrega_2.Config;
using Entrega_2.Dominio;
using Entrega_2.Factory;
using System.Data.SQLite;

namespace Entrega_2.DAO
{
    public class EstudianteDAO
    {
        private readonly SQLiteConnection _connection;

        public EstudianteDAO(IConexionBd conexionBd)
        {
            _connection = conexionBd.GetConnection();
        }

        // Crear un nuevo estudiante en la base de datos
        public void CrearEstudiante(Estudiante estudiante)
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

                        commandPersona.Parameters.AddWithValue("$nombre", estudiante.Nombre);
                        commandPersona.Parameters.AddWithValue("$edad", estudiante.Edad);

                        // Ejecutar el comando y obtener el Id generado
                        var personaId = (long)commandPersona.ExecuteScalar(); // last_insert_rowid() devuelve un long

                        // Insertar después en la tabla Estudiante
                        using (var commandEstudiante = _connection.CreateCommand())
                        {
                            commandEstudiante.CommandText = @"
                        INSERT INTO Estudiante (Id, Curso)
                        VALUES ($id, $curso)";

                            commandEstudiante.Parameters.AddWithValue("$id", personaId); // Usar el Id de Persona como Id de Estudiante
                            commandEstudiante.Parameters.AddWithValue("$curso", estudiante.Curso);

                            commandEstudiante.ExecuteNonQuery();
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

        // Leer un estudiante por su ID
        public Estudiante? LeerEstudiantePorId(int id)
        {
            PersonaDirector director = new PersonaDirector();
            IPersonaFactory estudianteFactory = new EstudianteFactory();
            IPersonaBuilder estudianteBuilder = estudianteFactory.CreateBuilder();
            director.Builder = estudianteBuilder;
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = @"
                SELECT e.Id AS EstudianteId, p.Nombre, p.Edad, e.Curso FROM Estudiante e JOIN Persona p ON e.Id = p.Id
                WHERE e.Id = $id";

                command.Parameters.AddWithValue("$id", id);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        director.BuildEstudiante(reader.GetInt32(0),    // Id
                                                 reader.GetString(1),   // Nombre
                                                 reader.GetInt32(2),    // Edad
                                                 reader.GetString(3));  // Curso

                        return (Estudiante)estudianteBuilder.GetPersona();
                    }
                }
            }
            Console.Error.WriteLine("Estudiante no encontrado");
            return null;
        }

        // Leer todos los estudiantes
        public List<Estudiante> LeerTodosLosEstudiantes()
        {
            PersonaDirector director = new PersonaDirector();
            IPersonaFactory estudianteFactory = new EstudianteFactory();
            IPersonaBuilder estudianteBuilder = estudianteFactory.CreateBuilder();
            director.Builder = estudianteBuilder;
            var estudiantes = new List<Estudiante>();

            using (var command = _connection.CreateCommand())
            {
                command.CommandText = "SELECT e.Id AS EstudianteId, p.Nombre, p.Edad, e.Curso FROM Estudiante e JOIN Persona p ON e.Id = p.Id;";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        director.BuildEstudiante(reader.GetInt32(0),    // Id
                                                 reader.GetString(1),   // Nombre
                                                 reader.GetInt32(2),    // Edad
                                                 reader.GetString(3));  // Curso

                        estudiantes.Add((Estudiante)estudianteBuilder.GetPersona());
                    }
                }
            }

            return estudiantes;
        }

        // Actualizar la información de un estudiante existente
        public void ActualizarEstudiante(Estudiante estudiante)
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

                        commandPersona.Parameters.AddWithValue("$nombre", estudiante.Nombre);
                        commandPersona.Parameters.AddWithValue("$edad", estudiante.Edad);
                        commandPersona.Parameters.AddWithValue("$id", estudiante.Id);

                        commandPersona.ExecuteNonQuery();
                    }

                    // Actualizar en la tabla Estudiante
                    using (var commandEstudiante = _connection.CreateCommand())
                    {
                        commandEstudiante.CommandText = @"
                    UPDATE Estudiante
                    SET Curso = $curso
                    WHERE Id = $id";

                        commandEstudiante.Parameters.AddWithValue("$curso", estudiante.Curso);
                        commandEstudiante.Parameters.AddWithValue("$id", estudiante.Id);

                        commandEstudiante.ExecuteNonQuery();
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

        // Eliminar un estudiante por su ID
        public void EliminarEstudiante(int id)
        {
            using (var transaction = _connection.BeginTransaction())
            {
                try
                {
                    // Eliminar primero en la tabla Estudiante
                    using (var commandEstudiante = _connection.CreateCommand())
                    {
                        commandEstudiante.CommandText = "DELETE FROM Estudiante WHERE Id = $id";
                        commandEstudiante.Parameters.AddWithValue("$id", id);

                        commandEstudiante.ExecuteNonQuery();
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

