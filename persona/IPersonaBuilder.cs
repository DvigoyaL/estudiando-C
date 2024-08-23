public interface IPersonaBuilder
{
    IPersonaBuilder ConId(int id);
    IPersonaBuilder ConNombre(string nombre);
    IPersonaBuilder ConEdad(int edad);
    Persona Build();
}

