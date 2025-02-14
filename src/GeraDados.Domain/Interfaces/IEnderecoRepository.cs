using GeraDados.Domain.Entities;

namespace GeraDados.Domain.Repositories;

public interface IEnderecoRepository: IRepositoryBase<Endereco>
{
    Endereco? ObtemEnderecoPorIdPessoa(int idPessoa);
}
