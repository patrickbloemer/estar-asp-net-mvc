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
    public class AdministradorController : Controller {

        // GET: Administrador
        public ActionResult Index() {
            Administrador a = new Administrador();
            a = LoginAdminDAO.RetornaAdminLogado();
            if (a != null) {
                return View(UsuarioDAO.ListarUsuarios());
            }else {
                return RedirectToAction("Login");
            }
        }

        // GET: Administrador/Details/5
        public ActionResult Detalhes(int? id) {
            Administrador a = new Administrador();
            a = LoginAdminDAO.RetornaAdminLogado();
            if (a != null) {
                if (id == null) {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Administrador administrador = AdministradorDAO.BuscarAdministradorPorId(id);
                if (administrador == null) {
                    return HttpNotFound();
                }
                return View(administrador);
            } else {
                return RedirectToAction("Login");
            }            
        }

        // GET: Administrador/Create
        public ActionResult Criar() {
            Administrador a = new Administrador();
            a = LoginAdminDAO.RetornaAdminLogado();
            if (a == null) {
                return View();
            } else {
                return RedirectToAction("Login");
            }
        }

        // POST: Administrador/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Criar([Bind(Include = "AdministradorId,AdministradorNome,AdministradorCpf,AdministradorTelefone,AdministradorEndereco,AdministradorEmail,AdministradorSenha")] Administrador administrador) {
            if (ModelState.IsValid) {
                Administrador a = new Administrador();
                a = LoginAdminDAO.RetornaAdminLogado();
                if (a == null) {
                    Administrador admin = new Administrador();
                    admin.AdministradorNome = administrador.AdministradorNome;
                    admin.AdministradorCpf = administrador.AdministradorCpf;
                    admin.AdministradorTelefone = administrador.AdministradorTelefone;
                    admin.AdministradorEndereco = administrador.AdministradorEndereco;
                    admin.AdministradorEmail = administrador.AdministradorEmail;
                    admin.AdministradorSenha = administrador.AdministradorSenha;

                    if (AdministradorDAO.VerificaCpfCadastro(admin.AdministradorCpf)) {
                        ViewBag.Mensagem = "CPF já cadastrado";
                        return RedirectToAction("Create");
                    }
                    else if(AdministradorDAO.AdicionarAdministrador(administrador)) {
                        return RedirectToAction("Login");
                    }
                }
            } else {
                return RedirectToAction("Login");
            }
            return View(administrador);

        }

        // GET: Administrador/Edit/5
        public ActionResult Editar(int? id) {
            Administrador a = new Administrador();
            a = LoginAdminDAO.RetornaAdminLogado();
            if (a != null) {
                if (id == null) {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Administrador administrador = AdministradorDAO.BuscarAdministradorPorId(id);
                if (administrador == null) {
                    return HttpNotFound();
                }
                return View(administrador);
            } else {
                return RedirectToAction("Login");
            }
        }

        // POST: Administrador/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar([Bind(Include = "AdministradorId,AdministradorNome,AdministradorCpf,AdministradorTelefone,AdministradorEndereco,AdministradorEmail,AdministradorSenha")] Administrador administrador) {
            if (ModelState.IsValid) {
                Administrador a = new Administrador();
                a = LoginAdminDAO.RetornaAdminLogado();
                if (a != null) {
                    Administrador adminAux = AdministradorDAO.BuscarAdministradorPorId(administrador.AdministradorId);
                    adminAux.AdministradorNome = administrador.AdministradorNome;
                    adminAux.AdministradorTelefone = administrador.AdministradorTelefone;
                    adminAux.AdministradorEndereco = administrador.AdministradorEndereco;
                    adminAux.AdministradorEmail = administrador.AdministradorEmail;
                    adminAux.AdministradorSenha = administrador.AdministradorSenha;

                    if (AdministradorDAO.AtualizarAdministrador(adminAux)) {
                        return RedirectToAction("Index");
                    }
                }
                return View(administrador);
            } else {
                return RedirectToAction("Login");
            }
        }

        // GET: Administrador/Delete/5
        public ActionResult Deletar(int? id) {
            Administrador a = new Administrador();
            a = LoginAdminDAO.RetornaAdminLogado();
            if (a != null) {
                if (id == null) {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Administrador administrador = AdministradorDAO.BuscarAdministradorPorId(id);
                if (administrador == null) {
                    return HttpNotFound();
                }
                return View(administrador);
            } else {
                return RedirectToAction("Login");
            }


        }

        // POST: Administrador/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id) {
            Administrador a = new Administrador();
            a = LoginAdminDAO.RetornaAdminLogado();
            if (a != null) {
                Administrador administrador = AdministradorDAO.BuscarAdministradorPorId(id);
                AdministradorDAO.RemoverAdministrador(administrador);
                return RedirectToAction("Index");
            } else {
                return RedirectToAction("Login");
            }
        }


        //GET:LOGIN
        public ActionResult Login() {
            Administrador a = new Administrador();
            a = LoginAdminDAO.RetornaAdminLogado();
            if (a != null) {
                return RedirectToAction("Index");
            } else {
                return View();
            }
        }

        // POST: Usuario/Login
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "AdministradorCpf,AdministradorSenha")] Administrador administrador) {
            Administrador a = new Administrador();
            if (ModelState.IsValid) {
                a.AdministradorCpf = administrador.AdministradorCpf;
                a.AdministradorSenha = administrador.AdministradorSenha;

                a = AdministradorDAO.LoginAdministrador(a);

                if (a != null) { // DIFERENTE DE 0 ENTAO É A ID DO USUARIO
                    LoginAdminDAO.AdicionaLoginAdmin(a);
                    return RedirectToAction("Index");
                } else {
                    ModelState.AddModelError("", "CPF e/ou Senha Inválido(s)");
                }
            }
            return View();
        }

        public ActionResult Logoff() {
            Guid guid = Guid.NewGuid();
            Session["Sessao"] = guid.ToString();
            return RedirectToAction("Login");
        }

    }
}
