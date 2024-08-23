public class EmpleadoFactory : PersonaFactory
{
    public override Persona CrearPersona()
    {
        return new Empleado();
    }
}