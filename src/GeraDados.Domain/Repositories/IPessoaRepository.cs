using GeraDados.DataModel.Models;

namespace GeraDados.DataModel.Repositories;

public interface IPessoaRepository: IRepositoryBase<Pessoa>
{
    Pessoa? ObtemPessoaPorCPF(string cpf);
}
