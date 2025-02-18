using DataHorseman.Domain.Enums;


namespace DataHorseman.Application.Dtos;

public class AtivoDto
{
    public int ID { get; set; }
    public eTipoDeAtivo TipoDeAtivo { get; set; }
    public string Ticker { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public decimal UltimoValorNegociado { get; set; }
}