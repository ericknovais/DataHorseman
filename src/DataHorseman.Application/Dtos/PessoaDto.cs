using DataHorseman.Infrastructure.Persistencia.Dtos;

namespace DataHorseman.Application.Dtos;

public class PessoaDto
{
    public int ID { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string CPF { get; set; } = string.Empty;
    public string RG { get; set; } = string.Empty;
    public DateTime DataNascimento { get; set; }
    public string Sexo { get; set; } = string.Empty;

    public static PessoaDto NovaPessoaDto(PessoaJson pessoaJson)
    { 
        var dataNascimento = DateTime.TryParse(pessoaJson.Data_nasc, out DateTime parsedDate) ? parsedDate : DateTime.MinValue;
        return new PessoaDto()
        {
            Nome = pessoaJson.Nome,
            CPF = pessoaJson.CPF,
            RG = pessoaJson.CPF,
            DataNascimento = dataNascimento,
            Sexo = pessoaJson.Sexo
        };
    }
}