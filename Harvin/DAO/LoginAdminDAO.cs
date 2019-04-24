using Eccomerce.DAO;
using Harvin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Harvin.DAO {
    public class LoginAdminDAO {
        private static Entities entities = Singleton.Instance.Entities;

        public static bool AdicionaLoginAdmin(Administrador admin) {
            try {
                LoginAdmin login = new LoginAdmin();
                login.LoginAdminAdministrador = admin;
                login.LoginAdminSessao = RetornarIdSessao();
                entities.LoginAdmins.Add(login);
                entities.SaveChanges();
                return true;
            } catch (Exception e) {
                return false;
            }
        }

        //LISTAR TODOS
        public static List<LoginAdmin> ListarLoginAdmin() {
            try {
                return entities.LoginAdmins.ToList();
            } catch (Exception e) {
                return null;
            }
        }

        //RETORNAR ADMINISTRADOR LOGADO
        public static Administrador RetornaAdminLogado() {
            try {
                foreach (LoginAdmin temp in ListarLoginAdmin()) {
                    if (temp.LoginAdminSessao.Equals(RetornarIdSessao())) {
                        foreach (Administrador tempAdmin in AdministradorDAO.ListarAdministradores()) {
                            if (temp.LoginAdminAdministrador.AdministradorId.Equals(tempAdmin.AdministradorId)) {
                                return tempAdmin;
                            }
                        }
                    }
                }
                return null;
            } catch (Exception e) {
                return null;
            }
        }

        //RETORNA OU GERA ID PRA SESSão
        public static string RetornarIdSessao() {
            if (HttpContext.Current.Session["Sessao"] == null) {
                //ESTE GUID GERA UMA SERIE ALFANUMERICA UNICA PARA CADA CARRINHO
                Guid guid = Guid.NewGuid();
                HttpContext.Current.Session["Sessao"] = guid.ToString();
            }
            return HttpContext.Current.Session["Sessao"].ToString();
        }


    }
}