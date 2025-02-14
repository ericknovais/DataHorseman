using Geradados.DataAccess.DB;
using GeraDados.DataModel.Models;
using GeraDados.DataModel.Repositories;

namespace Geradados.DataAccess.Repositories
{
    public class AtivoRepository : RepositoryBase<Ativo>, IAtivoRepository
    {
        ContextoDataBase ctx = new ContextoDataBase();
        public AtivoRepository(ContextoDataBase contexto) : base(contexto)
        {
            ctx = contexto;
        }

        public List<Ativo> ObtemAtivosPorTipoDeAtivo(TipoDeAtivo? tipoDeAtivo)
        {
#pragma warning disable  // Dereference of a possibly null reference.
            return ctx.Ativos.Where(ativo => ativo.TipoDeAtivo.ID.Equals(tipoDeAtivo.ID)).ToList();
        }
    }
}
