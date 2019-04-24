using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Harvin.Models {
    [Table("LoginUsers")]
    public class LoginUser {
        [Key]
        public int LoginUserId { get; set; }
        public Usuario LoginUserUsuario { get; set; }
        public string LoginUserSessao { get; set; }
    }
}