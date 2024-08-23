using System;
using System.Collections.Generic;

using System;

// Define the Persona class and its derived classes
public class Persona
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public int Edad { get; set; }
}

public class Empleado : Persona
{
    public string Cargo { get; set; }
    public decimal Sueldo { get; set; }
}

public class Estudiante : Persona
{
    public string Carrera { get; set; }
}

// Define the Builder interface
public interface IPersonaBuilder
{
    void SetBasicInfo(int id, string nombre, int edad);
    void SetEmpleadoDetails(string cargo, decimal sueldo);
    void SetEstudianteDetails(string carrera);
    Persona GetPersona();
}

// Implement a Concrete Builder for Empleado
public class EmpleadoBuilder : IPersonaBuilder
{
    private Empleado _empleado = new Empleado();

    public void SetBasicInfo(int id, string nombre, int edad)
    {
        _empleado.Id = id;
        _empleado.Nombre = nombre;
        _empleado.Edad = edad;
    }

    public void SetEmpleadoDetails(string cargo, decimal sueldo)
    {
        _empleado.Cargo = cargo;
        _empleado.Sueldo = sueldo;
    }

    public void SetEstudianteDetails(string carrera)
    {
        throw new NotImplementedException("EmpleadoBuilder cannot set Estudiante details.");
    }

    public Persona GetPersona()
    {
        return _empleado;
    }
}

// Implement a Concrete Builder for Estudiante
public class EstudianteBuilder : IPersonaBuilder
{
    private Estudiante _estudiante = new Estudiante();

    public void SetBasicInfo(int id, string nombre, int edad)
    {
        _estudiante.Id = id;
        _estudiante.Nombre = nombre;
        _estudiante.Edad = edad;
    }

    public void SetEmpleadoDetails(string cargo, decimal sueldo)
    {
        throw new NotImplementedException("EstudianteBuilder cannot set Empleado details.");
    }

    public void SetEstudianteDetails(string carrera)
    {
        _estudiante.Carrera = carrera;
    }

    public Persona GetPersona()
    {
        return _estudiante;
    }
}

// Define the Factory interface
public interface IPersonaFactory
{
    IPersonaBuilder CreateBuilder();
}

// Implement Concrete Factories
public class EmpleadoFactory : IPersonaFactory
{
    public IPersonaBuilder CreateBuilder()
    {
        return new EmpleadoBuilder();
    }
}

public class EstudianteFactory : IPersonaFactory
{
    public IPersonaBuilder CreateBuilder()
    {
        return new EstudianteBuilder();
    }
}

// Director class to manage the building process
public class PersonaDirector
{
    private IPersonaBuilder _builder;

    public IPersonaBuilder Builder
    {
        set { _builder = value; }
    }

    public void BuildEmpleado(int id, string nombre, int edad, string cargo, decimal sueldo)
    {
        _builder.SetBasicInfo(id, nombre, edad);
        _builder.SetEmpleadoDetails(cargo, sueldo);
    }

    public void BuildEstudiante(int id, string nombre, int edad, string carrera)
    {
        _builder.SetBasicInfo(id, nombre, edad);
        _builder.SetEstudianteDetails(carrera);
    }
}

// Usage
class Program
{
    static void Main(string[] args)
    {
        var director = new PersonaDirector();

        // Use the Factory to create an Empleado
        IPersonaFactory empleadoFactory = new EmpleadoFactory();
        IPersonaBuilder empleadoBuilder = empleadoFactory.CreateBuilder();
        director.Builder = empleadoBuilder;
        director.BuildEmpleado(1, "John Doe", 30, "Developer", 50000m);
        Empleado empleado = (Empleado)empleadoBuilder.GetPersona();
        Console.WriteLine($"Empleado: Id={empleado.Id}, Nombre={empleado.Nombre}, Edad={empleado.Edad}, Cargo={empleado.Cargo}, Sueldo={empleado.Sueldo}");

        // Use the Factory to create an Estudiante
        IPersonaFactory estudianteFactory = new EstudianteFactory();
        IPersonaBuilder estudianteBuilder = estudianteFactory.CreateBuilder();
        director.Builder = estudianteBuilder;
        director.BuildEstudiante(2, "Jane Smith", 22, "Computer Science");
        Estudiante estudiante = (Estudiante)estudianteBuilder.GetPersona();
        Console.WriteLine($"Estudiante: Id={estudiante.Id}, Nombre={estudiante.Nombre}, Edad={estudiante.Edad}, Carrera={estudiante.Carrera}");
    }
}
