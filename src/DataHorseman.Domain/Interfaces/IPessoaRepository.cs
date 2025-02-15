using DataHorseman.Domain.Entities;

namespace DataHorseman.Domain.Repositories;

public interface IPessoaRepository: IRepositoryBase<Pessoa>
{
    Pessoa? ObtemPessoaPorCPF(string cpf);
}
