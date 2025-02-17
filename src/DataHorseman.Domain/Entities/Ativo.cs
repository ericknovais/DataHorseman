using DataHorseman.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataHorseman.Domain.Entidades;

[Table("Ativos")]
public class Ativo : EntidadeBase
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

    public static Ativo AdicionarNovoAtivo(eTipoDeAtivo tipoDeAtivoID, string ticker, string nome, decimal ultimaNegociacao)
    {
        Ativo ativo = new Ativo()
        {
            TipoDeAtivoId = (int)tipoDeAtivoID,
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