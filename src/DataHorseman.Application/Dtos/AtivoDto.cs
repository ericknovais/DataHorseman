using DataHorseman.Domain.Enums;


namespace DataHorseman.Application.Dtos;

public class AtivoDto
{
    public int ID { get; set; }
    public eTipoDeAtivo TipoDeAtivoId { get; set; }
    public string Ticker { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public decimal UltimaNegociacao { get; set; }
}