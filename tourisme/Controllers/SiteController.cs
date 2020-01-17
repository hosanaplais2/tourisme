using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace tourisme.Controllers
{
    public class SiteController : Controller
    {
        tourismeEntities db = new tourismeEntities();
        // GET: Site
        public ActionResult Listsite()
        {
            return View(db.site.ToList());
        }

        //creation site
        [HttpGet]
        public ActionResult CreerSite()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreerSite(site siteSaisie)
        {
            if (ModelState.IsValid) 
            {
                var sites = db.site.ToList();
                string lastid = "";
                foreach (var sit in sites)
                {
                    lastid = sit.NumSite;
                }

                string d = lastid.Remove(0, 1);
                int s = Convert.ToInt32(d);
                s++;
                string a = Convert.ToString(s);
                string b = "";
                if (s < 10) { b = "S000" + a; }
                else if (s < 100) { b = "S00" + a; }
                else if (s < 1000) { b = "S0" + a; }
                else { b = "S" + a; }
                siteSaisie.NumSite = b;

                db.site.Add(siteSaisie);
                db.SaveChanges();
                return RedirectToAction("Listsite");
            }
            return View(siteSaisie);
        }

        //editer site
        public ActionResult EditerSite(string id)
        {
            if (id == null)
            {
                return RedirectToAction("Listsite");
            }
            site siteEditer = db.site.Find(id);
            return View(siteEditer);
        }

        [HttpPost]
        public ActionResult EditerSite(site siteEditer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(siteEditer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Listsite");
            }
            
            return View(siteEditer);
        }

        //effacer site
        
        public ActionResult SupprimmerSite(string id)
        {
            if (id == null)
            {
                return RedirectToAction("Listsite");
            }
            site siteEditer = db.site.Find(id);
            
            ViewBag.Message = id;
            return View(siteEditer);
        }
        [HttpPost]
        public ActionResult SupprimmerSite(FormCollection collection)
        {
            string id = collection["NumSiteSupp"];

            site siteEditer = db.site.Find(id);
            db.site.Remove(siteEditer);
            db.SaveChanges();
            return RedirectToAction("Listsite");
        }

        //details
        public ActionResult DetailsSite(string id)
        {
            if (id == null)
            {
                return RedirectToAction("Listvisiteur");
            }

            site visit = db.site.Find(id);
            string sites = visit.NumSite + "/" + visit.NomSite + "/" + visit.Lieu + "/" + visit.TarifJournaliere;
            var listvisiteur = db.visiteur.ToList();

            foreach (var sit in listvisiteur)
            {
                sites = sites + "/" + sit.NumVisiteur + "/" + sit.NomVisiteur;
            }
            ViewBag.Message = sites;

            return View(db.visite.ToList());
        }
    }
}