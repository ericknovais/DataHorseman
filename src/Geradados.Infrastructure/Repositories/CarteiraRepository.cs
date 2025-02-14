using Geradados.Infrastructure.Persistence;
using GeraDados.Domain.Entities;
using GeraDados.Domain.Repositories;

namespace Geradados.Infrastructure.Intercafes
{
    public class CarteiraRepository : RepositoryBase<Carteira>, ICarteiraRepository
    {
        ContextoDataBase ctx = new ContextoDataBase();
        public CarteiraRepository(ContextoDataBase contexto) : base(contexto)
        {
            ctx = contexto;
        }

        public new void Salvar(Carteira carteira)
        {
            if (carteira.ID.Equals(0) && carteira.Cota > 0)
                ctx.Set<Carteira>().Add(carteira);
        }
    }
}