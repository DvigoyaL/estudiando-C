namespace Entrega_2.Dominio
{
    public class Empleado : Persona
    {
        public string Cargo { get; set; }
        public double Sueldo { get; set; }
        public override string ToString()
        {
            return $"{base.ToString()}, Cargo: {Cargo}, Sueldo: {Sueldo}";
        }
    }

}
