using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Harvin.Models {
    [Table("LoginAdmins")]
    public class LoginAdmin {
        [Key]
        public int LoginAdminId { get; set; }
        public Administrador LoginAdminAdministrador { get; set; }
        public string LoginAdminSessao { get; set; }
    }
}