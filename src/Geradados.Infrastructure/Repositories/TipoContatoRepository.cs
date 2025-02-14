using Geradados.Infrastructure.Persistence;
using GeraDados.Domain.Entities;
using GeraDados.Domain.Repositories;

namespace Geradados.Infrastructure.Intercafes;

public class TipoContatoRepository : RepositoryBase<TipoContato>, ITipoContatoRepository
{
    ContextoDataBase ctx = new ContextoDataBase();
    public TipoContatoRepository(ContextoDataBase contexto) : base(contexto)
    {
        ctx = contexto;
    }
}
