using Eccomerce.DAO;
using Harvin.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Harvin.DAO {
    public class AutomovelDAO {
        private static Entities entities = Singleton.Instance.Entities;

        // Adicionar automóvel
        public static bool AdicionarAutomovel(Automovel automovel) {
            try {
                entities.Automoveis.Add(automovel);
                entities.SaveChanges();
                return true;
            } catch (Exception e) {
                return false;
            }
        }

        // Lista de automoveis
        public static List<Automovel> ListarAutomoveis() {
            return entities.Automoveis.ToList();
        }

        // Buscar por Id
        public static Automovel BuscarAutomovelPorId(int? id) {
            return entities.Automoveis.Find(id);
        }

        // Editar automovel
        public static bool AtualizarAutomovel(Automovel automovel) {
            try {
                entities.Entry(automovel).State = EntityState.Modified;
                entities.SaveChanges();
                return true;
            } catch (Exception e) {
                return false;
            }
        }

        // Remover Automovel
        public static bool RemoverAutomovel(Automovel automovel) {
            try {
                entities.Automoveis.Remove(automovel);
                entities.SaveChanges();
                return true;
            } catch (Exception) {
                return false;
            }
        }

        // Verificar Placa
        public static bool VerificarPlaca(string placa) {
            try {
                foreach (Automovel temp in ListarAutomoveis()) {
                    if (temp.AutomovelPlaca.Equals(placa)) {
                        return true;
                    }
                }
                return false;
            } catch (Exception e){
                return false;
            }

        }

    }
}