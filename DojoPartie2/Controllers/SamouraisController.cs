﻿using DojoPartie2.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TPModule6_1.Data;
using TPModule6_1.Extension;
using TpModule6Bo;

namespace TPModule6_1.Controllers
{
    public class SamouraisController : Controller
    {
        private TPModule6_1Context db = new TPModule6_1Context();

        // GET: Samourais
        public ActionResult Index()
        {
            return View(db.Samourais.ToList());
        }

        // GET: Samourais/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Samourai samourai = db.Samourais.Find(id);
            
            if (samourai == null)
            {
                return HttpNotFound();
            }
            return View(samourai);
        }

        // GET: Samourais/Create
        public ActionResult Create()
        {
            SamouraiViewModel vm = new SamouraiViewModel();
            vm.Armes = db.Armes.Where(x => !db.Samourais.Select(y => y.Arme.Id).Contains(x.Id)).ToList();
            vm.ArtMartials = db.ArtMartials.ToList();
            return View(vm);
        }

        // POST: Samourais/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SamouraiViewModel vm)
        {
            if (ModelState.IsValid)
            {

                if (vm.IdSelectedArme != null)
                {
                    vm.Samourai.Arme = db.Armes.FirstOrDefault(x => x.Id == vm.IdSelectedArme);
                }
                else
                {
                    vm.Samourai.Arme = null;
                }

                if (vm.IdArtMartials != null)
                {
                    vm.Samourai.ArtMartials = db.ArtMartials.Where(x => vm.IdArtMartials.Contains(x.Id)).ToList();
                }
                else
                {
                    vm.Samourai.ArtMartials = null;
                }

                db.Samourais.Add(vm.Samourai);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            vm.Armes = db.Armes.ToList();

            return View(vm);
        }

        // GET: Samourais/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SamouraiViewModel vm = new SamouraiViewModel();
            //vm.Samourai = db.Samourais.Include(x => x.Arme).FirstOrDefault(x => x.Id == id);
            vm.Samourai = db.Samourais.Find(id);
            vm.Armes = db.Armes.Where(x => !db.Samourais.Where(z => z.Id != vm.Samourai.Id).Select(y => y.Arme.Id).Contains(x.Id)).ToList();
            vm.ArtMartials = db.ArtMartials.ToList();

            if (vm.Samourai == null)
            {
                return HttpNotFound();
            }
            
            if (vm.Samourai.Arme != null)
            {
                vm.IdSelectedArme = vm.Samourai.Arme.Id;
            }

            if (vm.Samourai.ArtMartials != null)
            {
                vm.IdArtMartials = vm.Samourai.ArtMartials.Select(x => x.Id).ToList();
            }


            return View(vm);
        }

        // POST: Samourais/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SamouraiViewModel vm)
        {
            if (ModelState.IsValid)
            {
                //Inclide(x => x.Arme) récupère l'arme en mode Eager
                var currentSamourai = db.Samourais.Include(x => x.Arme).Include(x => x.ArtMartials).FirstOrDefault(x => x.Id == vm.Samourai.Id);
                currentSamourai.Force = vm.Samourai.Force;
                currentSamourai.Nom = vm.Samourai.Nom;

                //currentSamourai.CopyIn(vm.Samourai);

                if (vm.IdSelectedArme != null)
                {
                    currentSamourai.Arme = db.Armes.FirstOrDefault(x => x.Id == vm.IdSelectedArme);
                }
                else
                {
                    currentSamourai.Arme = null;
                }

                if (vm.IdArtMartials != null)
                {
                    currentSamourai.ArtMartials = db.ArtMartials.Where(x => vm.IdArtMartials.Contains(x.Id)).ToList();
                }
                else
                {
                    currentSamourai.Arme = null;
                }

                db.Entry(currentSamourai).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(vm);
        }

        // GET: Samourais/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Samourai samourai = db.Samourais.Find(id);
            if (samourai == null)
            {
                return HttpNotFound();
            }
            return View(samourai);
        }

        // POST: Samourais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Samourai samourai = db.Samourais.Find(id);
            db.Samourais.Remove(samourai);
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
