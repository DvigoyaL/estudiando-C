using Entrega_2.Dominio;

namespace Entrega_2.Builder
{
    public class EstudianteBuilder : IPersonaBuilder
    {
        private Estudiante _estudiante = new Estudiante();

        public void BuildBasicInfo(int id, string nombre, int edad)
        {
            // Set basic info for Persona
            _estudiante.Id = id;
            _estudiante.Nombre = nombre;
            _estudiante.Edad = edad;
        }

        public void BuildEmpleadoDetails(string cargo, double sueldo)
        {
            throw new NotImplementedException("EstudianteBuilder cannot build Empleado details.");
        }

        public void BuildEstudianteDetails(string curso)
        {
            // Set details specific to Estudiante
            _estudiante.Curso = curso;
        }

        public Persona GetPersona()
        {
            return _estudiante;
        }
    }

}
