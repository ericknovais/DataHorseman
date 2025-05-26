namespace DataHorseman.Application.Dtos;

public class PessoaDto
{
    public int ID { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string CPF { get; set; } = string.Empty;
    public string RG { get; set; } = string.Empty;
    public DateTime DataNascimento { get; set; }
    public string Sexo { get; set; } = string.Empty;

    public static PessoaDto NovaPessoaDto(string nome, string cpf, string rg, DateTime dataNascimento, string sexo)
    {
        return new PessoaDto()
        {
            Nome = nome,
            CPF = cpf,
            RG = rg,
            DataNascimento = dataNascimento,
            Sexo = sexo
        };
    }
}