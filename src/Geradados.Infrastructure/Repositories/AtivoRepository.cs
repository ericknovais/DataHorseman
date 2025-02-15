using Geradados.Infrastructure.Persistence;
using GeraDados.Domain.Entities;
using GeraDados.Domain.Repositories;

namespace Geradados.Infrastructure.Intercafes
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
            return ctx.Ativos.Where(ativo => ativo.TipoDeAtivoId.Equals(tipoDeAtivo.ID)).ToList();
        }
    }
}
