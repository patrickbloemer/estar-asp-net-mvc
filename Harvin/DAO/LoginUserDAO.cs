using Eccomerce.DAO;
using Harvin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Harvin.DAO {
    public class LoginUserDAO {
        private static Entities entities = Singleton.Instance.Entities;

        public static bool AdicionaLogin(Usuario usuario) {
            try {
                LoginUser login = new LoginUser();
                login.LoginUserUsuario = usuario;
                login.LoginUserSessao = RetornarIdSessao();
                entities.LoginUsers.Add(login);
                entities.SaveChanges();
                return true;
            } catch (Exception e){
                return false;
            }
        }   

        //RETORNAR LISTA DE LOGINS
        public static List<LoginUser> RetornarListaLoginsUsers() {
            try {
                return entities.LoginUsers.ToList();
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