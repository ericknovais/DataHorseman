using AutoMapper;
using DataHorseman.Application.Dtos;
using DataHorseman.Application.Interfaces;
using DataHorseman.Application.Validations;
using DataHorseman.Domain.Entidades;
using DataHorseman.Domain.Enums;
using DataHorseman.Domain.Interfaces;

namespace DataHorseman.Application.Services;

public class AtivoService : IAtivoService
{
    protected readonly IAtivoRepository _ativoRepository;
    protected readonly IMapper _mapper;
    public AtivoService(IAtivoRepository ativoRepository, IMapper mapper)
    {
        _ativoRepository = ativoRepository;
        _mapper = mapper;
    }

    public async Task AtualizaAsync(AtivoDto entidade)
    {
        ValidacoesService.EntidadeEhNula(entidade);
        ValidacoesService.ValidaIdMaiorQueZero(entidade.ID);
        var ativoExiste = await _ativoRepository.ObterPorIdAsync(entidade.ID);
        ValidacoesService.ObjetoNaoEncontrado(ativoExiste, entidade.ID, "ativo");
        ativoExiste.AtualizarAtivo(entidade.Ticker, entidade.Nome);
        _ativoRepository.Atualiza(ativoExiste);
        await _ativoRepository.SaveChangesAsync();
    }

    public async Task CriarNovoAsync(AtivoDto entidade)
    {
        ValidacoesService.EntidadeEhNula(entidade);

        var ativo = Ativo.NovoAtivo(
            entidade.TipoDeAtivoId,
            entidade.Ticker,
            entidade.Nome,
            entidade.UltimaNegociacao);

        await _ativoRepository.CriarNovoAsync(ativo);
    }

    public async Task ExcluirAsync(AtivoDto entidade)
    {
        ValidacoesService.EntidadeEhNula(entidade);
        ValidacoesService.ValidaIdMaiorQueZero(entidade.ID);
        var ativoExiste = await _ativoRepository.ObterPorIdAsync(entidade.ID);
        ValidacoesService.ObjetoNaoEncontrado(ativoExiste, entidade.ID, "ativo");
        _ativoRepository.Excluir(ativoExiste);
        await _ativoRepository.SaveChangesAsync();
    }

    public async Task<List<AtivoDto>> ObtemAtivosPorTipoDeAtivoIDAsync(eTipoDeAtivo tipoDeAtivoID)
    {
        ValidacoesService.ValidaIdMaiorQueZero((int)tipoDeAtivoID);

        var ativos = await _ativoRepository.ObtemAtivosPorTipoDeAtivoIDAsync(tipoDeAtivoID);
        if (ativos is null || !ativos.Any())
            return new List<AtivoDto>();

        return _mapper.Map<List<AtivoDto>>(ativos);
    }

    public async Task<AtivoDto?> ObterPorIdAsync(int id)
    {
        ValidacoesService.ValidaIdMaiorQueZero(id);
        var ativo = await _ativoRepository.ObterPorIdAsync(id);
        return ativo is not null ? _mapper.Map<AtivoDto>(ativo) : null;
    }

    public async Task<IList<AtivoDto>> ObterTodosAsync()
    {
        var ativos = await _ativoRepository.ObterTodosAsync();
        return ativos is null || !ativos.Any() ?
             new List<AtivoDto>()
            : _mapper.Map<IList<AtivoDto>>(ativos);
    }

    public async Task<int> SaveChangesAsync() => await _ativoRepository.SaveChangesAsync();

    public async Task CriarEmLoteAsync(IEnumerable<AtivoDto> entidades)
    {
        if (entidades == null || !entidades.Any())
            return;

        var ativos = entidades.Select(ativo =>
         Ativo.NovoAtivo(
             tipoDeAtivoID: ativo.TipoDeAtivoId,
             ticker: ativo.Ticker,
             nome: ativo.Nome,
             ultimaNegociacao: ativo.UltimaNegociacao
            )
        ).ToList();

        await _ativoRepository.CriarEmLoteAsync(ativos);
        await _ativoRepository.SaveChangesAsync();
    }
}