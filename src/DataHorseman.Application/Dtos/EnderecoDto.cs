using DataHorseman.Domain.Entidades;
using DataHorseman.Infrastructure.Persistencia.Dtos;

namespace DataHorseman.Application.Dtos;

public class EnderecoDto
{
    public int ID { get; set; }
    public string Logradouro { get; set; } = string.Empty;
    public int Numero { get; set; } = 0;
    public string Complemento { get; set; } = string.Empty;
    public string Bairro { get; set; } = string.Empty;
    public string Cidade { get; set; } = string.Empty;
    public string Estado { get; set; } = string.Empty;
    public string CEP { get; set; } = string.Empty;
    public Pessoa Pessoa { get; set; } = new Pessoa();

    public static EnderecoDto NovoEnderecoDto(Pessoa pessoa, PessoaJson pessoaJson)
    {
        return new EnderecoDto
        {
            Pessoa = pessoa,
            CEP = pessoaJson.CEP,
            Logradouro = pessoaJson.Endereco,
            Numero = pessoaJson.Numero,
            Bairro = pessoaJson.Bairro,
            Cidade = pessoaJson.Cidade,
            Estado = pessoaJson.Estado
        };
    }
}