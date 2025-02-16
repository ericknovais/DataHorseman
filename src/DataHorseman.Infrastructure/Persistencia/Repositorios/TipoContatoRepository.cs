using DataHorseman.Domain.Entidades;
using DataHorseman.Domain.Interfaces;
using DataHorseman.Infrastructure.Persistencia.DataContext;

namespace DataHorseman.Infrastructure.Persistencia.Repositorios;

public class TipoContatoRepository : RepositoryBase<TipoContato>, ITipoContatoRepository
{
    DataHorsemanDbContext ctx = new DataHorsemanDbContext();
    public TipoContatoRepository(DataHorsemanDbContext contexto) : base(contexto)
    {
        ctx = contexto;
    }
}