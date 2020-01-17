using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace tourisme.Controllers
{
    public class VisiteurController : Controller
    {
        // GET: Visiteur
        tourismeEntities db = new tourismeEntities();
        
        public ActionResult Listvisiteur()
        {
            return View(db.visiteur.ToList());
        }

        //creation visiteur
        [HttpGet]
        public ActionResult CreerVisiteur()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreerVisiteur(visiteur visiteurSaisie)
        {
            if (ModelState.IsValid)
            {

                var visit = db.visiteur.ToList();
                string lastid = "";
                foreach (var vis in visit)
                {
                    lastid = vis.NumVisiteur;
                }

                string d = lastid.Remove(0, 1);
                int s = Convert.ToInt32(d);
                s++;
                string a = Convert.ToString(s);
                string b ="";
                if (s < 10) { b = "V000" + a; }
                else if (s < 100) { b = "V00" + a; }
                else if (s < 1000) { b = "V0" + a; }
                else { b = "V" + a; }
                
                visiteurSaisie.NumVisiteur = b;
                db.visiteur.Add(visiteurSaisie);
                db.SaveChanges();
                return RedirectToAction("Listvisiteur");
            }
            return View(visiteurSaisie);
        }
        //Editer visiteur
        public ActionResult EditerVisiteur(string id)
        {
            if (id == null)
            {
                return RedirectToAction("Listvisiteur");
            }
            visiteur visiteurEditer = db.visiteur.Find(id);
            return View(visiteurEditer);
        }

        [HttpPost]
        public ActionResult EditerVisiteur(visiteur visiteurEditer)
        {
            if (ModelState.IsValid)
            {

                db.Entry(visiteurEditer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Listvisiteur");
            }

            return View(visiteurEditer);
        }

        public ActionResult Visiter(string id)
        {
            if (id==null)
            {
                return RedirectToAction("Listvisiteur");
                            
            }
            var listsite = db.site.ToList();
            string sites = id;
            foreach (var sit in listsite)
            {
                sites = sites + "/" + sit.NumSite + "/" + sit.NomSite;
            }
            ViewBag.Message = sites;
            return View();
        }
        [HttpPost]
        public ActionResult Visiter( FormCollection collection)
        {
            visite visiteinfo = new visite();
            var visit = db.visite.ToList();
           int lastid=0;
            foreach (var vis in visit)
            {
                lastid = vis.id;
            }
            lastid++;
            visiteinfo.id = lastid;
            visiteinfo.NumVisiteur = collection["NumVisiteur"];
            visiteinfo.NumSite = collection["NumSite"];
            int jours = Convert.ToInt32(collection["nbJours"]);
            visiteinfo.NbJours = jours;
            string date = collection["DateVisite"];

            visiteinfo.DateVisite = Convert.ToDateTime(date);
            db.visite.Add(visiteinfo);
            db.SaveChanges();
            
            return RedirectToAction("Listvisiteur");
        }

        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return RedirectToAction("Listvisiteur");
            }


            visiteur visit = db.visiteur.Find(id);
            string sites = visit.NumVisiteur + "/" + visit.NomVisiteur + "/" + visit.Adresse ;
            var listsite = db.site.ToList();
            
            foreach (var sit in listsite)
            {
                sites = sites + "/" + sit.NumSite + "/" + sit.NomSite;
            }
            ViewBag.Message = sites;

            return View(db.visite.ToList());
        }

        //effacer visiteur

        public ActionResult SupprimmerVisiteur(string id)
        {
            if (id == null)
            {
                return RedirectToAction("Listvisiteur");
            }
            visiteur Editer = db.visiteur.Find(id);

            ViewBag.Message = id;
            return View(Editer);
        }
        [HttpPost]
        public ActionResult SupprimmerVisiteur(FormCollection collection)
        {
            string id = collection["NumVisiteurSupp"];

            visiteur Editer = db.visiteur.Find(id);
            db.visiteur.Remove(Editer);
            db.SaveChanges();
            return RedirectToAction("Listvisiteur");
        }

    }
}