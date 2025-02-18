using DataHorseman.Domain.Entidades;

namespace DataHorseman.Application.Interfaces;

public interface IEnderecoService:IServiceBase<Endereco>
{
    Endereco? ObtemEnderecoPorIdPessoa(int idPessoa);
}
