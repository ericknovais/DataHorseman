using AutoMapper;
using DataHorseman.Application.Dtos;
using DataHorseman.Application.Extensions;
using DataHorseman.Application.Interfaces;
using DataHorseman.Application.Validations;
using DataHorseman.Domain.Entidades;
using DataHorseman.Domain.Interfaces;

namespace DataHorseman.Application.Services;

public class EnderecoService : IEnderecoService
{
    protected readonly IEnderecoRepository _enderecoRepository;
    protected readonly IPessoaRepository _pessoaRepository;
    protected readonly IMapper _mapper;

    public EnderecoService(IEnderecoRepository enderecoRepository, IMapper mapper, IPessoaRepository pessoaRepository)
    {
        _enderecoRepository = enderecoRepository;
        _mapper = mapper;
        _pessoaRepository = pessoaRepository;
    }

    public async Task AtualizaAsync(EnderecoDto entidade)
    {
        ValidacoesService.EntidadeEhNula(entidade);
        ValidacoesService.ValidaIdMaiorQueZero(entidade.ID);

        var enderecoExiste = await _enderecoRepository.ObterPorIdAsync(entidade.ID);
        ValidacoesService.ObjetoNaoEncontrado(enderecoExiste, entidade.ID, "endereço");

        var pessoaExiste = await _pessoaRepository.ObterPorIdAsync(entidade.Pessoa.ID);
        ValidacoesService.ObjetoNaoEncontrado(pessoaExiste, entidade.Pessoa.ID, "pessoa");

        enderecoExiste.AtualizarEndereco(pessoaExiste, entidade.ToEnderecoDados());
        _enderecoRepository.Atualiza(enderecoExiste);
        await _enderecoRepository.SaveChangesAsync();
    }

    public Task CriarEmLoteAsync(IEnumerable<EnderecoDto> entidades)
    {
        throw new NotImplementedException();
    }

    public async Task CriarNovoAsync(EnderecoDto entidade)
    {
        ValidacoesService.EntidadeEhNula(entidade);

        var pessoa = await _pessoaRepository.ObterPorIdAsync(entidade.Pessoa.ID);
        ValidacoesService.ObjetoNaoEncontrado(pessoa, entidade.Pessoa.ID, "pessoa");

        var novoEndereco = Endereco.NovoEndereco(pessoa, entidade.ToEnderecoDados());
        await _enderecoRepository.CriarNovoAsync(novoEndereco);
    }

    public async Task ExcluirAsync(EnderecoDto entidade)
    {
        ValidacoesService.EntidadeEhNula(entidade);
        ValidacoesService.ValidaIdMaiorQueZero(entidade.ID);
        var enderecoExiste = await _enderecoRepository.ObterPorIdAsync(entidade.ID);
        ValidacoesService.ObjetoNaoEncontrado(enderecoExiste, entidade.ID, "endereço");
        _enderecoRepository.Excluir(enderecoExiste);
        await _enderecoRepository.SaveChangesAsync();
    }

    public Endereco? ObtemEnderecoPorIdPessoa(int idPessoa)
    {
        ValidacoesService.ValidaIdMaiorQueZero(idPessoa);
        return _enderecoRepository.ObtemEnderecoPorIdPessoa(idPessoa);
    }

    public async Task<EnderecoDto?> ObterPorIdAsync(int id)
    {
        ValidacoesService.ValidaIdMaiorQueZero(id);
        var endereco = await _enderecoRepository.ObterPorIdAsync(id);
        return endereco is not null ? _mapper.Map<EnderecoDto>(endereco) : null;
    }

    public async Task<IList<EnderecoDto>> ObterTodosAsync()
    {
        var enderecos = await _enderecoRepository.ObterTodosAsync();
        return enderecos is null || !enderecos.Any() ?
            new List<EnderecoDto>() : _mapper.Map<IList<EnderecoDto>>(enderecos);
    }

    public Task<int> SaveChangesAsync() => _enderecoRepository.SaveChangesAsync();
}
