namespace DataHorseman.Domain.Entities;

public class EnderecoDados
{
    public string Logradouro { get; init; } = string.Empty;
    public string CEP { get; init; } = string.Empty;
    public string Bairro { get; init; } = string.Empty;
    public string Cidade { get; init; } = string.Empty;
    public string Estado { get; init; } = string.Empty;
    public int Numero { get; init; }
}