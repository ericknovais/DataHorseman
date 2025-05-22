using DataHorseman.Application.Dtos;
using DataHorseman.Domain.Entidades;

namespace DataHorseman.Application.Interfaces;

public interface IPessoaService : IServiceBase<PessoaDto>
{
    Task<PessoaDto?> ObtemPessoaPorCPF(string cpf);
    List<Pessoa> VerificaSePessoasJaCadastradas(List<string> cpfs);
    IList<Pessoa> FiltrarPessoasNaoCadastradas(IList<PessoaDto> pessoas);
}