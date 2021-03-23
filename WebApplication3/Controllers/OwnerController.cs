using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class OwnerController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult AddOwner()
        {
            return View();
        }

        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AddOwner(Owner owner)
        {
            //Product.ExpireDate = Convert.ToDateTime("06-01-2016");
            if (ModelState.IsValid)
            {
                db.ProductOwner.Add(owner);
                db.SaveChanges();
                // ViewData["message"] = "insert sucessfully";
                return RedirectToAction("OwnerList");
            }
            return View();
        }

        public ActionResult OwnerList(string searchBy, string search, int? page)
        {
            if (searchBy == "FirstName")
            {
                return View(db.ProductOwner.Where(x => x.FirstName == search || search == null).ToList().ToPagedList(page ?? 1, 5));
            }
            else
            {
                return View(db.ProductOwner.Where(x => x.FirstName.StartsWith(search) || search == null).ToList().ToPagedList(page ?? 1, 5));
            }
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Owner owner = db.ProductOwner.Find(id);
            if (owner == null)
            {
                return HttpNotFound();
            }
            return View(owner);
        }

        [HttpPost]

        public ActionResult Edit(Owner owner)
        {
            db.Entry(owner).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("OwnerList");
        }


        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Owner owner = db.ProductOwner.Find(id);
            if (owner == null)
            {
                return HttpNotFound();
            }
            return View(owner);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Owner owner = db.ProductOwner.Find(id);
            db.ProductOwner.Remove(owner);
            db.SaveChanges();
            return RedirectToAction("OwnerList");
        }
    }
}