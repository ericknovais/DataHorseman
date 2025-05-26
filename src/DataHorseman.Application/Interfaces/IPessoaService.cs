using DataHorseman.Application.Dtos;
using DataHorseman.Domain.Entidades;

namespace DataHorseman.Application.Interfaces;

public interface IPessoaService : IServiceBase<PessoaDto>
{
    Task<Pessoa?> ObtemPessoaPorCPF(string cpf);
    List<Pessoa> VerificaSePessoasJaCadastradas(List<string> cpfs);
    IList<Pessoa> FiltrarPessoasNaoCadastradas(IList<PessoaDto> pessoas);
    new Task<int> CriarNovoAsync(PessoaDto entidade);
}