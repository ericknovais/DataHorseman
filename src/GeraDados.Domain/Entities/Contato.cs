using System.ComponentModel.DataAnnotations.Schema;

namespace GeraDados.Domain.Entities;

[Table("Contatos")]
public class Contato : EntityBase
{
    public Contato()
    {
        Pessoa = new Pessoa();
        TipoContato = new TipoContato();
        Valor = string.Empty;
    }

    public Contato(Pessoa pessoa, TipoContato tipoContato, string valor)
    {
        Pessoa = pessoa;
        TipoContato = tipoContato;
        Valor = valor;
        DataCadastro = DateTime.Now;
        DataAtualizacao = DateTime.Now;
        Valida();
    }
    public Pessoa Pessoa { get; set; }
    public TipoContato? TipoContato { get; set; }
    public string Valor { get; set; }

    public static List<Contato> ListaDeContatos(Pessoa pessoa, IList<TipoContato> tipoContatos, string[] valoresContatos)
    {
        List<Contato> contatos = new List<Contato>();
        int cont = 0;
        foreach (TipoContato tipoContato in tipoContatos) 
        {
            contatos.Add(new Contato(pessoa, tipoContato, valoresContatos[cont]));
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
            _msgErro.Append($"E-mail com formato invalido! {Environment.NewLine}");
    }
}