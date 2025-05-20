using AutoMapper;
using DataHorseman.Application.Dtos;
using DataHorseman.Application.Interfaces;
using DataHorseman.Domain.Entidades;
using DataHorseman.Domain.Interfaces;

namespace DataHorseman.Application.Services;

public class CarteiraService : ICarteiraService
{
    protected readonly ICarteiraRepository _carteiraRepository;
    protected readonly IMapper _mapper;
    public CarteiraService(ICarteiraRepository carteiraRepository, IMapper mapper)
    {
        _carteiraRepository = carteiraRepository;
        _mapper = mapper;
    }

    public Task Atualiza(CarteiraDto entidade)
    {
        throw new NotImplementedException();
    }

    public async Task CriarNovasCarteirasLote(CarteiraDto entidade)
    {
        if (entidade is null)
            throw new ArgumentNullException(nameof(entidade), "A entidade fornecida não pode ser nula.");

        var acoes = entidade.Acoes ?? new List<AtivoDto>();
        var fiis = entidade.FundosImobiliarios ?? new List<AtivoDto>();

        CarteiraConfigurada carteiraConfigurada =
            CarteiraConfigurada.NovaCarteiraConfiguracao(
                _mapper.Map<List<Ativo>>(acoes),
                _mapper.Map<List<Ativo>>(fiis)
            );

        var tasks = new List<Task>();

        foreach (var acao in carteiraConfigurada.Acoes)
            tasks.Add(_carteiraRepository.CriarNovoAsync(
                Carteira.NovaCarteira(
                    entidade.Pessoa,
                    acao,
                    carteiraConfigurada.ValorParaAcoes)));

        foreach (var fii in carteiraConfigurada.Fiis)
            tasks.Add(_carteiraRepository.CriarNovoAsync(
                Carteira.NovaCarteira(
                    entidade.Pessoa,
                    fii,
                    carteiraConfigurada.ValorPorFiis)));

        await Task.WhenAll(tasks);
    }

    public Task CriarNovoAsync(CarteiraDto entidade)
    {
        throw new NotImplementedException();
    }

    public Task Excluir(CarteiraDto entidade)
    {
        throw new NotImplementedException();
    }

    public Task<CarteiraDto?> ObterPorIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IList<CarteiraDto>> ObterTodosAsync()
    {
        throw new NotImplementedException();
    }

    public Task<int> SaveChangesAsync()
    {
        throw new NotImplementedException();
    }
}