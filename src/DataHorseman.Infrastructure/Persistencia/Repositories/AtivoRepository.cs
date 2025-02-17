using DataHorseman.Domain.Entidades;
using DataHorseman.Domain.Enums;
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

        public List<Ativo> ObtemAtivosPorTipoDeAtivo(eTipoDeAtivo tipoDeAtivoID)
        {
#pragma warning disable  // Dereference of a possibly null reference.
            return ctx.Ativos.Where(ativo => ativo.TipoDeAtivoId == (int)tipoDeAtivoID).ToList();
        }
    }
}
