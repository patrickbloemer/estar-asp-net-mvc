using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Harvin.Models {
    [Table("Estares")]
    public class Estar {
        [Key]
        public int EstarId { get; set; }
        public Usuario EstarUsuario { get; set; }
        public Automovel EstarAutomovel { get; set; }

        //[ScaffoldColumn(false)]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        //public DateTime EstarData { get; set; }

        //[ScaffoldColumn(false)]
        //[DisplayFormat(DataFormatString = "{0:hh:mm}")]
        //public DateTime EstarHorario { get; set; }

        public DateTime EstarDataHorario { get; set; }

        public DateTime EstarHorarioFinal { get; set; }

    }
}