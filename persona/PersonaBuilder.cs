public class PersonaBuilder : IPersonaBuilder
{
    private Persona _persona;

    public PersonaBuilder()
    {
        _persona = new Persona();
    }

    public IPersonaBuilder ConId(int id)
    {
        _persona.Id = id;
        return this;
    }

    public IPersonaBuilder ConNombre(string nombre)
    {
        _persona.Name = nombre;
        return this;
    }

    public IPersonaBuilder ConEdad(int edad)
    {
        _persona.Age = edad;
        return this;
    }

    public Persona Build()
    {
        return _persona;
    }
}
