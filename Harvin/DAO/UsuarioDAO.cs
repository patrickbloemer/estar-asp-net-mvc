using Eccomerce.DAO;
using Harvin.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Harvin.DAO {
    public class UsuarioDAO {

        private static Entities entities = Singleton.Instance.Entities;

        public static bool AdicionarUsuario(Usuario usuario) {
            try {
                entities.Usuarios.Add(usuario);
                entities.SaveChanges();
                return true;
            } catch (Exception e) {
                return false;
            }
        }

        public static Usuario LoginUsuario(Usuario usuario) {
            try {
                foreach (Usuario temp in entities.Usuarios.ToList()) {
                    if (temp.UsuarioCpf.Equals(usuario.UsuarioCpf)) {
                        if (temp.UsuarioSenha.Equals(usuario.UsuarioSenha)) {
                            return temp;
                        }
                    }
                }
                return null;
            } catch (Exception e) {
                return null;
            }
        }

        //LISTAR
        public static List<Usuario> ListarUsuarios() {
            return entities.Usuarios.ToList();
        }

        //USCAR POR ID
        public static Usuario BuscarUsuarioPorId(int? id) {
            try {
                return entities.Usuarios.Find(id);
            } catch (Exception e) {
                return null;
            }
        }

        // Editar
        public static bool AtualizarUsuario(Usuario usuario) {

            try {
                entities.Entry(usuario).State = EntityState.Modified;
                entities.SaveChanges();
                return true;
            } catch (Exception e) {
                return false;
            }
        }

        // Remover
        public static bool RemoverUsuario(Usuario usuario) {
            try {
                entities.Usuarios.Remove(usuario);
                entities.SaveChanges();
                return true;
            } catch (Exception e) {
                return false;
            }
        }

        //RETORNAR USUARIO LOGADO
        public static Usuario RetornarUsuarioLogado() {
            try {
                foreach (LoginUser temp in LoginUserDAO.RetornarListaLoginsUsers()) {
                    if (temp.LoginUserSessao.Equals(LoginUserDAO.RetornarIdSessao())) {
                        foreach (Usuario user in ListarUsuarios()) {
                            if (temp.LoginUserUsuario.UsuarioId.Equals(user.UsuarioId)) {
                                return user;
                            }
                        }
                    }
                }
                return null;
            } catch (Exception e) {
                return null;
            }
        }


        //VERIFICA CPF EXISTENTE
        public static bool VerificaCpfCadastrado(string cpf) {
            try {
                foreach (Usuario temp in ListarUsuarios()) {
                    if (temp.UsuarioCpf.Equals(cpf)) {
                        return true;
                    }
                }
                return false;
            } catch (Exception) {
                return false;
            }
        }
    }
}