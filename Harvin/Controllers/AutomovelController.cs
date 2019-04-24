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
    public class AutomovelController : Controller {

        // GET: Automovel
        public ActionResult Index() {
            if (LoginAdminDAO.RetornaAdminLogado() != null) {
                return View(AutomovelDAO.ListarAutomoveis());
            }else {
                return RedirectToAction("Login", "Administrador");
            }
        }

        // GET: Automovel/Details/5
        public ActionResult Detalhes(int? id) {
            if (LoginAdminDAO.RetornaAdminLogado() != null) {
                if (id == null) {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Automovel automovel = AutomovelDAO.BuscarAutomovelPorId(id);
                if (automovel == null) {
                    return HttpNotFound();
                }
                return View(automovel);
            }else if(UsuarioDAO.RetornarUsuarioLogado() != null) {
                if (id == null) {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Automovel automovel = AutomovelDAO.BuscarAutomovelPorId(id);
                if (automovel == null) {
                    return HttpNotFound();
                }
                return View(automovel);
            }else {
                return RedirectToAction("Login", "Usuario");
            }
           
        }

        // GET: Automovel/Create
        public ActionResult Criar() {
            if (UsuarioDAO.RetornarUsuarioLogado() != null) {
                return View();
            }else {
                return RedirectToAction("Login", "Usuario");
            }
        }

        // POST: Automovel/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Criar([Bind(Include = "AutomovelId,AutomovelMarca,AutomovelModelo,AutomovelAno,AutomovelCor,AutomovelPlaca, AutomovelRenavan")] Automovel automovel) {
            if (ModelState.IsValid) {
                if (UsuarioDAO.RetornarUsuarioLogado() != null) {
                    if (AutomovelDAO.VerificarPlaca(automovel.AutomovelPlaca)) {
                        ViewBag.Mensagem = "Placa já cadastrada";
                        return View();
                    }else {
                        Usuario u = new Usuario();
                        u = UsuarioDAO.RetornarUsuarioLogado();
                        automovel.AutomovelUsuarioId = u.UsuarioId;
                        if (AutomovelDAO.AdicionarAutomovel(automovel)) {
                            return RedirectToAction("Index", "Usuario");
                        } else {
                            return RedirectToAction("Criar");
                        }
                    }
                }
            }
            return View(automovel);
        }

        // GET: Automovel/Edit/5
        public ActionResult Editar(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Automovel automovel = AutomovelDAO.BuscarAutomovelPorId(id);
            if (automovel == null) {
                return HttpNotFound();
            }

            Usuario u = new Usuario();
            u = UsuarioDAO.RetornarUsuarioLogado();
            Automovel a = new Automovel();
            a = AutomovelDAO.BuscarAutomovelPorId(id);

            if (u != null) {
                if (a.AutomovelUsuarioId.Equals(u.UsuarioId)) {
                    return View(a);
                } else {
                    return RedirectToAction("SeusVeiculos", "Automovel");
                }
            }
            return RedirectToAction("Login", "Usuario");
        }

        // POST: Automovel/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar([Bind(Include = "AutomovelId,AutomovelMarca,AutomovelModelo,AutomovelAno,AutomovelCor,AutomovelPlaca, AutomovelRenavan")] Automovel automovel) {
            if (ModelState.IsValid) {
                Automovel carroAux = AutomovelDAO.BuscarAutomovelPorId(automovel.AutomovelId);
                carroAux.AutomovelMarca = automovel.AutomovelMarca;
                carroAux.AutomovelModelo = automovel.AutomovelModelo;
                carroAux.AutomovelAno = automovel.AutomovelAno;
                carroAux.AutomovelCor = automovel.AutomovelCor;
                carroAux.AutomovelRenavan = automovel.AutomovelRenavan;
                if (AutomovelDAO.AtualizarAutomovel(carroAux)) {
                    Usuario u = new Usuario();
                    u = UsuarioDAO.RetornarUsuarioLogado();
                    if (u != null) {
                        return RedirectToAction("SeusVeiculos", "Automovel");
                    }else {
                        return RedirectToAction("Login", "Usuario");
                    }
                }
            }
            return View(automovel);
        }
        // GET: Automovel/Delete/5
        public ActionResult Delete(int? id) {
            if (UsuarioDAO.RetornarUsuarioLogado() != null) {
                if (id == null) {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Automovel automovel = AutomovelDAO.BuscarAutomovelPorId(id);
                if (automovel == null) {
                    return HttpNotFound();
                }

                foreach (Estar temp in EstarDAO.ListarEstares()) { 
                    if (temp.EstarAutomovel.AutomovelId.Equals(id)) {
                        if (DateTime.Now > temp.EstarDataHorario && DateTime.Now < temp.EstarHorarioFinal) {
                            return RedirectToAction("SeusVeiculos", "Automovel");
                        }
                    }
                }

                return View(automovel);
                
            }
            return RedirectToAction("Login", "Usuario");
        }

        // POST: Automovel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id) {
            Automovel automovel = AutomovelDAO.BuscarAutomovelPorId(id);
            AutomovelDAO.RemoverAutomovel(automovel);
            return RedirectToAction("SeusVeiculos", "Automovel");
        }


        // GET: Automovel/SeusVeiculos
        public ActionResult SeusVeiculos() {
            Usuario u = new Usuario();
            u = UsuarioDAO.RetornarUsuarioLogado();
            if (u == null) {
                return RedirectToAction("Login", "Usuario");
            }
            return View(u);
        }
    }
}
