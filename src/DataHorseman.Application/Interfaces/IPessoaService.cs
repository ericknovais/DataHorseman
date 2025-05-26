using DataHorseman.Application.Dtos;
using DataHorseman.Domain.Entidades;

namespace DataHorseman.Application.Interfaces;

public interface IPessoaService : IServiceBase<PessoaDto>
{
    Task<Pessoa?> ObtemPessoaPorCPFAsync(string cpf);
    Task<List<Pessoa>> VerificaSePessoasJaCadastradasAsync(List<string> cpfs);
    Task<IList<Pessoa>> FiltrarPessoasNaoCadastradas(IList<PessoaDto> pessoas);
    new Task<int> CriarNovoAsync(PessoaDto entidade);
}