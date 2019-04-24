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
using System.Web;


namespace Harvin.Controllers {
    public class UsuarioController : Controller {
        // GET: Usuario
        public ActionResult Index() {
            Usuario u = new Usuario();
            u = UsuarioDAO.RetornarUsuarioLogado();

            if (u != null) {
                return View(u);
            } else {
                ModelState.AddModelError("", "É necessário fazer Login para acessar o site");
            }
            return RedirectToAction("Login");
        }

        // GET: Usuario/Details/5
        public ActionResult Detalhes(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = UsuarioDAO.BuscarUsuarioPorId(id);
            if (usuario == null) {
                return HttpNotFound();
            }
            return View(usuario);
        }

        public ActionResult Login() {
            return View();
        }

        // POST: Usuario/Login
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "UsuarioCpf,UsuarioSenha")] Usuario usuario) {
            Usuario u = new Usuario();
            if (ModelState.IsValid) {
                u.UsuarioCpf = usuario.UsuarioCpf;
                u.UsuarioSenha = usuario.UsuarioSenha;

                u = UsuarioDAO.LoginUsuario(u);

                Guid guid = Guid.NewGuid();
                Session["Sessao"] = guid.ToString();


                if (u != null) { // DIFERENTE DE 0 ENTAO É A ID DO USUARIO
                    LoginUserDAO.AdicionaLogin(u);
                    return RedirectToAction("Index");
                } else {
                    ModelState.AddModelError("", "CPF e/ou Senha Inválido(s)");
                }
            }
            return View(usuario);
        }

        // GET: Usuario/Create
        public ActionResult Cadastro() {
            return View();
        }

        // POST: Usuario/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastro([Bind(Include = "UsuarioId,UsuarioNome,UsuarioCpf,UsuarioTelefone,UsuarioEndereco,UsuarioEmail,UsuarioSenha,UsuarioSaldo")] Usuario usuario) {
            if (ModelState.IsValid) {
                Usuario u = new Usuario();
                u.UsuarioNome = usuario.UsuarioNome;
                u.UsuarioCpf = usuario.UsuarioCpf;
                u.UsuarioTelefone = usuario.UsuarioTelefone;
                u.UsuarioEndereco = usuario.UsuarioEndereco;
                u.UsuarioEndereco = usuario.UsuarioEmail;
                u.UsuarioSenha = usuario.UsuarioSenha;
                u.UsuarioSaldo = 0;

                if (UsuarioDAO.VerificaCpfCadastrado(u.UsuarioCpf)) {
                    ViewBag.Mensagem = "Cpf Já cadastrado";
                    return View();
                }else {
                    if (UsuarioDAO.AdicionarUsuario(usuario)) {
                        return RedirectToAction("Login");
                    } else {
                        return RedirectToAction("Cadastro");
                    }
                }
            }

            return View(usuario);
        }

        // GET: Usuario/Edit/5
        public ActionResult Editar(int? id) {
            if (id.Equals(null)) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = UsuarioDAO.BuscarUsuarioPorId(id);
            if (usuario == null) {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuario/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar([Bind(Include = "UsuarioId,UsuarioNome,UsuarioCpf,UsuarioTelefone,UsuarioEndereco,UsuarioEmail,UsuarioSenha,UsuarioSaldo")] Usuario usuario) {
            if (ModelState.IsValid) {
                Usuario usuarioAux = UsuarioDAO.BuscarUsuarioPorId(usuario.UsuarioId);
                usuarioAux.UsuarioNome = usuario.UsuarioNome;
                usuarioAux.UsuarioEmail = usuario.UsuarioEmail;
                usuarioAux.UsuarioEndereco = usuario.UsuarioEndereco;
                usuarioAux.UsuarioTelefone = usuario.UsuarioTelefone;
                usuarioAux.UsuarioSenha = usuario.UsuarioSenha;

                if (UsuarioDAO.AtualizarUsuario(usuarioAux)) {
                    return RedirectToAction("Index");
                }
            }
            return View(usuario);
        }

        // GET: Usuario/Delete/5
        public ActionResult Deletar(int? id) {

            if (LoginAdminDAO.RetornaAdminLogado() != null) {
                if (id == null) {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Usuario usuario = UsuarioDAO.BuscarUsuarioPorId(id);
                if (usuario == null) {
                    return HttpNotFound();
                }
                return View(usuario);
            } else {
                return RedirectToAction("Login", "Administrador");
            }
        }

        // POST: Usuario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            Usuario usuario = UsuarioDAO.BuscarUsuarioPorId(id);
            if (UsuarioDAO.RemoverUsuario(usuario)) {
                return RedirectToAction("IndexConfig");
            }
            return View(usuario);
        }


        // Inseri agora

        public ActionResult Saldo() {
            if (UsuarioDAO.RetornarUsuarioLogado() != null) {
                return View();
            }else {
                return RedirectToAction("Login");
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Saldo([Bind(Include = "HistoricoCompraSaldoId, SaldoUsuario, NumeroCartao, NomeCompletoCartao, BandeiraCartao, CodigoSegurancaCartao")] HistoricoCompraSaldo historico) {
            if (UsuarioDAO.RetornarUsuarioLogado() != null) {
                if (ModelState.IsValid) {
                    Usuario u = new Usuario();
                    u = UsuarioDAO.RetornarUsuarioLogado();
                    HistoricoCompraSaldo hist = new HistoricoCompraSaldo();
                    hist.HistoricoCompraSaldoUsuario = u;
                    hist.SaldoUsuario = historico.SaldoUsuario;
                    hist.NomeCompletoCartao = historico.NomeCompletoCartao;
                    hist.NumeroCartao = historico.NumeroCartao;
                    hist.BandeiraCartao = historico.BandeiraCartao;
                    hist.CodigoSegurancaCartao = historico.CodigoSegurancaCartao;

                    if (HistoricoCompraSaldoDAO.AdicionarHistorico(hist)) {
                        Usuario user = new Usuario();
                        user = UsuarioDAO.BuscarUsuarioPorId(u.UsuarioId);
                        user.UsuarioSaldo = hist.SaldoUsuario + user.UsuarioSaldo;
                        if (UsuarioDAO.AtualizarUsuario(user)) {
                            return RedirectToAction("Index");
                        } else {
                            return RedirectToAction("Saldo");
                        }
                    } else {
                        return RedirectToAction("Saldo");
                    }
                } else {
                    return View();
                }
            }else {
                return RedirectToAction("Login");
            }            
        }


        // GET: UsuarioConfiguração
        public ActionResult IndexConfig() {
            Administrador admin = new Administrador();
            admin = LoginAdminDAO.RetornaAdminLogado();
            if (admin != null) {
                return View(UsuarioDAO.ListarUsuarios());
            } else {
                ModelState.AddModelError("", "É necessário fazer Login para acessar o site");
            }
            return RedirectToAction("Login");
        }



        public ActionResult Logoff() {
            Guid guid = Guid.NewGuid();
            Session["Sessao"] = guid.ToString();
            return RedirectToAction("Login");
        }
    }
}