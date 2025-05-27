using DataHorseman.Application.Dtos;
using DataHorseman.Domain.Entidades;

namespace DataHorseman.Application.Interfaces;

public interface IEnderecoService:IServiceBase<EnderecoDto>
{
    Endereco? ObtemEnderecoPorIdPessoa(int idPessoa);
}
