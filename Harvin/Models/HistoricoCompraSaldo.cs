using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Harvin.Models {
    [Table("HistoricoCompraSaldos")]
    public class HistoricoCompraSaldo {
        [Key]
        public int HistoricoCompraSaldoId { get; set; }

        public Usuario HistoricoCompraSaldoUsuario { get; set; }

        [Display(Name = "Saldo")]
        public double SaldoUsuario { get; set; }

        [Display(Name = "Número do Cartão")]
        public string NumeroCartao { get; set; }

        [Display(Name = "Nome Completo")]
        public string NomeCompletoCartao { get; set; }

        [Display(Name = "Bandeira")]
        public string BandeiraCartao { get; set; }

        [Display(Name = "Cód. Segurança")]
        public int CodigoSegurancaCartao { get; set; }
    }
}