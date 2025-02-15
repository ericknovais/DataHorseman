using System.ComponentModel.DataAnnotations.Schema;

namespace DataHorseman.Domain.Entities;

[Table("Pessoas")]
public class Pessoa : EntityBase
{
    public Pessoa()
    {
        Nome = string.Empty;
        CPF = string.Empty;
        RG = string.Empty;
        Sexo = string.Empty;
    }

    public Pessoa(string nome, string cpf, string rg, string sexo, DateTime dataNascimento)
    {
        Nome = nome;
        CPF = cpf;
        RG = rg;
        Sexo = sexo;
        DataNascimento = dataNascimento;
        DataCadastro = DateTime.Now;
        DataAtualizacao = DateTime.Now;
        Valida();
    }

    public string Nome { get; set; }
    public string CPF { get; set; }
    public string RG { get; set; }
    public DateTime DataNascimento { get; set; }
    public string Sexo { get; set; }

    public override void Valida()
    {
        ValidaCampoTexto(Nome, "Nome Completo");
        ValidaCampoTexto(CPF, "CPF");
        ValidaCampoTexto(RG, "RG");
        ValidaCampoTexto(Sexo, "Sexo");
        base.Valida();
    }
}
