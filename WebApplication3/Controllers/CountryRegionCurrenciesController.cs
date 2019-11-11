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
    public class CountryRegionCurrenciesController : Controller
    {
        private AdventureWorks2008R2Entities db = new AdventureWorks2008R2Entities();

        // GET: CountryRegionCurrencies
        public ActionResult Index()
        {
            var countryRegionCurrencies = db.CountryRegionCurrencies.Include(c => c.CountryRegion).Include(c => c.Currency);
            return View(countryRegionCurrencies.ToList());
        }

        // GET: CountryRegionCurrencies/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CountryRegionCurrency countryRegionCurrency = db.CountryRegionCurrencies.Find(id);
            if (countryRegionCurrency == null)
            {
                return HttpNotFound();
            }
            return View(countryRegionCurrency);
        }

        // GET: CountryRegionCurrencies/Create
        public ActionResult Create()
        {
            ViewBag.CountryRegionCode = new SelectList(db.CountryRegions, "CountryRegionCode", "Name");
            ViewBag.CurrencyCode = new SelectList(db.Currencies, "CurrencyCode", "Name");
            return View();
        }

        // POST: CountryRegionCurrencies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CountryRegionCode,CurrencyCode,ModifiedDate,isDeleted")] CountryRegionCurrency countryRegionCurrency)
        {
            if (ModelState.IsValid)
            {
                db.CountryRegionCurrencies.Add(countryRegionCurrency);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CountryRegionCode = new SelectList(db.CountryRegions, "CountryRegionCode", "Name", countryRegionCurrency.CountryRegionCode);
            ViewBag.CurrencyCode = new SelectList(db.Currencies, "CurrencyCode", "Name", countryRegionCurrency.CurrencyCode);
            return View(countryRegionCurrency);
        }

        // GET: CountryRegionCurrencies/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CountryRegionCurrency countryRegionCurrency = db.CountryRegionCurrencies.Find(id);
            if (countryRegionCurrency == null)
            {
                return HttpNotFound();
            }
            ViewBag.CountryRegionCode = new SelectList(db.CountryRegions, "CountryRegionCode", "Name", countryRegionCurrency.CountryRegionCode);
            ViewBag.CurrencyCode = new SelectList(db.Currencies, "CurrencyCode", "Name", countryRegionCurrency.CurrencyCode);
            return View(countryRegionCurrency);
        }

        // POST: CountryRegionCurrencies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CountryRegionCode,CurrencyCode,ModifiedDate,isDeleted")] CountryRegionCurrency countryRegionCurrency)
        {
            if (ModelState.IsValid)
            {
                db.Entry(countryRegionCurrency).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CountryRegionCode = new SelectList(db.CountryRegions, "CountryRegionCode", "Name", countryRegionCurrency.CountryRegionCode);
            ViewBag.CurrencyCode = new SelectList(db.Currencies, "CurrencyCode", "Name", countryRegionCurrency.CurrencyCode);
            return View(countryRegionCurrency);
        }

        // GET: CountryRegionCurrencies/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CountryRegionCurrency countryRegionCurrency = db.CountryRegionCurrencies.Find(id);
            if (countryRegionCurrency == null)
            {
                return HttpNotFound();
            }
            return View(countryRegionCurrency);
        }

        // POST: CountryRegionCurrencies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            var res = (from c in db.CountryRegionCurrencies
                       where c.CountryRegionCode == id
                       select c).FirstOrDefault();

            if (res != null)
            {
                res.isDeleted = true;
                db.SaveChanges();
                ViewBag.Message = string.Format("Congrats! Delete success");
            }

            CountryRegionCurrency countryRegionCurrency = db.CountryRegionCurrencies.Find(id);



            return View(countryRegionCurrency);
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
