using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TrabalhoAspFinal.Models;

namespace TrabalhoAspFinal.Controllers
{
    public class PessoaController : Controller
    {
        private TrabalhoAspFinalContext db = new TrabalhoAspFinalContext();

        public ActionResult Index(int? id)
        {
            int QuantidadeContatos = db.Pessoas.Count();
            int Paginacao;

            if(QuantidadeContatos %5 == 0)
            {
                Paginacao = QuantidadeContatos / 5;
            }
            else
            {
                Paginacao = (QuantidadeContatos / 5) + 1;
            }
            ViewBag.Paginacao = Paginacao;

            if(id != null)
            {
                int Anterior = (int)(id - 1) * 5;
                return View(db.Pessoas.OrderBy(x => x.Nome).Skip(Anterior).Take(5).ToList());
            }
            return View(db.Pessoas.OrderBy(x => x.Nome).Take(5).ToList());
            //List<Pessoa> Lista = new List<Pessoa>();
            //Lista = db.Pessoas.ToList();
            //return View(Lista.OrderBy(x => x.Nome));
        }

        [AutenticarFiltro(Roles = "Administrador")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pessoa pessoa = db.Pessoas.Find(id);
            if (pessoa == null)
            {
                return HttpNotFound();
            }
            return View(pessoa);
        }

        [AutenticarFiltro(Roles = "Administrador")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PessoaId,Nome,Idade,Telefone,Endereco,Email,Imagem,Favorita,Login,Senha,Administrador")] Pessoa pessoa, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var filePath = Path.Combine(Server.MapPath("~/Images"), fileName);

                    file.SaveAs(filePath);
                    pessoa.Imagem = fileName;
                }
                else
                {
                    pessoa.Imagem = "Sem-Foto.jpg";
                }

                db.Pessoas.Add(pessoa);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = pessoa.PessoaId });
            }

            return View(pessoa);
        }

        [AutenticarFiltro(Roles = "Administrador")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pessoa pessoa = db.Pessoas.Find(id);
            if (pessoa == null)
            {
                return HttpNotFound();
            }
            return View(pessoa);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PessoaId,Nome,Idade,Telefone,Endereco,Email,Imagem,Favorita,Login,Senha,Administrador")] Pessoa pessoa, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var filePath = Path.Combine(Server.MapPath("~/Images"), fileName);
                    file.SaveAs(filePath);
                    pessoa.Imagem = fileName;
                }
                else
                {
                    pessoa.Imagem = "Sem-Foto.jpg";
                }

                db.Entry(pessoa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new { id = pessoa.PessoaId });
            }
            return View(pessoa);
        }

        [AutenticarFiltro(Roles = "Administrador")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pessoa pessoa = db.Pessoas.Find(id);
            if (pessoa == null)
            {
                return HttpNotFound();
            }
            return View(pessoa);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pessoa pessoa = db.Pessoas.Find(id);
            List<MensagemContato> Mensagens = db.MensagemContatoes.Where(x => x.ContatoId == pessoa.PessoaId).ToList();
            for (int i = 0; i < Mensagens.Count; i++)
            {
                MensagemContato Mc = Mensagens[i];
                db.MensagemContatoes.Remove(Mc);
            }
            //db.MensagemContatoes.Remove(db.MensagemContatoes.Where(x => x.ContatoId == pessoa.PessoaId));
            db.Pessoas.Remove(pessoa);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Buscar(string Inicial)
        {
            List<Pessoa> Busca = new List<Pessoa>();
            if (Inicial == "Favorito")
            {
                Busca = db.Pessoas.Where(x => x.Favorita == true).ToList();
                return View(Busca.OrderBy(x => x.Nome));
            }
            else if (Inicial != " ")
            {
                Busca = db.Pessoas.Where(x => x.Nome.StartsWith(Inicial)).ToList();
                return View(Busca.OrderBy(x => x.Nome));
            }
            return RedirectToAction("Index");
        }


        public ActionResult AdicionarFavorito(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pessoa pessoa = db.Pessoas.Find(id);
            if (pessoa == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    if (pessoa.Favorita)
                    {
                        pessoa.Favorita = false;
                    }
                    else
                    {
                        pessoa.Favorita = true;
                    }
                    db.Entry(pessoa).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Details", new { id = pessoa.PessoaId });
                }
                return View(pessoa);
            }
        }

        [AutenticarFiltro(Roles = "Contato")]
        public ActionResult MensagensContato(string id)
        {
            Usuario u = db.Usuarios.FirstOrDefault(x => x.Login.Equals(id));
            ViewBag.Mensagens = db.MensagemContatoes.Where(x => x.ContatoId == u.PessoaId).ToList();
            return View(u);
        }

        public ActionResult ListarPessoas()
        {
            ReportViewer relatorio = new ReportViewer();
            relatorio.ProcessingMode = ProcessingMode.Local;
            relatorio.LocalReport.ReportPath = Request.MapPath("~/Reports/ContatosAtuais.rdlc");
            relatorio.LocalReport.DataSources.Add(new ReportDataSource("ContatosAtuais", db.Pessoas.ToList()));
            relatorio.SizeToReportContent = true;
            relatorio.Width = System.Web.UI.WebControls.Unit.Percentage(100);
            relatorio.Height = System.Web.UI.WebControls.Unit.Percentage(200);
            relatorio.CssClass = "form-control";

            ViewBag.Relatorio = relatorio;
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