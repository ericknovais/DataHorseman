using DataHorseman.Domain.Entities;

namespace DataHorseman.Domain.Repositories;

public interface IAtivoRepository : IRepositoryBase<Ativo>
{
    List<Ativo> ObtemAtivosPorTipoDeAtivo(TipoDeAtivo? tipoDeAtivo);
}