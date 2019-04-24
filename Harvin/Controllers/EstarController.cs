using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Harvin.Models;
using Harvin.DAO;

namespace Harvin.Controllers {
    public class EstarController : Controller {
        private static Automovel a = new Automovel();


        // GET: Estar
        public ActionResult Index() {
            if (LoginAdminDAO.RetornaAdminLogado() != null) {
                ViewBag.Mensagem = "O carro selecionado já está estacionado"; 
                return View(EstarDAO.ListarEstares());
            }else {
                return RedirectToAction("Login", "Administrador");
            }
        }

        // GET: Estar/Details/5
        public ActionResult Detalhes(int? id) {
            if (LoginAdminDAO.RetornaAdminLogado() != null) {
                if (id == null) {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Estar estar = EstarDAO.BuscarEstarPorId(id);
                if (estar == null) {
                    return HttpNotFound();
                }
                return View(estar);
            }else {
                return RedirectToAction("Login", "Administrador");
            }


        }

        // GET: Estar/Create
        public ActionResult Criar(Automovel automovel) {
            //PROCURA QUAL O AUTOMOVEL
            if (UsuarioDAO.RetornarUsuarioLogado() != null) {
                Estar e = new Estar();

                //VERIFICA SE AUTOMOVEL JÁ ESTÁ ESTACIONADO
                if (EstarDAO.VerificaCarroEstacionado(automovel.AutomovelId) != null) {
                    return RedirectToAction("Index", "Usuario");
                }
                a = automovel;
                e.EstarAutomovel = automovel;
                e.EstarDataHorario = DateTime.Now;
                e.EstarHorarioFinal = e.EstarDataHorario.AddHours(1);
                return View(e);
            }else {
                return RedirectToAction("Login", "Usuario");
            }
        }

        // POST: Estar/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Criar() {
            if (ModelState.IsValid) {
                Estar e = new Estar();
                e.EstarDataHorario = DateTime.Now;
                e.EstarHorarioFinal = e.EstarDataHorario.AddHours(1);
                e.EstarAutomovel = AutomovelDAO.BuscarAutomovelPorId(a.AutomovelId);
                e.EstarUsuario = UsuarioDAO.RetornarUsuarioLogado();

                if (e.EstarUsuario.UsuarioSaldo < 2) {
                    ViewBag.Mensagem = "Saldo Insuficiente";
                    return View();
                }else {
                    if (EstarDAO.AdicionarEstar(e)) {
                        Usuario u = new Usuario();
                        u = UsuarioDAO.BuscarUsuarioPorId(e.EstarUsuario.UsuarioId);
                        u.UsuarioSaldo = u.UsuarioSaldo - 2;

                        if (UsuarioDAO.AtualizarUsuario(u)) {
                            return RedirectToAction("Index", "Usuario");
                        }
                    } else {
                        ModelState.AddModelError("", "Erro ao Estacionar o Automóvel - Banco de Dados");
                        return RedirectToAction("Create");
                    }
                }
            }
            return RedirectToAction("Index", "Usuario");
        }

        //// GET: Estar/Edit/5
        //public ActionResult Edit(int? id) {
        //    if (id == null) {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Estar estar = EstarDAO.BuscarEstarPorId(id);
        //    if (estar == null) {
        //        return HttpNotFound();
        //    }
        //    return View(estar);
        //}

        // POST: Estar/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "EstarId, EstarData,EstarHorario,EstarRua,EstarAtivo")] Estar estar, int UsuarioId, int AutomovelId) {
        //    if (ModelState.IsValid) {
        //        Estar estarAux = EstarDAO.BuscarEstarPorId(estar.EstarId);
        //        estarAux.EstarUsuario = estar.EstarUsuario;
        //        estarAux.EstarAutomovel = AutomovelDAO.BuscarAutomovelPorId(AutomovelId);
        //        estarAux.EstarUsuario = UsuarioDAO.BuscarUsuarioPorId(UsuarioId);
        //        estarAux.EstarRua = estar.EstarRua;
        //        estarAux.EstarDataHorario = DateTime.Now;
        //        estarAux.EstarHorarioFinal = estarAux.EstarHorarioFinal.AddHours(1);
        //        estarAux.EstarAtivo = estar.EstarAtivo;

        //        if (EstarDAO.AtualizarEstar(estarAux)) {
        //            return RedirectToAction("Index");
        //        }
        //    }
        //    return View(estar);
        //}

        //// GET: Estar/Delete/5
        //public ActionResult Delete(int? id) {
        //    if (id == null) {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Estar estar = EstarDAO.BuscarEstarPorId(id);
        //    if (estar == null) {
        //        return HttpNotFound();
        //    }
        //    return View(estar);
        //}

        //// POST: Estar/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id) {
        //    Estar estar = EstarDAO.BuscarEstarPorId(id);
        //    EstarDAO.RemoverEstar(estar);
        //    return RedirectToAction("Index");
        //}

    }
}
