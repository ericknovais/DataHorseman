using DataHorseman.Domain.Entidades;
using DataHorseman.Domain.Interfaces;
using DataHorseman.Infrastructure.Persistencia.DataContext;

namespace DataHorseman.Infrastructure.Persistencia.Repositories
{
    public class AtivoRepository : RepositoryBase<Ativo>, IAtivoRepository
    {
        DataHorsemanDbContext ctx = new DataHorsemanDbContext();
        public AtivoRepository(DataHorsemanDbContext contexto) : base(contexto)
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
