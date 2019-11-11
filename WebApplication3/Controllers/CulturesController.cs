using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication3;

namespace WebApplication3.Controllers
{
    public class CulturesController : Controller
    {
        private AdventureWorks2008R2Entities db = new AdventureWorks2008R2Entities();

        // GET: Cultures
        public ActionResult Index()
        {
            return View(db.Cultures.ToList());
        }

        // GET: Cultures/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Culture culture = db.Cultures.Find(id);
            if (culture == null)
            {
                return HttpNotFound();
            }
            return View(culture);
        }

        // GET: Cultures/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cultures/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CultureID,Name,ModifiedDate,isDeleted")] Culture culture)
        {
            if (ModelState.IsValid)
            {
                db.Cultures.Add(culture);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(culture);
        }

        // GET: Cultures/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Culture culture = db.Cultures.Find(id);
            if (culture == null)
            {
                return HttpNotFound();
            }
            return View(culture);
        }

        // POST: Cultures/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CultureID,Name,ModifiedDate,isDeleted")] Culture culture)
        {
            if (ModelState.IsValid)
            {
                db.Entry(culture).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(culture);
        }

        // GET: Cultures/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Culture culture = db.Cultures.Find(id);
            if (culture == null)
            {
                return HttpNotFound();
            }
            return View(culture);
        }

        // POST: Cultures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            var res = (from c in db.Cultures
                       where c.CultureID == id
                       select c).FirstOrDefault();

            if (res != null)
            {
                res.isDeleted = true;
                db.SaveChanges();
                ViewBag.Message = string.Format("Congrats! Delete success");
            }

            Culture culture = db.Cultures.Find(id);



            return View(culture);
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
