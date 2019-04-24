namespace Harvin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Start : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Administradores",
                c => new
                    {
                        AdministradorId = c.Int(nullable: false, identity: true),
                        AdministradorNome = c.String(),
                        AdministradorCpf = c.String(),
                        AdministradorTelefone = c.String(),
                        AdministradorEndereco = c.String(),
                        AdministradorEmail = c.String(),
                        AdministradorSenha = c.String(),
                    })
                .PrimaryKey(t => t.AdministradorId);
            
            CreateTable(
                "dbo.Automoveis",
                c => new
                    {
                        AutomovelId = c.Int(nullable: false, identity: true),
                        AutomovelMarca = c.String(),
                        AutomovelModelo = c.String(),
                        AutomovelAno = c.Int(nullable: false),
                        AutomovelCor = c.String(),
                        AutomovelPlaca = c.String(),
                        AutomovelUsuarioId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AutomovelId);
            
            CreateTable(
                "dbo.Estares",
                c => new
                    {
                        EstarId = c.Int(nullable: false, identity: true),
                        EstarDataHorario = c.DateTime(nullable: false),
                        EstarHorarioFinal = c.DateTime(nullable: false),
                        EstarRua = c.String(),
                        EstarAutomovel_AutomovelId = c.Int(),
                        EstarUsuario_UsuarioId = c.Int(),
                    })
                .PrimaryKey(t => t.EstarId)
                .ForeignKey("dbo.Automoveis", t => t.EstarAutomovel_AutomovelId)
                .ForeignKey("dbo.Usuarios", t => t.EstarUsuario_UsuarioId)
                .Index(t => t.EstarAutomovel_AutomovelId)
                .Index(t => t.EstarUsuario_UsuarioId);
            
            CreateTable(
                "dbo.Usuarios",
                c => new
                    {
                        UsuarioId = c.Int(nullable: false, identity: true),
                        UsuarioNome = c.String(),
                        UsuarioCpf = c.String(),
                        UsuarioTelefone = c.String(),
                        UsuarioEndereco = c.String(),
                        UsuarioEmail = c.String(),
                        UsuarioSenha = c.String(),
                        UsuarioSaldo = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.UsuarioId);
            
            CreateTable(
                "dbo.HistoricoCompraSaldos",
                c => new
                    {
                        HistoricoCompraSaldoId = c.Int(nullable: false, identity: true),
                        SaldoUsuario = c.Double(nullable: false),
                        NumeroCartao = c.String(),
                        NomeCompletoCartao = c.String(),
                        BandeiraCartao = c.String(),
                        CodigoSegurancaCartao = c.Int(nullable: false),
                        HistoricoCompraSaldoUsuario_UsuarioId = c.Int(),
                    })
                .PrimaryKey(t => t.HistoricoCompraSaldoId)
                .ForeignKey("dbo.Usuarios", t => t.HistoricoCompraSaldoUsuario_UsuarioId)
                .Index(t => t.HistoricoCompraSaldoUsuario_UsuarioId);
            
            CreateTable(
                "dbo.LoginAdmins",
                c => new
                    {
                        LoginAdminId = c.Int(nullable: false, identity: true),
                        LoginAdminSessao = c.String(),
                        LoginAdminAdministrador_AdministradorId = c.Int(),
                    })
                .PrimaryKey(t => t.LoginAdminId)
                .ForeignKey("dbo.Administradores", t => t.LoginAdminAdministrador_AdministradorId)
                .Index(t => t.LoginAdminAdministrador_AdministradorId);
            
            CreateTable(
                "dbo.LoginUsers",
                c => new
                    {
                        LoginUserId = c.Int(nullable: false, identity: true),
                        LoginUserSessao = c.String(),
                        LoginUserUsuario_UsuarioId = c.Int(),
                    })
                .PrimaryKey(t => t.LoginUserId)
                .ForeignKey("dbo.Usuarios", t => t.LoginUserUsuario_UsuarioId)
                .Index(t => t.LoginUserUsuario_UsuarioId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LoginUsers", "LoginUserUsuario_UsuarioId", "dbo.Usuarios");
            DropForeignKey("dbo.LoginAdmins", "LoginAdminAdministrador_AdministradorId", "dbo.Administradores");
            DropForeignKey("dbo.HistoricoCompraSaldos", "HistoricoCompraSaldoUsuario_UsuarioId", "dbo.Usuarios");
            DropForeignKey("dbo.Estares", "EstarUsuario_UsuarioId", "dbo.Usuarios");
            DropForeignKey("dbo.Estares", "EstarAutomovel_AutomovelId", "dbo.Automoveis");
            DropIndex("dbo.LoginUsers", new[] { "LoginUserUsuario_UsuarioId" });
            DropIndex("dbo.LoginAdmins", new[] { "LoginAdminAdministrador_AdministradorId" });
            DropIndex("dbo.HistoricoCompraSaldos", new[] { "HistoricoCompraSaldoUsuario_UsuarioId" });
            DropIndex("dbo.Estares", new[] { "EstarUsuario_UsuarioId" });
            DropIndex("dbo.Estares", new[] { "EstarAutomovel_AutomovelId" });
            DropTable("dbo.LoginUsers");
            DropTable("dbo.LoginAdmins");
            DropTable("dbo.HistoricoCompraSaldos");
            DropTable("dbo.Usuarios");
            DropTable("dbo.Estares");
            DropTable("dbo.Automoveis");
            DropTable("dbo.Administradores");
        }
    }
}
