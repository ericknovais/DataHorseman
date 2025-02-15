using DataHorseman.Domain.Entities;
using DataHorseman.Domain.Repositories;
using DataHorseman.Infrastructure.Persistence;

namespace DataHorseman.Infrastructure.Intercafes;

public class TipoContatoRepository : RepositoryBase<TipoContato>, ITipoContatoRepository
{
    ContextoDataBase ctx = new ContextoDataBase();
    public TipoContatoRepository(ContextoDataBase contexto) : base(contexto)
    {
        ctx = contexto;
    }
}