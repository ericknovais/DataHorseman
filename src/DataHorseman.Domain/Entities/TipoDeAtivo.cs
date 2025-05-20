using System.ComponentModel.DataAnnotations.Schema;

namespace DataHorseman.Domain.Entidades;

[Table("TipoDeAtivos")]
public class TipoDeAtivo : EntidadeBase
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public new int ID { get; set; }
    public TipoDeAtivo()
    {
        Descricao = string.Empty;
    }

    public string Descricao { get; set; }

    public override void Valida()
    {
        ValidaCampoTexto(Descricao, "Descrição");
        base.Valida();
    }

    public List<TipoDeAtivo> CarregaTipoDeAtivo()
    {
        return new List<TipoDeAtivo>()
        {
            new TipoDeAtivo()
            {
                ID = 1,
                Descricao = "Ação",
                DataCadastro = DateTime.Now,
                DataAtualizacao = DateTime.Now,
            },
            new TipoDeAtivo()
            {
                ID = 2,
                Descricao = "Fundo Imobiliario",
                DataCadastro = DateTime.Now,
                DataAtualizacao =  DateTime.Now
            }
        };
    }
}
