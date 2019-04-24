using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Harvin.Models {
    [Table("Automoveis")]
    public class Automovel {
        [Key]
        public int AutomovelId { get; set; }

        [Display(Name = "Marca")]
        public string AutomovelMarca { get; set; }

        [Display(Name = "Modelo")]
        public string AutomovelModelo { get; set; }

        [Display(Name = "Ano")]
        public int AutomovelAno { get; set; }

        [Display(Name = "Cor")]
        public string AutomovelCor { get; set; }

        [Display(Name = "Placa")]
        public string AutomovelPlaca { get; set; }

        [Display(Name = "Renavan")]
        public string AutomovelRenavan { get; set; }

        public int AutomovelUsuarioId { get; set; }
    }
}