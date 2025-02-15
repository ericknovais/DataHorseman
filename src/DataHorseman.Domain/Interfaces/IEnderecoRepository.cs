using DataHorseman.Domain.Entities;

namespace DataHorseman.Domain.Repositories;

public interface IEnderecoRepository: IRepositoryBase<Endereco>
{
    Endereco? ObtemEnderecoPorIdPessoa(int idPessoa);
}
