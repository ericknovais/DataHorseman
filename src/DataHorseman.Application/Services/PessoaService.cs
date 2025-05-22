using AutoMapper;
using DataHorseman.Application.Dtos;
using DataHorseman.Application.Interfaces;
using DataHorseman.Application.Validations;
using DataHorseman.Domain.Entidades;
using DataHorseman.Domain.Interfaces;

namespace DataHorseman.Application.Services;

public class PessoaService : IPessoaService
{
    protected readonly IPessoaRepository _repository;
    protected readonly IMapper _mapper;
    public PessoaService(IPessoaRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task AtualizaAsync(PessoaDto entidade)
    {
        ValidacoesService.EntidadeEhNula(entidade);
        ValidacoesService.ValidaIdMaiorQueZero(entidade.ID);

        var pessoaExiste = await _repository.ObterPorIdAsync(entidade.ID);
        ValidacoesService.ObjetoNaoEncontrado(pessoaExiste, entidade.ID, "pessoa");

        pessoaExiste.Atualiza(entidade.Nome);
        _repository.Atualiza(pessoaExiste);
        await _repository.SaveChangesAsync();
    }

    public async Task CriarEmLoteAsync(IEnumerable<PessoaDto> entidades)
    {
        ValidacoesService.ValidaListaNulaOuVazia(entidades, "pessoas");

        var pessoas = entidades.Select(
            entidades => Pessoa.Novo(
                nome: entidades.Nome,
                cpf: entidades.CPF,
                rg: entidades.RG,
                sexo: entidades.Sexo,
                dataNascimento: entidades.DataNascimento)
            ).ToList();

        await _repository.CriarEmLoteAsync(pessoas);
        await _repository.SaveChangesAsync();
    }

    public async Task CriarNovoAsync(PessoaDto entidade)
    {
        ValidacoesService.EntidadeEhNula(entidade);

        Pessoa pessoa = Pessoa.Novo(
            nome: entidade.Nome,
            cpf: entidade.CPF,
            rg: entidade.RG,
            sexo: entidade.Sexo,
            dataNascimento: entidade.DataNascimento);

        await _repository.CriarNovoAsync(pessoa);
    }

    public async Task ExcluirAsync(PessoaDto entidade)
    {
        ValidacoesService.EntidadeEhNula(entidade);
        ValidacoesService.ValidaIdMaiorQueZero(entidade.ID);
        var pessoaExiste = await _repository.ObterPorIdAsync(entidade.ID);
        ValidacoesService.ObjetoNaoEncontrado(pessoaExiste, entidade.ID, "pessoa");
        _repository.Excluir(pessoaExiste);
        await _repository.SaveChangesAsync();
    }

    public async Task<PessoaDto?> ObtemPessoaPorCPF(string cpf)
    {
        if (string.IsNullOrWhiteSpace(cpf))
            throw new ArgumentException("CPF não pode ser nulo ou vazio.", nameof(cpf));
        var pessoaExiste = await _repository.ObtemPessoaPorCPF(cpf);
        return pessoaExiste == null ? null : _mapper.Map<PessoaDto>(pessoaExiste);
    }

    public async Task<PessoaDto?> ObterPorIdAsync(int id)
    {
        ValidacoesService.ValidaIdMaiorQueZero(id);
        var pessoaExiste = await _repository.ObterPorIdAsync(id);
        return pessoaExiste == null ? null : _mapper.Map<PessoaDto>(pessoaExiste);
    }

    public async Task<IList<PessoaDto>> ObterTodosAsync()
    {
        var pessoas = await _repository.ObterTodosAsync();
        return pessoas is null || !pessoas.Any()
            ? new List<PessoaDto>()
            : _mapper.Map<IList<PessoaDto>>(pessoas);
    }


    public async Task<int> SaveChangesAsync() => await _repository.SaveChangesAsync();

    public List<Pessoa> VerificaSePessoasJaCadastradas(List<string> cpfs)
    {
        return _repository.VerificaSePessoasJaCadastradas(cpfs);
    }

    public IList<Pessoa> FiltrarPessoasNaoCadastradas(IList<PessoaDto> pessoas)
    {
        var cpfs = pessoas.Select(p => p.CPF).ToList();
        var pessoasJaCadastradas = _repository.VerificaSePessoasJaCadastradas(cpfs);

        if (pessoasJaCadastradas.Any())
        {
            var cpfsJaCadastrados = pessoasJaCadastradas
                .Select(p => p.CPF)
                .ToHashSet(); // mais rápido para busca

            pessoas = pessoas
                .Where(p => !cpfsJaCadastrados.Contains(p.CPF))
                .ToList();
        }

        return _mapper.Map<IList<Pessoa>>(pessoas);
    }
}