using Eccomerce.DAO;
using Harvin.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Harvin.DAO {
    public class AdministradorDAO {
        private static Entities entities = Singleton.Instance.Entities;

        // Add admin
        public static bool AdicionarAdministrador(Administrador administrador) {
            try {
                entities.Administradores.Add(administrador);
                entities.SaveChanges();
                return true;
            } catch (Exception e) {
                return false;
            }
        }

        // Login Admin
        public static Administrador LoginAdministrador(Administrador administrador) {
            try {
                foreach (Administrador temp in entities.Administradores.ToList()) {
                    if (temp.AdministradorCpf.Equals(administrador.AdministradorCpf)) {
                        if (temp.AdministradorSenha.Equals(administrador.AdministradorSenha)) {
                            return temp;
                        }
                    }
                }
                return null;
            } catch (Exception e) {
                return null;
            }
        }

        // Lista dos admins
        public static List<Administrador> ListarAdministradores() {
            return entities.Administradores.ToList();
        }

        public static Administrador BuscarAdministradorPorId(int? id) {
            return entities.Administradores.Find(id);
        }

        // Editar admin
        public static bool AtualizarAdministrador(Administrador administrador) {
            try {
                entities.Entry(administrador).State = EntityState.Modified;
                entities.SaveChanges();
                return true;
            } catch (Exception e) {
                return false;
            }
        }

        // Remover Admin
        public static bool RemoverAdministrador(Administrador administrador) {
            try {
                entities.Administradores.Remove(administrador);
                entities.SaveChanges();
                return true;
            } catch (Exception) {
                return false;
            }
        }

        //VERIFICA CPF CADASTRADO
        public static bool VerificaCpfCadastro(string cpf) {
            try {
                foreach (Administrador temp in ListarAdministradores()) {
                    if (temp.AdministradorCpf.Equals(cpf)) {
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