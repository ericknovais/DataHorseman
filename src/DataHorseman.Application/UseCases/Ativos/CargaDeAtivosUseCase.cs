using DataHorseman.Application.Dtos;
using DataHorseman.Application.Interfaces;
using DataHorseman.Domain.Enums;
using DataHorseman.Infrastructure.Persistencia.Dtos;
using System.Globalization;

namespace DataHorseman.Application.UseCases.Ativos;

public class CargaDeAtivosUseCase : ICargaDeAtivosUseCase
{
    private readonly IService _service;
    public CargaDeAtivosUseCase(IService service)
    {
        _service = service;
    }
    public async Task ExecutarAsync(eTipoDeAtivo tipoDeAtivo)
    {
        string caminhoDoArquivo = ObterCaminhoCompleto(tipoDeAtivo);

        if (!File.Exists(caminhoDoArquivo))
            throw new FileNotFoundException($"Arquivo não encontrado: {caminhoDoArquivo}");

        var ativos = _service.ArquivoService.LerArquivoJson<AtivoJson>(caminhoDoArquivo);

        if (ativos != null)
        {
            if (tipoDeAtivo == eTipoDeAtivo.FundoImobiliario)
                ativos = FiltrarFiis(ativos);

            var ativosDTO = MapearParaDto(ativos, tipoDeAtivo);
            await _service.AtivoService.CriarEmLoteAsync(ativosDTO);
        }
    }

    private IList<AtivoJson> FiltrarFiis(IList<AtivoJson> ativos) => ativos.Where(a => a.Ultimo != "0").ToList();

    private IEnumerable<AtivoDto> MapearParaDto(IList<AtivoJson> ativos, eTipoDeAtivo tipoDeAtivo)
    {
        var cultura = new CultureInfo("pt-BR");
        return ativos.Select(a =>
        {
            decimal valor = 0;
            var valorStr = $"{a.Ultimo},{a.Decimal}";
            decimal.TryParse(valorStr, NumberStyles.Any, cultura, out valor);

            return new AtivoDto
            {
                TipoDeAtivoId = tipoDeAtivo,
                Ticker = a.Ticker,
                Nome = a.Nome,
                UltimaNegociacao = valor
            };
        }).ToList();
    }
    private string ObterCaminhoCompleto(eTipoDeAtivo tipo)
    {
        var basePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CargaDeAtivos");
        return Path.Combine(basePath, ObterNomeArquivo(tipo));
    }
    private string ObterNomeArquivo(eTipoDeAtivo tipo) => tipo switch
    {
        eTipoDeAtivo.Acao => "acoes.json",
        eTipoDeAtivo.FundoImobiliario => "fiis.json",
        _ => throw new ArgumentOutOfRangeException(nameof(tipo), "Tipo de ativo não suportado.")
    };
}