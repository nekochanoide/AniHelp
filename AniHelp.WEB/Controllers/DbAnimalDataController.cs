using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AniHelp.WEB.Models;

namespace AniHelp.WEB.Controllers
{
    public class DbAnimalDatasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: DbRideRequests
        public ActionResult Index()
        {
            return View(db.AnimalDatas.ToList());
        }

        // GET: DbRideRequests/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DbAnimalData dbAnimalData = db.AnimalDatas.Find(id);
            if (dbAnimalData == null)
            {
                return HttpNotFound();
            }
            return View(dbAnimalData);
        }

        // GET: DbRideRequests/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DbRideRequests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Filled,Name,Pregnancy,CrueltySigns")] DbAnimalData dbAnimalData)
        {
            if (ModelState.IsValid)
            {
                db.AnimalDatas.Add(dbAnimalData);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dbAnimalData);
        }


        // GET: DbRideRequests/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DbAnimalData dbAnimalData = db.AnimalDatas.Find(id);
            if (dbAnimalData == null)
            {
                return HttpNotFound();
            }
            return View(dbAnimalData);
        }

        // POST: DbAnimalDatas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DbAnimalData dbAnimalData = db.AnimalDatas.Find(id);
            dbAnimalData.HealthTroubles.Clear();
            db.AnimalDatas.Remove(dbAnimalData);
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