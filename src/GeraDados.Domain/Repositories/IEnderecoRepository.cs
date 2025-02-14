using GeraDados.DataModel.Models;

namespace GeraDados.DataModel.Repositories;

public interface IEnderecoRepository: IRepositoryBase<Endereco>
{
    Endereco? ObtemEnderecoPorIdPessoa(int idPessoa);
}
