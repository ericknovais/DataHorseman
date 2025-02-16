using DataHorseman.Domain.Entidades;

namespace DataHorseman.Domain.Interfaces;

public interface IEnderecoRepository: IRepositoryBase<Endereco>
{
    Endereco? ObtemEnderecoPorIdPessoa(int idPessoa);
}
