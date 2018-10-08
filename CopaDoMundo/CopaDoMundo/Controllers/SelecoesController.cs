using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CopaDoMundo.Models;
using PagedList;

namespace CopaDoMundo.Controllers
{
    public class SelecoesController : Controller
    {
        private CopaDoMundoEntities db = new CopaDoMundoEntities();

        // GET: Selecoes
        public ActionResult Index(int? pag)
        {
            return View(db.Selecoes1.OrderBy(s => s.Nome).ToList().ToList());
        }

        // GET: Selecoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Selecoes selecoes = db.Selecoes1.Find(id);
            if (selecoes == null)
            {
                return HttpNotFound();
            }
            return View(selecoes);
        }

        // GET: Selecoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Selecoes/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdSelecao,Nome,QtdTitulos,Bandeira,Descricao")] Selecoes selecoes)
        {
            if (ModelState.IsValid)
            {
                if (db.Selecoes1.Count() == 16)
                {
                    //Oitavas();
                    //exibir alerta de 16 seleções já cadastradas
                }
                else if (db.Selecoes1.Count() < 17)
                {
                    db.Selecoes1.Add(selecoes);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            return View(selecoes);
        }


        // GET: Selecoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Selecoes selecoes = db.Selecoes1.Find(id);
            if (selecoes == null)
            {
                return HttpNotFound();
            }
            return View(selecoes);
        }

        // POST: Selecoes/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdSelecao,Nome,QtdTitulos,Bandeira,Descricao")] Selecoes selecoes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(selecoes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(selecoes);
        }

        // GET: Selecoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Selecoes selecoes = db.Selecoes1.Find(id);
            if (selecoes == null)
            {
                return HttpNotFound();
            }
            return View(selecoes);
        }

        // POST: Selecoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Selecoes selecoes = db.Selecoes1.Find(id);
            db.Selecoes1.Remove(selecoes);
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

        public ActionResult SortearOitavas()
        {
            List<Selecoes> lstSelecoes = new List<Selecoes>();
            lstSelecoes = db.Selecoes1.ToList();

            if (lstSelecoes.Count() == 16)
            {
                db.Jogos.RemoveRange(db.Jogos.Where(j => j.FaseId <= 8));
                db.SaveChanges();

                List<Selecoes> randomList = new List<Selecoes>();
                Jogos jogos;

                Random r = new Random();
                int randomIndex = 0;
                while (lstSelecoes.Count > 0)
                {
                    randomIndex = r.Next(0, lstSelecoes.Count); //Choose a random object in the list
                    randomList.Add(lstSelecoes[randomIndex]); //add it to the new, random list
                    lstSelecoes.RemoveAt(randomIndex); //remove to avoid duplicates
                }

                randomList.Count();

                for (int i = 0; i < randomList.Count(); i += 2)
                {
                    jogos = new Jogos();

                    jogos.SelecaoId1 = randomList[i].IdSelecao;
                    jogos.SelecaoId2 = randomList[i + 1].IdSelecao;
                    jogos.Gol1 = 0;
                    jogos.Gol2 = 0;
                    jogos.FaseId = 8;

                    db.Jogos.Add(jogos);
                    db.SaveChanges();
                }
            }

            return RedirectToAction("Oitavas", "Jogos");
        }

        public ActionResult RemoverTodas()
        {
            db.Jogos.RemoveRange(db.Jogos.Where(j => j.FaseId <= 8));
            db.SaveChanges();

            db.Selecoes1.RemoveRange(db.Selecoes1.Where(s => s.IdSelecao > 0));
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
