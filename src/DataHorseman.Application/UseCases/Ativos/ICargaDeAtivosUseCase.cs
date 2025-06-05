using DataHorseman.Domain.Enums;

namespace DataHorseman.Application.UseCases.Ativos;

public interface ICargaDeAtivosUseCase
{
    Task ExecutarAsync(eTipoDeAtivo tipoDeAtivo);
}
