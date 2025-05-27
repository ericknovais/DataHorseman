using DataHorseman.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataHorseman.Domain.Entidades;

[Table("Enderecos")]
public class Endereco : EntidadeBase
{
    public Pessoa Pessoa { get; private set; } = new Pessoa();
    public string CEP { get; private set; } = string.Empty;
    public string Logradouro { get; private set; } = string.Empty;
    public int Numero { get; private set; } = 0;
    public string Bairro { get; private set; } = string.Empty;
    public string Cidade { get; private set; } = string.Empty;
    public string Estado { get; private set; } = string.Empty;

    public override void Valida()
    {
        LimparErros();
        ValidaCamposObrigatorios(
            (CEP, "CEP"),
            (Logradouro, "Logradouro"),
            (Bairro, "Bairro"),
            (Cidade, "Cidade"),
            (Estado, "Estado")
        );
        ValidaCampoNumerico(Numero, "Número");
        base.Valida();
    }

    public static Endereco NovoEndereco(Pessoa pessoa, EnderecoDados enderecoDados)
    {
        Endereco endereco = new Endereco
        {
            Pessoa = pessoa,
            CEP = enderecoDados.CEP,
            Logradouro = enderecoDados.Logradouro,
            Numero = enderecoDados.Numero,
            Bairro = enderecoDados.Bairro,
            Cidade = enderecoDados.Cidade,
            Estado = enderecoDados.Estado,
            DataCadastro = DateTime.UtcNow,
            DataAtualizacao = DateTime.UtcNow,
        };
        endereco.Valida();
        return endereco;
    }

    public void AtualizarEndereco(Pessoa pessoa, EnderecoDados enderecoDados)
    {  
        Pessoa = pessoa;
        Logradouro = enderecoDados.Logradouro;
        CEP = enderecoDados.CEP;
        Bairro = enderecoDados.Bairro;
        Cidade = enderecoDados.Cidade;
        Estado = enderecoDados.Estado;
        Numero = enderecoDados.Numero;
        DataAtualizacao = DateTime.UtcNow;
        Valida();
    }
}