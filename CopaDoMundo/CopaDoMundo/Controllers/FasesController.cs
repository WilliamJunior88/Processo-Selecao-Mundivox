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
    public class FasesController : Controller
    {
        private CopaDoMundoEntities db = new CopaDoMundoEntities();

        // GET: Fases
        public ActionResult Index()
        {
            return View(db.Fases1.ToList());
        }

        // GET: Fases/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fases fases = db.Fases1.Find(id);
            if (fases == null)
            {
                return HttpNotFound();
            }
            return View(fases);
        }

        // GET: Fases/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Fases/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FaseId,Descricao")] Fases fases)
        {
            if (ModelState.IsValid)
            {
                db.Fases1.Add(fases);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(fases);
        }

        // GET: Fases/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fases fases = db.Fases1.Find(id);
            if (fases == null)
            {
                return HttpNotFound();
            }
            return View(fases);
        }

        // POST: Fases/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FaseId,Descricao")] Fases fases)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fases).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fases);
        }

        // GET: Fases/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fases fases = db.Fases1.Find(id);
            if (fases == null)
            {
                return HttpNotFound();
            }
            return View(fases);
        }

        // POST: Fases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Fases fases = db.Fases1.Find(id);
            db.Fases1.Remove(fases);
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
