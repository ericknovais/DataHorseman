using System.ComponentModel.DataAnnotations.Schema;

namespace DataHorseman.Domain.Entidades;

[Table("TipoDeAtivos")]
public class TipoDeAtivo : EntidadeBase
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public new int ID { get; set; }
    public string Descricao { get; set; } = string.Empty;

    public override void Valida()
    {
        ValidaCampoTexto(Descricao, "Descrição");
        base.Valida();
    }

    public static List<TipoDeAtivo> CarregaTipoDeAtivo()
    {
        return new List<TipoDeAtivo>()
        {
            NovoTipoDeAtivo(1, "Ação"),
            NovoTipoDeAtivo(2, "Fundo Imobiliario"),
        };
    }
    public static TipoDeAtivo NovoTipoDeAtivo(int id, string descricao)
    {
        var tipoDeAtivo = new TipoDeAtivo
        {
            ID = id,
            Descricao = descricao,
            DataCadastro = DateTime.Now,
            DataAtualizacao = DateTime.Now
        };
        tipoDeAtivo.Valida();
        return tipoDeAtivo;
    }

    public void AtualizarTipoDeAtivo(string descricao)
    {
        Descricao = descricao;
        DataAtualizacao = DateTime.Now;
        Valida();
    }
}
