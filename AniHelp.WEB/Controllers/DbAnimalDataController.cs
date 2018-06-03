using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using AniHelp.WEB.Models;
using OfficeOpenXml;

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
        public ActionResult Download(int id)
        {
            DbAnimalData g = db.AnimalDatas.Find(id);

            ExcelPackage pkg;
            using (var stream = System.IO.File.OpenRead
                (HostingEnvironment.ApplicationPhysicalPath + "template.xlsx"))
            {
                pkg = new ExcelPackage(stream);
                stream.Dispose();
            }

            var worksheet = pkg.Workbook.Worksheets[1];
            worksheet.Name = g.Name == "" ? "Лист1" : g.Name;

            worksheet.Cells[2, 1].Value = g.Filled.ToString("dd MMMM, yyyy");
            worksheet.Cells[2, 2].Value = g.Name;
            worksheet.Cells[2, 3].Value = g.SeizurePlace;
            worksheet.Cells[2, 4].Value = g.Collar;
            worksheet.Cells[2, 5].Value = g.Pregnancy ? "Есть" : "Нет";
            worksheet.Cells[2, 6].Value = g.CrueltySigns ? "Есть" : "Нет";
            worksheet.Cells[2, 7].Value = g.EuthanasiaCause;
            worksheet.Cells[2, 8].Value = g.Action.ToString().Replace("_", " ");

            int i = 2;
            foreach (var e in g.HealthTroubles)
            {
                worksheet.Cells[i, 9].Value = e.HealthTrouble;
                i++;
            }

            // Заполнение файла Excel вышими данными
            var ms = new MemoryStream();
            pkg.SaveAs(ms);
            return File(ms.ToArray(), "application/ooxml",
                    (g.Name == "" ? "Без Названия" : g.Name).Replace(" ", "") + ".xlsx");
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