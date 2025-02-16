using DataHorseman.Domain.Entidades;

namespace DataHorseman.Domain.Interfaces;

public interface IAtivoRepository : IRepositoryBase<Ativo>
{
    List<Ativo> ObtemAtivosPorTipoDeAtivo(TipoDeAtivo? tipoDeAtivo);
}