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
    public class CountryRegionsController : Controller
    {
        private AdventureWorks2008R2Entities db = new AdventureWorks2008R2Entities();

        // GET: CountryRegions
        public ActionResult Index()
        {
            return View(db.CountryRegions.ToList());
        }

        // GET: CountryRegions/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CountryRegion countryRegion = db.CountryRegions.Find(id);
            if (countryRegion == null)
            {
                return HttpNotFound();
            }
            return View(countryRegion);
        }

        // GET: CountryRegions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CountryRegions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CountryRegionCode,Name,ModifiedDate,isDeleted")] CountryRegion countryRegion)
        {
            if (ModelState.IsValid)
            {
                db.CountryRegions.Add(countryRegion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(countryRegion);
        }

        // GET: CountryRegions/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CountryRegion countryRegion = db.CountryRegions.Find(id);
            if (countryRegion == null)
            {
                return HttpNotFound();
            }
            return View(countryRegion);
        }

        // POST: CountryRegions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CountryRegionCode,Name,ModifiedDate,isDeleted")] CountryRegion countryRegion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(countryRegion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(countryRegion);
        }

        // GET: CountryRegions/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CountryRegion countryRegion = db.CountryRegions.Find(id);
            if (countryRegion == null)
            {
                return HttpNotFound();
            }
            return View(countryRegion);
        }

        // POST: CountryRegions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            var res = (from c in db.CountryRegions
                       where c.CountryRegionCode == id
                       select c).FirstOrDefault();

            if (res != null)
            {
                res.isDeleted = true;
                db.SaveChanges();
                ViewBag.Message = string.Format("Congrats! Delete success");
            }

            CountryRegion country = db.CountryRegions.Find(id);



            return View(country);
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
