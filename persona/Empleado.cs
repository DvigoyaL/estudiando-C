public class Empleado: Persona
{
	private string cargo;
	private double sueldo;

    public Empleado(int id, string nombre, int age, string cargo, double sueldo):base(id, nombre, age)
    {
        this.cargo = cargo;
        this.sueldo = sueldo;
    }

    public string Cargo
    {
        set { this.cargo = value; }
        get { return cargo; }
    }

    public double Sueldo
    {
        set { sueldo = value; }
        get { return sueldo; }
    }

    public string Nombre
    {
        get { return Name; }
    }

    public int Edad
    {
        get { return Age; }
    }

    public void mostrarInfo()
    {
        Console.WriteLine($"Nombre {Name} numero {Id} sueldo del empleado {sueldo} y cargo {cargo} ");
    }
}
