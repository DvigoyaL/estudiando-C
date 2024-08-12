public class Empleado: Persona
{
	private string cargo;
	private double sueldo;

    public Empleado(int id, string nombre, int age, string cargo, double sueldo):base(id, nombre, age)
    {
        this.cargo = cargo;
        this.sueldo = sueldo;
    }

    public void mostrarInfo()
    {
        Console.WriteLine($"Nombre {Name} sueldo del empleado {sueldo} y cargo {cargo} ");
    }
}
