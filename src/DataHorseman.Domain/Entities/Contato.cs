using System.ComponentModel.DataAnnotations.Schema;
using DataHorseman.Domain.Enums;

namespace DataHorseman.Domain.Entidades;

[Table("Contatos")]
public class Contato : EntidadeBase
{
    public Pessoa Pessoa { get; set; } = new Pessoa();
    public TipoContato? TipoContato { get; set; }
    public int TipoContatoId { get; set; }

    public string Valor { get; set; } = string.Empty;

    public static List<Contato> ListaDeContatos(Pessoa pessoa, IList<TipoContato> tipoContatos, string[] valoresContatos)
    {
        List<Contato> contatos = new List<Contato>();
        int cont = 0;
        foreach (TipoContato tipoContato in tipoContatos)
        {
            contatos.Add(NovoContato(pessoa, tipoContato, valoresContatos[cont]));
            cont++;
        }
        return contatos;
    }

    public override void Valida()
    {
        var descricaoCampo = DescricaoCampo(TipoContato);
        ValidaCampoTexto(Valor, descricaoCampo);
        if (TipoContato != null && TipoContato.ID.Equals((int)eTipoContato.Email))
            ValidaEmail();
        base.Valida();
    }

    private string DescricaoCampo(TipoContato? tipoContato)
    {
        var descricaoCampo = string.Empty;
        if (tipoContato != null)
            switch (tipoContato.ID)
            {
                case (int)eTipoContato.Email:
                    descricaoCampo = "E-mail";
                    break;
                case (int)eTipoContato.Fixo:
                    descricaoCampo = "Telefone Fixo";
                    break;
                case (int)eTipoContato.Celular:
                    descricaoCampo = "Celular";
                    break;
            }
        return descricaoCampo;
    }

    private void ValidaEmail()
    {
        if (!Valor.Contains("@"))
            AdicionarErro($"E-mail com formato invalido! {Environment.NewLine}");
    }

    public static Contato NovoContato(Pessoa pessoa, TipoContato tipoContato, string valor)
    {
        var contato = new Contato
        {
            Pessoa = pessoa,
            TipoContatoId = tipoContato.ID,
            Valor = valor,
            DataCadastro = DateTime.UtcNow,
            DataAtualizacao = DateTime.UtcNow
        };
        contato.Valida();
        return contato;
    }

    public void AtualizarContato(string valor)
    {
        valor = valor.Trim();
        DataAtualizacao = DateTime.UtcNow;
        Valida();
    }
}