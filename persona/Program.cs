// See https://aka.ms/new-console-template for more information
using System.Data.SQLite;

// Crear la instancia de la conexión a la base de datos
SQLiteConnection connection = Database.Instance.GetConnection();

// Crear instancias de DAO
var empleadoDAO = new EmpleadoDAO(connection);
var archivoDAO = new ArchivoDAO(connection);
List<Empleado> empleados = empleadoDAO.LeerTodosLosEmpleados();

// Ejemplo: Crear y guardar un empleado
var nuevoEmpleado = new Empleado(1, "Jose Perez", 30, "Desarrollador", 45000);
empleadoDAO.CrearEmpleado(nuevoEmpleado);
empleados = empleadoDAO.LeerTodosLosEmpleados();

foreach (var empleado in empleados)
{
    empleado.mostrarInfo(); // Muestra la información del empleado en consola
}
// Ejemplo: Guardar un archivo binario
archivoDAO.GuardarArchivo(@"C:\Users\DAVID\source\repos\aplicaciones-base\persona\prueba.pdf"); 

// Ejemplo: Listar y recuperar archivos
var archivos = archivoDAO.ListarArchivos();
foreach (var archivo in archivos)
{
    Console.WriteLine($"Id: {archivo.Id}, Nombre: {archivo.NombreArchivo}");
}

// Ejemplo: Leer y guardar un archivo desde la base de datos
archivoDAO.LeerArchivo(1, @"C:\Users\DAVID\source\repos\aplicaciones-base\persona\lecturas.bd");

// Ejemplo: Actualizar un empleado existente
var empleadoExistente = empleadoDAO.LeerEmpleadoPorId(1);
if (empleadoExistente != null)
{
    empleadoExistente.Cargo = "Gerente";
    empleadoExistente.Sueldo = 60000;
    empleadoDAO.ActualizarEmpleado(empleadoExistente);
}

empleadoDAO.EliminarEmpleado(1);
empleadoDAO.EliminarEmpleado(3);
empleados = empleadoDAO.LeerTodosLosEmpleados();

foreach (var empleado in empleados)
{
    empleado.mostrarInfo(); // Muestra la información del empleado en consola
}