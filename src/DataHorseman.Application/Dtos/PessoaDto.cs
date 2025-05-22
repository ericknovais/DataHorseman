namespace DataHorseman.Application.Dtos;

public class PessoaDto
{
    public int ID { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string CPF { get; set; } = string.Empty;
    public string RG { get; set; } = string.Empty;
    public DateTime DataNascimento { get; set; }
    public string Sexo { get; set; } = string.Empty;
}