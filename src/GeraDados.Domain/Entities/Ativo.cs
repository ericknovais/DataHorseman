using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeraDados.Domain.Entities;

[Table("Ativos")]
public class Ativo : EntityBase
{
    [Required]
    public int TipoDeAtivoId { get; set; }

    [ForeignKey("TipoDeAtivoId")]
    public TipoDeAtivo TipoDeAtivo { get; private set; } = null!;

    public string Ticker { get; private set; } = string.Empty;
    public string Nome { get;private set; } = string.Empty;
    public decimal UltimaNegociacao { get; private set; } = 0;

    public override void Valida()
    {
        ValidaCampoTexto(Ticker, "Ticker");
        ValidaCampoTexto(Nome, "Nome");
        base.Valida();
    }

    public static Ativo AdicionarNovoAtivo(TipoDeAtivo tipoDeAtivo, string ticker, string nome, decimal ultimaNegociacao)
    {
        Ativo ativo = new Ativo()
        {
            TipoDeAtivo = tipoDeAtivo,
            Ticker = ticker.Trim(),
            Nome = nome.Trim(),
            UltimaNegociacao = ultimaNegociacao,
            DataCadastro = DateTime.Now,
            DataAtualizacao = DateTime.Now,

        };
        ativo.Valida();
        return ativo;
    }

    public static List<Ativo> ListaDeAtivosAleatoriaEComQuantidadeDeAtivo(List<Ativo> listaAtivo, int quantidade, Random ordenaLista)
    {
        if (listaAtivo == null || !listaAtivo.Any())
            return new List<Ativo>();

        quantidade = Math.Min(quantidade, listaAtivo.Count);
        return listaAtivo.OrderBy(_ => ordenaLista.Next()).Take(quantidade).ToList();
    }
}