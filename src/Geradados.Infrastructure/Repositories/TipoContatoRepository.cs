using Geradados.DataAccess.DB;
using GeraDados.DataModel.Models;
using GeraDados.DataModel.Repositories;

namespace Geradados.DataAccess.Repositories;

public class TipoContatoRepository : RepositoryBase<TipoContato>, ITipoContatoRepository
{
    ContextoDataBase ctx = new ContextoDataBase();
    public TipoContatoRepository(ContextoDataBase contexto) : base(contexto)
    {
        ctx = contexto;
    }
}
