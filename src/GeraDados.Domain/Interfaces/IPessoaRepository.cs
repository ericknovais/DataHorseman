using GeraDados.Domain.Entities;

namespace GeraDados.Domain.Repositories;

public interface IPessoaRepository: IRepositoryBase<Pessoa>
{
    Pessoa? ObtemPessoaPorCPF(string cpf);
}
