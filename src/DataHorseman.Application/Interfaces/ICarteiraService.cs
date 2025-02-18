using DataHorseman.Application.Dtos;

namespace DataHorseman.Application.Interfaces;

public interface ICarteiraService: IServiceBase<CarteiraDto>
{
    Task CriarNovasCarteirasLote(CarteiraDto entidade);
}