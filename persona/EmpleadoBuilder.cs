public class EmpleadoBuilder : PersonaBuilder
{
    private Empleado _empleado;

    public EmpleadoBuilder() : base()
    {
        _empleado = new Empleado();
    }

    public EmpleadoBuilder ConCargo(string cargo)
    {
        _empleado.Cargo = cargo;
        return this;
    }

    public EmpleadoBuilder ConSueldo(double sueldo)
    {
        _empleado.Sueldo = sueldo;
        return this;
    }

    public new Empleado Build()
    {
        return _empleado;
    }
}
