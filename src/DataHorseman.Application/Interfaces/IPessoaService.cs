using DataHorseman.Domain.Entidades;

namespace DataHorseman.Application.Interfaces;

public interface IPessoaService :IServiceBase<Pessoa>
{
    Pessoa? ObtemPessoaPorCPF(string cpf);
}
