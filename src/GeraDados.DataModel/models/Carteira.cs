﻿using System.ComponentModel.DataAnnotations.Schema;

namespace GeraDados.DataModel.Models
{
    [Table("Carteiras")]
    public class Carteira : EntityBase
    {
        public Carteira()
        {
            Pessoa = new Pessoa();
            Ativo = new Ativo();
        }
        public Pessoa Pessoa { get; set; }
        public Ativo Ativo { get; set; }
        public int Cota { get; set; }

        public override void Valida()
        {
            ValidaCampoNumerico(Cota, "Cota");
            base.Valida();
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