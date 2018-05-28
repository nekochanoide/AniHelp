using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections.ObjectModel;
using AniHelp.WEB.Models;

namespace AniHelp.WEB.Controllers
{
    public class UploadController : Controller
    {
        // GET: Upload
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Print(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                var dto = BlankDtoHelper.LoadFromStream(file.InputStream);

                using (var db = new ApplicationDbContext())
                {
                    var row = new DbAnimalData
                    {
                        Filled = dto.Filled,
                        Name = dto.Name,
                        SeizurePlace = dto.SeizurePlace,
                        Collar = dto.Status.Collar,
                        Pregnancy = dto.Status.Pregnancy,
                        CrueltySigns = dto.Status.CrueltySigns,
                        EuthanasiaCause = dto.Status.EuthanasiaCause,
                        Action = (Models.Actions)(int)dto.Action
                    };

                    row.HealthTroubles = new Collection<DbHealthTrouble>();

                    foreach (var htDto in dto.Status.HealthTroubles)
                    {
                        row.HealthTroubles.Add(new DbHealthTrouble
                        {
                            HealthTrouble = htDto
                        });
                    }

                    db.AnimalDatas.Add(row);
                    db.SaveChanges();
                }

                return View(dto);
            }

            return RedirectToAction("Index");
        }
    }
}
