using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Harvin.Models {
    [Table("Administradores")]
    public class Administrador {
        [Key]
        public int AdministradorId { get; set; }

        [Display(Name = "Nome Completo")]
        public string AdministradorNome { get; set; }

        [Display(Name = "CPF")]
        public string AdministradorCpf { get; set; }

        [Display(Name = "Telefone")]
        public string AdministradorTelefone { get; set; }

        [Display(Name = "Endereço")]
        public string AdministradorEndereco { get; set; }

        [Display(Name = "E-mail")]
        public string AdministradorEmail { get; set; }

        [Display(Name = "Senha")]
        [DataType(DataType.Password)]
        public string AdministradorSenha { get; set; }
    }
}