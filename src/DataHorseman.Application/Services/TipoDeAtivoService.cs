using AutoMapper;
using DataHorseman.Application.Interfaces;
using DataHorseman.Application.Validations;
using DataHorseman.Domain.Entidades;
using DataHorseman.Domain.Interfaces;

namespace DataHorseman.Application.Services;

public class TipoDeAtivoService : ITipoDeAtivoService
{
    protected readonly ITipoDeAtivoRepository _tipoDeAtivoRepository;
    protected readonly IMapper _mapper;

    public TipoDeAtivoService(ITipoDeAtivoRepository tipoDeAtivoRepository, IMapper mapper)
    {
        _tipoDeAtivoRepository = tipoDeAtivoRepository;
        _mapper = mapper;
    }

    public async Task AtualizaAsync(TipoDeAtivo entidade)
    {
        ValidacoesService.EntidadeEhNula(entidade);
        ValidacoesService.ValidaIdMaiorQueZero(entidade.ID);
        var tipoDeAtivoExiste = await _tipoDeAtivoRepository.ObterPorIdAsync(entidade.ID);
        ValidacoesService.ObjetoNaoEncontrado(tipoDeAtivoExiste, entidade.ID, "tipo de ativo");
        tipoDeAtivoExiste.AtualizarTipoDeAtivo(entidade.Descricao);
        _tipoDeAtivoRepository.Atualiza(tipoDeAtivoExiste);
        await _tipoDeAtivoRepository.SaveChangesAsync();
    }

    public async Task CriarEmLoteAsync(IEnumerable<TipoDeAtivo> entidades)
    {
        await _tipoDeAtivoRepository.CriarEmLoteAsync(entidades);
    }

    public async Task CriarNovoAsync(TipoDeAtivo entidade)
    {
        ValidacoesService.EntidadeEhNula(entidade);
        await _tipoDeAtivoRepository.CriarNovoAsync(entidade);
    }

    public async Task ExcluirAsync(TipoDeAtivo entidade)
    {
        ValidacoesService.EntidadeEhNula(entidade);
        ValidacoesService.ValidaIdMaiorQueZero(entidade.ID);
        var tipoDeAtivoExiste = _tipoDeAtivoRepository.ObterPorIdAsync(entidade.ID);
        ValidacoesService.ObjetoNaoEncontrado(tipoDeAtivoExiste, entidade.ID, "tipo de ativo");
        _tipoDeAtivoRepository.Excluir(entidade);
        await _tipoDeAtivoRepository.SaveChangesAsync();
    }

    public async Task<TipoDeAtivo?> ObterPorIdAsync(int id)
    {
        ValidacoesService.ValidaIdMaiorQueZero(id);
        return await _tipoDeAtivoRepository.ObterPorIdAsync(id);
    }

    public async Task<IList<TipoDeAtivo>> ObterTodosAsync()
    {
        var tipoDeAtivos = await _tipoDeAtivoRepository.ObterTodosAsync();
        return tipoDeAtivos is null || !tipoDeAtivos.Any() 
            ? new List<TipoDeAtivo>() : tipoDeAtivos;
    }

    public Task<int> SaveChangesAsync() => _tipoDeAtivoRepository.SaveChangesAsync();
}