using DataHorseman.Application.Dtos;
using DataHorseman.Domain.Entidades;
using DataHorseman.Domain.Enums;

namespace DataHorseman.Application.Interfaces;

public interface IAtivoService : IServiceBase<AtivoDto>
{
    List<AtivoDto> ObtemAtivosPorTipoDeAtivoID(eTipoDeAtivo tipoDeAtivoID);
}