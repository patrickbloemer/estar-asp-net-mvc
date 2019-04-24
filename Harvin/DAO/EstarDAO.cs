using Eccomerce.DAO;
using Harvin.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Harvin.DAO {
    public class EstarDAO {
        private static Entities entities = Singleton.Instance.Entities;

        // Adicionar EstaR
        public static bool AdicionarEstar(Estar estar) {
            try {
                entities.Estares.Add(estar);
                entities.SaveChanges();
                return true;
            } catch (Exception e) {
                return false;
            }
        }

        // Lista de EstaRes
        public static List<Estar> ListarEstares() {
            return entities.Estares.Include("EstarAutomovel").ToList();
        }

        // Buscar por Id
        public static Estar BuscarEstarPorId(int? id) {
            return entities.Estares.Include("EstarUsuario").Include("EstarAutomovel").FirstOrDefault(x => x.EstarId == id);
        }

        // Editar EstaR
        public static bool AtualizarEstar(Estar estar) {
            try {
                entities.Entry(estar).State = EntityState.Modified;
                entities.SaveChanges();
                return true;
            } catch (Exception e) {
                return false;
            }
        }

        // Remover EstaR
        public static bool RemoverEstar(Estar estar) {
            try {
                entities.Estares.Remove(estar);
                entities.SaveChanges();
                return true;
            } catch (Exception) {
                return false;
            }
        }

        //VERIFICA CARRO ESTACIONADO
        public static Estar VerificaCarroEstacionado(int id) {
            try {
                Usuario u = new Usuario();
                u = UsuarioDAO.RetornarUsuarioLogado();
                return entities.Estares.FirstOrDefault(x => x.EstarAutomovel.AutomovelId == id && x.EstarUsuario.UsuarioId == u.UsuarioId && x.EstarDataHorario < DateTime.Now && x.EstarHorarioFinal > DateTime.Now);
                } catch (Exception) {
                return null;
            }
        }
    }
}