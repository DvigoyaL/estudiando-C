namespace Entrega_2.Dominio
{
    public class Estudiante : Persona
    {
        public string Curso { get; set; }

        public override string ToString()
        {
            return $"{base.ToString()}, Curso: {Curso}";
        }
    }

}
