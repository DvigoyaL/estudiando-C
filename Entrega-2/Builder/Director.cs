namespace Entrega_2.Builder
{
    public class PersonaDirector
    {
        private IPersonaBuilder _builder;

        public IPersonaBuilder Builder
        {
            set { _builder = value; }
        }

        public void BuildEmpleado(int id, string nombre, int edad, string cargo, double sueldo)
        {
            _builder.BuildBasicInfo(id, nombre, edad);
            _builder.BuildEmpleadoDetails(cargo, sueldo);
        }

        public void BuildEstudiante(int id, string nombre, int edad, string carrera)
        {
            _builder.BuildBasicInfo(id, nombre, edad);
            _builder.BuildEstudianteDetails(carrera);
        }
    }
}
