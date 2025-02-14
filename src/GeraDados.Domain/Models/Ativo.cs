using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeraDados.DataModel.Models;

[Table("Ativos")]
public class Ativo : EntityBase
{
    public Ativo()
    {
        TipoDeAtivo = new TipoDeAtivo();
        Ticker = string.Empty;
        Nome = string.Empty;
    }

    public Ativo(TipoDeAtivo tipoDeAtivo, string ticker, string nome, string ultimaNegociacao)
    {
        TipoDeAtivo = tipoDeAtivo;
        Ticker = ticker;
        Nome = nome;
        UltimaNegociacao = Convert.ToDecimal(ultimaNegociacao);
        DataCadastro = DateTime.Now;
        DataAtualizacao = DateTime.Now;
        Valida();
    }

    [Required]
    public TipoDeAtivo? TipoDeAtivo { get; set; }
    public string Ticker { get; set; }
    public string Nome { get; set; }
    public decimal UltimaNegociacao { get; set; }

    public override void Valida()
    {
        ValidaCampoTexto(Ticker, "Ticker");
        ValidaCampoTexto(Nome, "Nome");
        base.Valida();
    }

    public static List<Ativo> ListaDeAtivosAleatoriaEComQuantidadeDeAtivo(List<Ativo> listaAtivo, int quantidade, Random ordenaLista)
    {
        return listaAtivo.OrderBy(acoes => ordenaLista.Next()).Take(quantidade).ToList();
    }
}