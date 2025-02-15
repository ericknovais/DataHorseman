using System.ComponentModel.DataAnnotations.Schema;

namespace DataHorseman.Domain.Entities;

[Table("TipoContatos")]
public class TipoContato : EntityBase
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public new int ID { get; set; }
    public TipoContato()
    {
        Descricao = string.Empty;
    }

    public TipoContato(TipoContato tipoContato)
    {
        ID = tipoContato.ID;
        Descricao = tipoContato.Descricao;
        DataCadastro = tipoContato.DataCadastro;
        DataAtualizacao = DateTime.Now;
        Valida();
    }

    public string Descricao { get; set; }

    public List<TipoContato> CarregaListaTipoContato()
    {
        List<TipoContato> tipoContatos = new List<TipoContato>()
        {
            new TipoContato() {
                ID = 1,
                Descricao = "E-mail",
                DataCadastro = DateTime.Now,
                DataAtualizacao = DateTime.Now,
            },
            new TipoContato() {
                ID = 2,
                Descricao = "Telefone Fixo",
                DataCadastro = DateTime.Now,
                DataAtualizacao = DateTime.Now,
            },
            new TipoContato() {
                ID = 3,
                Descricao = "Celular",
                DataCadastro = DateTime.Now,
                DataAtualizacao = DateTime.Now,
            }
        };
        return tipoContatos;
    }
}
