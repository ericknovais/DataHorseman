using System.ComponentModel.DataAnnotations.Schema;

namespace DataHorseman.Domain.Entidades;

[Table("TipoContatos")]
public class TipoContato : EntidadeBase
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public new int ID { get; set; }
    public string Descricao { get; set; } = string.Empty;

    public override void Valida()
    {
        ValidaCampoTexto(Descricao, "Descrição");
        base.Valida();
    }
    public static TipoContato NovoTipoContato(int id, string descricao)
    {
        var tipoContato = new TipoContato
        {
            ID = id,
            Descricao = descricao,
            DataCadastro = DateTime.Now,
            DataAtualizacao = DateTime.Now
        };
        tipoContato.Valida();
        return tipoContato;
    }
    public static List<TipoContato> CarregaListaTipoContato()
    {
        List<TipoContato> tipoContatos = new List<TipoContato>()
        {
            NovoTipoContato(10, "E-mail"),
            NovoTipoContato(20, "Telefone Fixo"),
            NovoTipoContato(30, "Celular")
        };
        return tipoContatos;
    }
    public void AtualizarTipoContato(string descricao)
    {
        Descricao = descricao;
        DataAtualizacao = DateTime.Now;
        Valida();
    }
}