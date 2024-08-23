using Entrega_2.Dominio;

namespace Entrega_2.Builder
{
    public interface IPersonaBuilder
    {
        void BuildBasicInfo(int id, string nombre, int edad);
        void BuildEmpleadoDetails(string cargo, double sueldo);
        void BuildEstudianteDetails(string curso);
        Persona GetPersona();
    }

}
