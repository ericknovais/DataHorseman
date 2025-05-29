using AutoMapper;
using DataHorseman.Application.Interfaces;
using DataHorseman.Application.Validations;
using DataHorseman.Domain.Entidades;
using DataHorseman.Domain.Interfaces;

namespace DataHorseman.Application.Services;

public class TipoContatoService : ITipoContatoService
{
    protected readonly ITipoContatoRepository _tipoContatoRepository;
    protected readonly IMapper _mapper;

    public TipoContatoService(ITipoContatoRepository tipoContatoRepository, IMapper mapper)
    {
        _tipoContatoRepository = tipoContatoRepository;
        _mapper = mapper;
    }

    public async Task AtualizaAsync(TipoContato entidade)
    {
        ValidacoesService.EntidadeEhNula(entidade);
        ValidacoesService.ValidaIdMaiorQueZero(entidade.ID);
        var tipoContatoExiste = await _tipoContatoRepository.ObterPorIdAsync(entidade.ID);
        ValidacoesService.ObjetoNaoEncontrado(tipoContatoExiste, entidade.ID, "tipo de contato");
        tipoContatoExiste.AtualizarTipoContato(entidade.Descricao);
        _tipoContatoRepository.Atualiza(tipoContatoExiste);
        await _tipoContatoRepository.SaveChangesAsync();
    }

    public async Task CriarEmLoteAsync(IEnumerable<TipoContato> entidades)
    {
        await _tipoContatoRepository.CriarEmLoteAsync(entidades);
    }

    public async Task CriarNovoAsync(TipoContato entidade)
    {
        ValidacoesService.EntidadeEhNula(entidade);
        await _tipoContatoRepository.CriarNovoAsync(entidade);
    }

    public async Task ExcluirAsync(TipoContato entidade)
    {
        ValidacoesService.EntidadeEhNula(entidade);
        ValidacoesService.ValidaIdMaiorQueZero(entidade.ID);
        var tipoContatoExiste = await _tipoContatoRepository.ObterPorIdAsync(entidade.ID);
        ValidacoesService.ObjetoNaoEncontrado(tipoContatoExiste, entidade.ID, "tipo de contato");
        _tipoContatoRepository.Excluir(entidade);
        await _tipoContatoRepository.SaveChangesAsync();
    }

    public async Task<TipoContato?> ObterPorIdAsync(int id)
    {
        ValidacoesService.ValidaIdMaiorQueZero(id);
        return await _tipoContatoRepository.ObterPorIdAsync(id);
    }

    public async Task<IList<TipoContato>> ObterTodosAsync()
    {
        var tipoContatos = await _tipoContatoRepository.ObterTodosAsync();
        return tipoContatos is null || !tipoContatos.Any() 
            ? new List<TipoContato>() : tipoContatos;
    }

    public Task<int> SaveChangesAsync() => _tipoContatoRepository.SaveChangesAsync();
}