using Eccomerce.DAO;
using Harvin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Harvin.DAO {
    public class HistoricoCompraSaldoDAO {

        private static Entities entities = Singleton.Instance.Entities;

        // Adicionar EstaR
        public static bool AdicionarHistorico(HistoricoCompraSaldo historico) {
            try {
                entities.HistoricoCompraSaldos.Add(historico);
                entities.SaveChanges();
                return true;
            } catch (Exception e) {
                return false;
            }
        }
    }
}