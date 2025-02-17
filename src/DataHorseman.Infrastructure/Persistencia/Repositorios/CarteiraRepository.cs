using DataHorseman.Domain.Entidades;
using DataHorseman.Domain.Interfaces;
using DataHorseman.Infrastructure.Persistencia.DataContext;

namespace DataHorseman.Infrastructure.Persistencia.Repositorios
{
    public class CarteiraRepository : RepositoryBase<Carteira>, ICarteiraRepository
    {
        DataHorsemanDbContext ctx = new DataHorsemanDbContext();
        public CarteiraRepository(DataHorsemanDbContext contexto) : base(contexto)
        {
            ctx = contexto;
        }

        public new void CriarNovoAsync(Carteira carteira)
        {
            if (carteira.ID.Equals(0) && carteira.Cota > 0)
                ctx.Set<Carteira>().Add(carteira);
        }
    }
}