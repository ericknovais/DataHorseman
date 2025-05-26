using DataHorseman.Domain.Entidades;
using DataHorseman.Domain.Enums;

namespace DataHorseman.Domain.Interfaces;

public interface IAtivoRepository : IRepositoryBase<Ativo>
{
   Task<List<Ativo>> ObtemAtivosPorTipoDeAtivoIDAsync(eTipoDeAtivo tipoDeAtivoID);
}