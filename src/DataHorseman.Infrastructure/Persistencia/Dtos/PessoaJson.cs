namespace DataHorseman.Infrastructure.Persistencia.Dtos;

public class PessoaJson
{
    public PessoaJson()
    {
        Nome = string.Empty;
        CPF = string.Empty;
        RG = string.Empty;
        Data_nasc = string.Empty;
        Sexo = string.Empty;
        Email = string.Empty;
        CEP = string.Empty;
        Endereco= string.Empty;
        Numero = 0;
        Bairro = string.Empty;
        Cidade = string.Empty;
        Estado = string.Empty;
        Telefone_fixo = string.Empty;
        Celular = string.Empty;
    }
    public string Nome { get; set; }
    public string CPF { get; set; }
    public string RG { get; set; }
    public string Data_nasc { get; set; }
    public string Sexo { get; set; }
    public string Email { get; set; }
    public string CEP { get; set; }
    public string Endereco { get; set; }
    public int Numero { get; set; }
    public string Bairro { get; set; }
    public string Cidade { get; set; }
    public string Estado { get; set; }
    public string Telefone_fixo { get; set; }
    public string Celular { get; set; }
}