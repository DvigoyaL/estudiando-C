using Entrega_2.Dominio;

namespace Entrega_2.Builder
{
    public class EmpleadoBuilder : IPersonaBuilder
    {
        private Empleado _empleado = new Empleado();

        public void BuildBasicInfo(int id, string nombre, int edad)
        {
            // Set basic info for Persona
            _empleado.Id = id;
            _empleado.Nombre = nombre;
            _empleado.Edad = edad;
        }

        public void BuildEmpleadoDetails(string cargo, double sueldo)
        {
            // Set details specific to Empleado
            _empleado.Cargo = cargo;
            _empleado.Sueldo = sueldo;
        }

        public void BuildEstudianteDetails(string carrera)
        {
            throw new NotImplementedException("EmpleadoBuilder cannot build Estudiante details.");
        }

        public Persona GetPersona()
        {
            return _empleado;
        }
    }

}
