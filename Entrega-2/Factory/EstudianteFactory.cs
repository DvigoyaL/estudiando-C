using Entrega_2.Builder;
using Entrega_2.Dominio;

namespace Entrega_2.Factory
{
    public class EstudianteFactory : IPersonaFactory
    {
        public IPersonaBuilder CreateBuilder()
        {
            return new EstudianteBuilder();
        }
    }

}
