using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CopaDoMundo.Models;

namespace CopaDoMundo.Controllers
{
    public class JogosController : Controller
    {
        private CopaDoMundoEntities db = new CopaDoMundoEntities();

        // GET: Jogos
        public ActionResult Index()
        {
            var jogos = db.Jogos.Include(j => j.Fas).Include(j => j.Seleco).Include(j => j.Seleco1);
            return View(jogos.ToList());
        }

        public ActionResult Oitavas()
        {
            var jogos = db.Jogos.Include(j => j.Fas).Include(j => j.Seleco).Include(j => j.Seleco1);
            return View(jogos.Where(j => j.FaseId == 8).ToList());
        }

        public ActionResult Quartas()
        {
            var jogos = db.Jogos.Include(j => j.Fas).Include(j => j.Seleco).Include(j => j.Seleco1);
            return View(jogos.Where(j => j.FaseId == 4).ToList());
        }

        public ActionResult Semi()
        {
            var jogos = db.Jogos.Include(j => j.Fas).Include(j => j.Seleco).Include(j => j.Seleco1);
            return View(jogos.Where(j => j.FaseId == 2).ToList());
        }

        public ActionResult Terceiro()
        {
            var jogos = db.Jogos.Include(j => j.Fas).Include(j => j.Seleco).Include(j => j.Seleco1);
            return View(jogos.Where(j => j.FaseId == 1).ToList());
        }

        public ActionResult Final()
        {
            var jogos = db.Jogos.Include(j => j.Fas).Include(j => j.Seleco).Include(j => j.Seleco1);
            return View(jogos.Where(j => j.FaseId == 0).ToList());
        }

        // GET: Jogos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jogos jogos = db.Jogos.Find(id);
            if (jogos == null)
            {
                return HttpNotFound();
            }
            return View(jogos);
        }

        // GET: Jogos/Create
        public ActionResult Create()
        {
            ViewBag.FaseId = new SelectList(db.Fases1, "FaseId", "Descricao");
            ViewBag.SelecaoId1 = new SelectList(db.Selecoes1, "IdSelecao", "Nome");
            ViewBag.SelecaoId2 = new SelectList(db.Selecoes1, "IdSelecao", "Nome");
            return View();
        }

        // POST: Jogos/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "JogoId,SelecaoId1,SelecaoId2,Gol1,Gol2,FaseId")] Jogos jogos)
        {
            if (ModelState.IsValid)
            {
                db.Jogos.Add(jogos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FaseId = new SelectList(db.Fases1, "FaseId", "Descricao", jogos.FaseId);
            ViewBag.SelecaoId1 = new SelectList(db.Selecoes1, "IdSelecao", "Nome", jogos.SelecaoId1);
            ViewBag.SelecaoId2 = new SelectList(db.Selecoes1, "IdSelecao", "Nome", jogos.SelecaoId2);
            return View(jogos);
        }
        //public ActionResult Edit(Jogos jg)
        //{
        //    return View("Oitavas");
        //}

        // GET: Jogos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jogos jogos = db.Jogos.Find(id);

            if (jogos == null)
            {
                return HttpNotFound();
            }
            return View(jogos);

        }

        // POST: Jogos/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "JogoId,SelecaoId1,SelecaoId2,Gol1,Gol2,FaseId")] Jogos jogos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jogos).State = EntityState.Modified;
                db.SaveChanges();

                if (jogos.FaseId == 8)
                    return RedirectToAction("Oitavas");
                else if (jogos.FaseId == 4)
                    return RedirectToAction("Quartas");
                else if (jogos.FaseId == 2)
                    return RedirectToAction("Semi");
                else if (jogos.FaseId == 1)
                    return RedirectToAction("Terceiro");
                else if (jogos.FaseId == 0)
                    return RedirectToAction("Final");

            }

