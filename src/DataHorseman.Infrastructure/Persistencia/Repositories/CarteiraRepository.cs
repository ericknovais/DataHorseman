using DataHorseman.Domain.Entidades;
using DataHorseman.Domain.Interfaces;
using DataHorseman.Infrastructure.Persistencia.DataContext;

namespace DataHorseman.Infrastructure.Persistencia.Repositories
{
    public class CarteiraRepository : RepositoryBase<Carteira>, ICarteiraRepository
    {
       protected readonly DataHorsemanDbContext _contexto = new DataHorsemanDbContext();
        public CarteiraRepository(DataHorsemanDbContext contexto) : base(contexto)
        {
            _contexto = contexto;
        }

        public new void CriarNovoAsync(Carteira carteira)
        {
            if (carteira.ID == 0 && carteira.Cota > 0)
                _contexto.Set<Carteira>().Add(carteira);
        }
    }
}