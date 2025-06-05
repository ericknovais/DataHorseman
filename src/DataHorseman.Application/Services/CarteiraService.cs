using AutoMapper;
using DataHorseman.Application.Dtos;
using DataHorseman.Application.Interfaces;
using DataHorseman.Application.Validations;
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

    public Task AtualizaAsync(CarteiraDto entidade)
    {
        throw new NotImplementedException();
    }

    public Task CriarEmLoteAsync(IEnumerable<CarteiraDto> entidades)
    {
        throw new NotImplementedException();
    }

    public async Task CriarNovasCarteirasLote(CarteiraDto entidade)
    {
        ValidacoesService.EntidadeEhNula(entidade);

        var acoes = entidade.Acoes ?? new List<AtivoDto>();
        var fiis = entidade.FundosImobiliarios ?? new List<AtivoDto>();

        CarteiraConfigurada carteiraConfigurada =
            CarteiraConfigurada.NovaCarteiraConfiguracao(
                _mapper.Map<List<Ativo>>(acoes),
                _mapper.Map<List<Ativo>>(fiis)
            );

        var tasks = new List<Task>();
        tasks.AddRange(CriarCarteirasValidas(carteiraConfigurada.Acoes, entidade.Pessoa, carteiraConfigurada.ValorParaAcoes));
        tasks.AddRange(CriarCarteirasValidas(carteiraConfigurada.Fiis, entidade.Pessoa, carteiraConfigurada.ValorParaAcoes));

        await Task.WhenAll(tasks);
    }
    private List<Task> CriarCarteirasValidas(IEnumerable<Ativo> ativos, Pessoa pessoa, double valor)
    {
        return ativos
            .Select(ativo => Carteira.NovaCarteira(pessoa, ativo, valor))
            .Where(carteira => carteira.Cota > 0)
            .Select(carteira => _carteiraRepository.CriarNovoAsync(carteira))
            .ToList();
    }

    public Task CriarNovoAsync(CarteiraDto entidade)
    {
        throw new NotImplementedException();
    }

    public Task ExcluirAsync(CarteiraDto entidade)
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