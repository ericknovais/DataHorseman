using DataHorseman.Domain.Entities;
using DataHorseman.Domain.Repositories;
using DataHorseman.Infrastructure.Persistence;

namespace DataHorseman.Infrastructure.Intercafes;

public class TipoDeAtivoRepository : RepositoryBase<TipoDeAtivo>, ITipoDeAtivoRepository
{
    ContextoDataBase ctx = new ContextoDataBase();
    public TipoDeAtivoRepository(ContextoDataBase contexto) : base(contexto)
    {
        ctx = contexto;
    }
}