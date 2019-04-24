using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Harvin.Models {
    [Table("Usuarios")]
    public class Usuario {
        [Key]
        public int UsuarioId { get; set; }

        [Display(Name = "Nome Completo")]
        public string UsuarioNome { get; set; }

        [Display(Name = "CPF")]
        public string UsuarioCpf { get; set; }

        [Display(Name = "Telefone")]
        public string UsuarioTelefone { get; set; }

        [Display(Name = "Endereço")]
        public string UsuarioEndereco { get; set; }

        [Display(Name = "E-mail")]
        public string UsuarioEmail { get; set; }

        [Display(Name = "Senha")]
        [DataType(DataType.Password)]
        public string UsuarioSenha { get; set; }

        [Display(Name = "Saldo")]
        public double UsuarioSaldo { get; set; }
    }
}