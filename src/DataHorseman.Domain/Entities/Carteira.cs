﻿using System.ComponentModel.DataAnnotations.Schema;

namespace DataHorseman.Domain.Entidades
{
    [Table("Carteiras")]
    public class Carteira : EntidadeBase
    {
        //public Carteira(Pessoa pessoa, Ativo ativo, double valorPorAtivo)
        //{
        //    Pessoa = pessoa;
        //    Ativo = ativo;
        //    Cota = Carteira.QuantidadeDeUmAtivo(valorPorAtivo, (double)ativo.UltimaNegociacao);
        //    DataCadastro = DateTime.Now;
        //    DataAtualizacao = DateTime.Now;
        //}

        public Pessoa Pessoa { get; set; } = new Pessoa();
        public Ativo Ativo { get; set; } = new Ativo();
        public int Cota { get; set; }

        public override void Valida()
        {
            ValidaCampoNumerico(Cota, "Cota");
            base.Valida();
        }

        public static Carteira NovaCarteira(Pessoa pessoa, Ativo ativo, double valorPorAtivo)
        {
            var cota = Carteira.QuantidadeDeUmAtivo(valorPorAtivo, (double)ativo.UltimaNegociacao);
            Carteira carteira = new Carteira()
            {
                Pessoa = pessoa,
                Ativo = ativo,
                Cota = cota,
                DataCadastro = DateTime.Now,
                DataAtualizacao = DateTime.Now
            };
            return carteira;
        }

        public static int InicializaValorInicialDaPessoa()
        {
            return new Random().Next(200, 1000000);
        }

        public static double PorcentagelDoValorParaUmTipoDeAtivo(int valorInicial)
        {
            double porcentagem = Convert.ToDouble(new Random().Next(1, 100) / 100.00);
            return (valorInicial * porcentagem);
        }

        public static int QuantidadeDeUmAtivo(double valorParaAtivo, double valorDoAtivo)
        {
            return (int)(valorParaAtivo / valorDoAtivo);
        }
    }
}