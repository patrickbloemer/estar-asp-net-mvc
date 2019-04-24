using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Harvin.Models {
    public class Entities :DbContext {
        public DbSet<Administrador> Administradores { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Estar> Estares { get; set; }
        public DbSet<Automovel> Automoveis { get; set; }
        public DbSet<LoginUser> LoginUsers { get; set; }
        public DbSet<LoginAdmin> LoginAdmins { get; set; }
        public DbSet<HistoricoCompraSaldo> HistoricoCompraSaldos { get; set; }
    }
}