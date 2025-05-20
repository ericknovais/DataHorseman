using DataHorseman.Domain.Entidades;
using DataHorseman.Domain.Interfaces;
using DataHorseman.Infrastructure.Persistencia.DataContext;

namespace DataHorseman.Infrastructure.Persistencia.Repositories;

public class TipoDeAtivoRepository : RepositoryBase<TipoDeAtivo>, ITipoDeAtivoRepository
{
    DataHorsemanDbContext ctx = new DataHorsemanDbContext();
    public TipoDeAtivoRepository(DataHorsemanDbContext contexto) : base(contexto)
    {
        ctx = contexto;
    }
}