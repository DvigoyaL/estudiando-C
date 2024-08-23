using Entrega_2.Builder;
using Entrega_2.Dominio;
using Entrega_2.Factory;
using Entrega_2.Config;
using System.Data.SQLite;
using Entrega_2.DAO;

var director = new PersonaDirector();
IPersonaFactory empleadoFactory = new EmpleadoFactory();
IPersonaBuilder empleadoBuilder = empleadoFactory.CreateBuilder();
director.Builder = empleadoBuilder;
director.BuildEmpleado(1, "John Doe", 30, "Developer", 50000);
Empleado empleado = (Empleado)empleadoBuilder.GetPersona();

// Use the Factory to create an Estudiante
IPersonaFactory estudianteFactory = new EstudianteFactory();
IPersonaBuilder estudianteBuilder = estudianteFactory.CreateBuilder();
director.Builder = estudianteBuilder;
director.BuildEstudiante(2, "Jane Smith", 22, "Computer Science");
Estudiante estudiante = (Estudiante)estudianteBuilder.GetPersona();

IConexionBd connection = Database.Instance;
CrearTablas crearDb = new CrearTablas(connection);
crearDb.Tablas();
var empleadoDAO = new EmpleadoDAO(connection);
var estudianteDAO = new EstudianteDAO(connection);
var archivoDAO = new ArchivoBinDAO(connection);
empleadoDAO.CrearEmpleado(empleado);
estudianteDAO.CrearEstudiante(estudiante);

/*empleadoDAO.EliminarEmpleado(1);
empleadoDAO.EliminarEmpleado(2);*/
List<Empleado> empleados = empleadoDAO.LeerTodosLosEmpleados();
List<Estudiante> estudiantes = estudianteDAO.LeerTodosLosEstudiantes();
foreach (var Empleado in empleados)
{
    Console.WriteLine(Empleado);
}
foreach (var Estudiante in estudiantes)
{
    Console.WriteLine(Estudiante);
}

archivoDAO.GuardarArchivo(@"C:\Users\DAVID\source\repos\aplicaciones-base\Entrega-2\ArchivosBin\prueba.pdf");

var archivos = archivoDAO.ListarArchivos();
foreach (var archivo in archivos)
{
    Console.WriteLine($"Id: {archivo.Id}, Nombre: {archivo.NombreArchivo}");
}

archivoDAO.LeerArchivo(1, @"C:\Users\DAVID\source\repos\aplicaciones-base\Entrega-2\ArchivosBin\ArchivosRecuperadosBd\");
