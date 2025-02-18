using AutoMapper;
using DataHorseman.Application.Dtos;
using DataHorseman.Application.Interfaces;
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

    private void EntidadeEhNula(AtivoDto entidade)
    {
        if (entidade is null)
            throw new ArgumentNullException(nameof(entidade), "O ativo fornecido não pode ser nula");
    }

    public async Task Atualiza(AtivoDto entidade)
    {
        EntidadeEhNula(entidade);

        var ativo = await _ativoRepository.ObterPorIdAsync(entidade.ID);
        if (ativo is null)
            throw new KeyNotFoundException($"Nenhum ativo encontrado com o ID {entidade.ID}.");

        ativo.AtualizarAtivo(entidade.Ticker, entidade.Nome);
        _ativoRepository.Atualiza(ativo);
        await _ativoRepository.SaveChangesAsync();
    }

    public async Task CriarNovoAsync(AtivoDto entidade)
    {
        EntidadeEhNula(entidade);

        var ativo = Ativo.NovoAtivo(
            entidade.TipoDeAtivo,
            entidade.Ticker,
            entidade.Nome,
            entidade.UltimoValorNegociado);

        await _ativoRepository.CriarNovoAsync(ativo);
    }

    public async Task Excluir(AtivoDto entidade)
    {
        EntidadeEhNula(entidade);

        var ativo = await _ativoRepository.ObterPorIdAsync(entidade.ID);

        if (ativo is null)
            throw new KeyNotFoundException($"Nenhum ativo encontrado com o ID {entidade.ID}.");

        _ativoRepository.Excluir(ativo);
        await _ativoRepository.SaveChangesAsync();
    }

    public List<AtivoDto> ObtemAtivosPorTipoDeAtivoID(eTipoDeAtivo tipoDeAtivoID)
    {
        if ((int)tipoDeAtivoID <= 0)
            throw new ArgumentException("O ID do tipo de ativo deve ser maior que zero.", nameof(tipoDeAtivoID));

        var ativos = _ativoRepository.ObtemAtivosPorTipoDeAtivoID(tipoDeAtivoID);
        if (ativos is null || !ativos.Any())
            return new List<AtivoDto>();

        return _mapper.Map<List<AtivoDto>>(ativos);
    }

    public async Task<AtivoDto?> ObterPorIdAsync(int id)
    {
        if (id <= 0)
            throw new ArgumentOutOfRangeException(nameof(id), "O ID do ativo deve ser maior que zero.");

        var ativo = await _ativoRepository.ObterPorIdAsync(id);

        return ativo is not null ? _mapper.Map<AtivoDto>(ativo) : null;
    }

    public async Task<IList<AtivoDto>> ObterTodosAsync()
    {
        var ativos = await _ativoRepository.ObterTodosAsync();
        if (ativos is null || !ativos.Any())
            return new List<AtivoDto>();
        return _mapper.Map<IList<AtivoDto>>(ativos);
    }

    public async Task<int> SaveChangesAsync() => await _ativoRepository.SaveChangesAsync();
}