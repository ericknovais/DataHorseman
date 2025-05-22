using DataHorseman.Domain.Entidades;

namespace DataHorseman.Domain.Interfaces;

public interface IPessoaRepository: IRepositoryBase<Pessoa>
{
   Task<Pessoa?> ObtemPessoaPorCPF(string cpf);
    List<Pessoa> VerificaSePessoasJaCadastradas(List<string> cpfs);
}
