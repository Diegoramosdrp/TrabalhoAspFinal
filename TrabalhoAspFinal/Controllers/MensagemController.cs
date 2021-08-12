using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TrabalhoAspFinal.Models;

namespace TrabalhoAspFinal.Controllers
{
    public class MensagemController : Controller
    {
        private TrabalhoAspFinalContext db = new TrabalhoAspFinalContext();
        private static Pessoa pessoa;

        // GET: /Mensagem/
        public ActionResult Index()
        {
            return View(db.MensagemContatoes.ToList());
        }

        // GET: /Mensagem/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MensagemContato mensagemcontato = db.MensagemContatoes.Find(id);
            if (mensagemcontato == null)
            {
                return HttpNotFound();
            }
            return View(mensagemcontato);
        }

        // GET: /Mensagem/Create
        public ActionResult Create(int? id)
        {
            pessoa = db.Pessoas.Find(id);
            ViewBag.Pessoa = pessoa;
            ViewBag.Mensagem = db.MensagemContatoes.Where(x => x.ContatoId == pessoa.PessoaId).ToList();
            return View();
        }

        // POST: /Mensagem/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="MensagemId,Mensagem,ContatoId")] MensagemContato mensagemcontato)
        {
            pessoa = db.Pessoas.Find(pessoa.PessoaId);
            ViewBag.Pessoa = pessoa;
            ViewBag.Mensagem = db.MensagemContatoes.Where(x => x.ContatoId == pessoa.PessoaId).ToList();
            if (ModelState.IsValid)
            {
                mensagemcontato.Pessoa = pessoa;
                mensagemcontato.ContatoId = pessoa.PessoaId;
                db.MensagemContatoes.Add(mensagemcontato);
                db.SaveChanges();
                return RedirectToAction("Create", new { id = pessoa.PessoaId });
            }

            return View(mensagemcontato);
        }

        // GET: /Mensagem/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MensagemContato mensagemcontato = db.MensagemContatoes.Find(id);
            if (mensagemcontato == null)
            {
                return HttpNotFound();
            }
            return View(mensagemcontato);
        }

        // POST: /Mensagem/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="MensagemId,Mensagem,ContatoId")] MensagemContato mensagemcontato)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mensagemcontato).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mensagemcontato);
        }

        // GET: /Mensagem/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MensagemContato mensagemcontato = db.MensagemContatoes.Find(id);
            if (mensagemcontato == null)
            {
                return HttpNotFound();
            }
            return View(mensagemcontato);
        }

        // POST: /Mensagem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MensagemContato mensagemcontato = db.MensagemContatoes.Find(id);
            db.MensagemContatoes.Remove(mensagemcontato);
            db.SaveChanges();
            return RedirectToAction("Index");
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
