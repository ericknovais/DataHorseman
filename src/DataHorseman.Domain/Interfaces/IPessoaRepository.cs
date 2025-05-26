using DataHorseman.Domain.Entidades;

namespace DataHorseman.Domain.Interfaces;

public interface IPessoaRepository : IRepositoryBase<Pessoa>
{
    Task<Pessoa?> ObtemPessoaPorCPFAsync(string cpf);
    Task<List<Pessoa>> VerificaSePessoasJaCadastradasAsync(List<string> cpfs);
}