            return RedirectToAction("Index", "Selecoes");
        }

        // GET: Jogos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jogos jogos = db.Jogos.Find(id);
            if (jogos == null)
            {
                return HttpNotFound();
            }
            return View(jogos);
        }

        // POST: Jogos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Jogos jogos = db.Jogos.Find(id);
            db.Jogos.Remove(jogos);
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

        public ActionResult SortearQuartas()
        {
            List<Jogos> lstJogos = new List<Jogos>();
            lstJogos = db.Jogos.Where(j => j.FaseId == 8).ToList();

            if (lstJogos.Count() == 8)
            {
                db.Jogos.RemoveRange(db.Jogos.Where(j => j.FaseId <= 4));
                db.SaveChanges();

                List<Jogos> randomList = new List<Jogos>();
                Jogos jogos;

                Random r = new Random();
                int randomIndex = 0;
                while (lstJogos.Count > 0)
                {
                    randomIndex = r.Next(0, lstJogos.Count); //Choose a random object in the list
                    randomList.Add(lstJogos[randomIndex]); //add it to the new, random list
                    lstJogos.RemoveAt(randomIndex); //remove to avoid duplicates
                }

                randomList.Count();

                for (int i = 0; i < randomList.Count(); i += 2)
                {
                    jogos = new Jogos();

                    if (randomList[i].Gol1 > randomList[i].Gol2)
                        jogos.SelecaoId1 = randomList[i].SelecaoId1;
                    else if (randomList[i].Gol2 > randomList[i].Gol1)
                        jogos.SelecaoId1 = randomList[i].SelecaoId2;

                    if (randomList[i + 1].Gol1 > randomList[i + 1].Gol2)
                        jogos.SelecaoId2 = randomList[i + 1].SelecaoId1;
                    else if (randomList[i + 1].Gol2 > randomList[i + 1].Gol1)
                        jogos.SelecaoId2 = randomList[i + 1].SelecaoId2;

                    jogos.Gol1 = 0;
                    jogos.Gol2 = 0;
                    jogos.FaseId = 4;

                    db.Jogos.Add(jogos);
                    db.SaveChanges();
                }
            }

            return RedirectToAction("Quartas", "Jogos");
        }

        public ActionResult SortearSemi()
        {
            List<Jogos> lstJogos = new List<Jogos>();
            lstJogos = db.Jogos.Where(j => j.FaseId == 4).ToList();

            if (lstJogos.Count() == 4)
            {
                db.Jogos.RemoveRange(db.Jogos.Where(j => j.FaseId <= 2));
                db.SaveChanges();

                List<Jogos> randomList = new List<Jogos>();
                Jogos jogos;

                Random r = new Random();
                int randomIndex = 0;
                while (lstJogos.Count > 0)
                {
                    randomIndex = r.Next(0, lstJogos.Count); //Choose a random object in the list
                    randomList.Add(lstJogos[randomIndex]); //add it to the new, random list
                    lstJogos.RemoveAt(randomIndex); //remove to avoid duplicates
                }

                randomList.Count();

                for (int i = 0; i < randomList.Count(); i += 2)
                {
                    jogos = new Jogos();

                    if (randomList[i].Gol1 > randomList[i].Gol2)
                        jogos.SelecaoId1 = randomList[i].SelecaoId1;
                    else if (randomList[i].Gol2 > randomList[i].Gol1)
                        jogos.SelecaoId1 = randomList[i].SelecaoId2;

                    if (randomList[i + 1].Gol1 > randomList[i + 1].Gol2)
                        jogos.SelecaoId2 = randomList[i + 1].SelecaoId1;
                    else if (randomList[i + 1].Gol2 > randomList[i + 1].Gol1)
                        jogos.SelecaoId2 = randomList[i + 1].SelecaoId2;

                    jogos.Gol1 = 0;
                    jogos.Gol2 = 0;
                    jogos.FaseId = 2;

                    db.Jogos.Add(jogos);
                    db.SaveChanges();
                }
            }

            return RedirectToAction("Semi", "Jogos");
        }

        public ActionResult SortearFinal()
        {
            List<Jogos> lstJogos = new List<Jogos>();
            lstJogos = db.Jogos.Where(j => j.FaseId == 2).ToList();

            if (lstJogos.Count() == 2)
            {
                db.Jogos.RemoveRange(db.Jogos.Where(j => j.FaseId <= 1));
                db.SaveChanges();

                List<Jogos> randomList = new List<Jogos>();
                Jogos jogosT;
                Jogos jogosF;

                Random r = new Random();
                int randomIndex = 0;
                while (lstJogos.Count > 0)
                {
                    randomIndex = r.Next(0, lstJogos.Count); //Choose a random object in the list
                    randomList.Add(lstJogos[randomIndex]); //add it to the new, random list
                    lstJogos.RemoveAt(randomIndex); //remove to avoid duplicates
                }

                randomList.Count();

                for (int i = 0; i < randomList.Count(); i += 2)
                {
                    jogosT = new Jogos();
                    jogosF = new Jogos();

                    if (randomList[i].Gol1 > randomList[i].Gol2)
                        jogosF.SelecaoId1 = randomList[i].SelecaoId1;
                    else if (randomList[i].Gol2 > randomList[i].Gol1)
                        jogosF.SelecaoId1 = randomList[i].SelecaoId2;

                    if (randomList[i + 1].Gol1 > randomList[i + 1].Gol2)
                        jogosF.SelecaoId2 = randomList[i + 1].SelecaoId1;
                    else if (randomList[i + 1].Gol2 > randomList[i + 1].Gol1)
                        jogosF.SelecaoId2 = randomList[i + 1].SelecaoId2;

                    jogosF.Gol1 = 0;
                    jogosF.Gol2 = 0;
                    jogosF.FaseId = 0;

                    db.Jogos.Add(jogosF);
                    db.SaveChanges();

                    if (randomList[i].Gol1 < randomList[i].Gol2)
                        jogosT.SelecaoId1 = randomList[i].SelecaoId1;
                    else if (randomList[i].Gol2 < randomList[i].Gol1)
                        jogosT.SelecaoId1 = randomList[i].SelecaoId2;

                    if (randomList[i + 1].Gol1 < randomList[i + 1].Gol2)
                        jogosT.SelecaoId2 = randomList[i + 1].SelecaoId1;
                    else if (randomList[i + 1].Gol2 < randomList[i + 1].Gol1)
                        jogosT.SelecaoId2 = randomList[i + 1].SelecaoId2;

                    jogosT.Gol1 = 0;
                    jogosT.Gol2 = 0;
                    jogosT.FaseId = 1;

                    db.Jogos.Add(jogosT);
                    db.SaveChanges();
                }
            }

            return RedirectToAction("Final", "Jogos");
        }
    }
}
