using System.ComponentModel.DataAnnotations.Schema;

namespace DataHorseman.Domain.Entidades;

[Table("Pessoas")]
public class Pessoa : EntidadeBase
{
    public string Nome { get; set; } = string.Empty;
    public string CPF { get; set; } = string.Empty;
    public string RG { get; set; } = string.Empty;
    public DateTime DataNascimento { get; set; }
    public string Sexo { get; set; } = string.Empty;

    public override void Valida()
    {
        LimparErros();
        ValidaCamposObrigatorios(
            (Nome, "Nome Completo"),
            (CPF, "CPF"),
            (RG, "RG"),
            (Sexo, "Sexo")
        );

        if (DataNascimento > DateTime.Now)
            AdicionarErro("Data de nascimento não pode ser futura.");

        //if (!CpfValido(CPF))
        //    AdicionarErro("CPF inválido.");

        base.Valida();
    }

    private bool CpfValido(string cpf)
    {
        return cpf.Length >= 11 && cpf.All(char.IsDigit); 
    }

    public static Pessoa Novo(string nome, string cpf, string rg, string sexo, DateTime dataNascimento)
    {

        Pessoa pessoa = new Pessoa()
        {
            Nome = nome,
            CPF = cpf,
            RG = rg,
            Sexo = sexo,
            DataNascimento = dataNascimento,
            DataCadastro = DateTime.UtcNow,
            DataAtualizacao = DateTime.UtcNow
        };
        pessoa.Valida();
        return pessoa;
    }

    public void Atualiza(string nome) 
    {
        this.Nome = nome;
        DataAtualizacao = DateTime.UtcNow;
        Valida();
    }
}
