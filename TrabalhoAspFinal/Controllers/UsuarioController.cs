using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TrabalhoAspFinal.Models;

namespace TrabalhoAspFinal.Controllers
{
    public class UsuarioController : Controller
    {
        private TrabalhoAspFinalContext db = new TrabalhoAspFinalContext();

        // GET: /Usuario/
        public ActionResult Index()
        {
            //var usuarios = db.Usuarios.Include(u => u.pessoa);
            return View(db.Usuarios.ToList());
        }

        // GET: /Usuario/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // GET: /Usuario/Create
        public ActionResult Create()
        {
            ViewBag.PermissaoId = new SelectList(db.Permissaos.ToList(), "PermissaoId", "PermissaoNome");
            ViewBag.PessoaId = new SelectList(db.Pessoas, "PessoaId", "Email");

            ReportViewer relatorio = new ReportViewer();
            relatorio.ProcessingMode = ProcessingMode.Local;
            relatorio.LocalReport.ReportPath = Request.MapPath("~/Reports/ContatosAtuais.rdlc");
            relatorio.LocalReport.DataSources.Add(new ReportDataSource("ContatosAtuais", db.Pessoas.OrderBy(x =>x.Nome).ToList()));
            relatorio.SizeToReportContent = true;

            ViewBag.Relatorio = relatorio;
            return View();
        }

        // POST: /Usuario/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UsuarioId,Login,Senha,PessoaId,ConfirmacaoSenha")] Usuario usuario, int PermissaoId)
        {
            //ViewBag.PermissaoId = db.Permissaos.ToList();
            //ViewBag.PessoaId = new SelectList(db.Pessoas, "PessoaId", "Nome", usuario.PessoaId);
            Pessoa p = new Pessoa();
            if(PermissaoId == 1)
            {
                usuario.PessoaId = 0;
            }
            if (ModelState.IsValid)
            {
                usuario.Permissoes.Add(db.Permissaos.Find(PermissaoId));
                db.Usuarios.Add(usuario);
                db.SaveChanges();
                return RedirectToAction("Login");
            }
            Create();
            //else
            //{
            //    usuario.Permissoes.Add(db.Permissaos.Find(PermissaoId));
            //    db.Usuarios.Add(usuario);
            //    db.SaveChanges();
            //    return RedirectToAction("Login");
            //}
            return View(usuario);
        }

        // GET: /Usuario/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            ViewBag.PessoaId = new SelectList(db.Pessoas, "PessoaId", "Nome", usuario.PessoaId);
            return View(usuario);
        }

        // POST: /Usuario/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UsuarioId,Login,Senha,PessoaId")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PessoaId = new SelectList(db.Pessoas, "PessoaId", "Nome", usuario.PessoaId);
            return View(usuario);
        }

        // GET: /Usuario/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: /Usuario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Usuario usuario = db.Usuarios.Find(id);
            db.Usuarios.Remove(usuario);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Usuario u, bool PermanecerConectado)
        {
            u = db.Usuarios.FirstOrDefault(x => x.Login.Equals(u.Login) &&
                x.Senha.Equals(u.Senha));
            if (u != null)
            {
                FormsAuthentication.SetAuthCookie(u.Login, PermanecerConectado);
                return RedirectToAction("Index", "Pessoa");
            }
            else
            {
                ModelState.AddModelError("", "Usuário ou senha inválido!");
                return View();
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Pessoa");
        }

        public ActionResult Negado()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
