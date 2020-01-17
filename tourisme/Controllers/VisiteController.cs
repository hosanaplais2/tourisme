using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace tourisme.Controllers
{
    public class VisiteController : Controller
    {
        tourismeEntities db = new tourismeEntities();
        // GET: visite
        public ActionResult Listvisite()
        {
            
            string data = "";
            var listvisiteur = db.visiteur.ToList();

            foreach (var sit in listvisiteur)
            {
                data = data + "/" + sit.NumVisiteur + "/" + sit.NomVisiteur;
            }
            var listsite = db.site.ToList();

            foreach (var sit in listsite)
            {
                data = data + "/" + sit.NumSite + "/" + sit.NomSite;
            }
            ViewBag.Message = data;
            return View(db.visite.ToList());
        }
    }
}