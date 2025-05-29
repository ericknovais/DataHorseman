using System.ComponentModel.DataAnnotations.Schema;

namespace DataHorseman.Domain.Entidades
{
    [Table("Carteiras")]
    public class Carteira : EntidadeBase
    {
        private static readonly Random _random = new();

        public Pessoa? Pessoa { get; set; } 
        public int PessoaId { get; set; }
        public Ativo? Ativo { get; set; } 
        public int AtivoId { get; set; }
        public int Cota { get; set; }

        public override void Valida()
        {
            ValidaCampoNumerico(Cota, "Cota");
            ValidaCampoNumerico(PessoaId, "PessoaId");
            ValidaCampoNumerico(AtivoId, "AtivoId");
            base.Valida();
        }

        public static Carteira NovaCarteira(Pessoa pessoa, Ativo ativo, double valorPorAtivo)
        {

            var cota = Carteira.QuantidadeDeUmAtivo(valorPorAtivo, Convert.ToDouble(ativo.UltimaNegociacao));
            Carteira carteira = new Carteira()
            {
                PessoaId = pessoa.ID,
                AtivoId = ativo.ID,
                Cota = cota,
                DataCadastro = DateTime.Now,
                DataAtualizacao = DateTime.Now
            };
            carteira.Valida();
            return carteira;
        }

        public static int InicializaValorInicialDaPessoa() => _random.Next(200, 1000000);
        public static double PorcentagemDoValorParaUmTipoDeAtivo(int valorInicial)
        {
            double porcentagem = _random.Next(1, 100) / 100.00;
            return (valorInicial * porcentagem);
        }
        public static int QuantidadeDeUmAtivo(double valorParaAtivo, double valorDoAtivo)
        {
            return (int)(valorParaAtivo / valorDoAtivo);
        }
    }
}