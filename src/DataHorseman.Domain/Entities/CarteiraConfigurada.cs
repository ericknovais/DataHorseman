using System.ComponentModel.DataAnnotations.Schema;

namespace DataHorseman.Domain.Entities;

[NotMapped]
public class CarteiraConfigurada
{
    public CarteiraConfigurada()
    {
        Acoes = new List<Ativo>();
        Fiis = new List<Ativo>();

    }

    public int ValorInicial { get; set; }
    public double ValorParaAcoes { get; set; }
    public int QuantidadeDeAcoes { get; set; }
    public double ValorPorAcoes { get; set; }
    public double ValorParaFiis { get; set; }
    public int QuantidaDeFiis { get; set; }
    public double ValorPorFiis { get; set; }
    public int ValorTotal { get; set; }
    public List<Ativo> Acoes { get; set; }
    public List<Ativo> Fiis { get; set; }

    public static CarteiraConfigurada NovaCarteiraConfiguracao(List<Ativo> acoes, List<Ativo> fiis) 
    {
        Random rdm = new Random();
        int valorInicial = Carteira.InicializaValorInicialDaPessoa();
        double valorParaAcoes = Carteira.PorcentagelDoValorParaUmTipoDeAtivo(valorInicial);
        int qtdAcoes = rdm.Next(5, 30);
        double valorPorAcoes = valorParaAcoes / qtdAcoes;
        double valorParaFiis = valorInicial - valorParaAcoes;
        int qtdFiis = rdm.Next(5, 15);
        double valorPorFiis = valorParaFiis / qtdFiis;
        double valorTotal = valorParaAcoes + valorParaFiis;
        List<Ativo> listaAcoes = acoes;
        List<Ativo> listaFiis = fiis;

        return new CarteiraConfigurada()
        {
            ValorInicial = valorInicial,
            ValorParaAcoes = valorParaAcoes,
            QuantidadeDeAcoes = qtdAcoes,
            ValorPorAcoes = valorPorAcoes,
            ValorParaFiis = valorParaFiis,
            QuantidaDeFiis = qtdFiis,
            ValorPorFiis = valorPorFiis,
            ValorTotal = (int)valorTotal,
            Acoes = Ativo.ListaDeAtivosAleatoriaEComQuantidadeDeAtivo(listaAcoes, qtdAcoes, rdm),
            Fiis = Ativo.ListaDeAtivosAleatoriaEComQuantidadeDeAtivo(listaFiis, qtdFiis, rdm),
        };
    }
}