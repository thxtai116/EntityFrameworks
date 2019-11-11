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
    public class CurrencyRatesController : Controller
    {
        private AdventureWorks2008R2Entities db = new AdventureWorks2008R2Entities();

        // GET: CurrencyRates
        public ActionResult Index()
        {
            var currencyRates = db.CurrencyRates.Include(c => c.Currency).Include(c => c.Currency1);
            return View(currencyRates.ToList());
        }

        // GET: CurrencyRates/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CurrencyRate currencyRate = db.CurrencyRates.Find(id);
            if (currencyRate == null)
            {
                return HttpNotFound();
            }
            return View(currencyRate);
        }

        // GET: CurrencyRates/Create
        public ActionResult Create()
        {
            ViewBag.FromCurrencyCode = new SelectList(db.Currencies, "CurrencyCode", "Name");
            ViewBag.ToCurrencyCode = new SelectList(db.Currencies, "CurrencyCode", "Name");
            return View();
        }

        // POST: CurrencyRates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CurrencyRateID,CurrencyRateDate,FromCurrencyCode,ToCurrencyCode,AverageRate,EndOfDayRate,ModifiedDate,isDeleted")] CurrencyRate currencyRate)
        {
            if (ModelState.IsValid)
            {
                db.CurrencyRates.Add(currencyRate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FromCurrencyCode = new SelectList(db.Currencies, "CurrencyCode", "Name", currencyRate.FromCurrencyCode);
            ViewBag.ToCurrencyCode = new SelectList(db.Currencies, "CurrencyCode", "Name", currencyRate.ToCurrencyCode);
            return View(currencyRate);
        }

        // GET: CurrencyRates/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CurrencyRate currencyRate = db.CurrencyRates.Find(id);
            if (currencyRate == null)
            {
                return HttpNotFound();
            }
            ViewBag.FromCurrencyCode = new SelectList(db.Currencies, "CurrencyCode", "Name", currencyRate.FromCurrencyCode);
            ViewBag.ToCurrencyCode = new SelectList(db.Currencies, "CurrencyCode", "Name", currencyRate.ToCurrencyCode);
            return View(currencyRate);
        }

        // POST: CurrencyRates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CurrencyRateID,CurrencyRateDate,FromCurrencyCode,ToCurrencyCode,AverageRate,EndOfDayRate,ModifiedDate,isDeleted")] CurrencyRate currencyRate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(currencyRate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FromCurrencyCode = new SelectList(db.Currencies, "CurrencyCode", "Name", currencyRate.FromCurrencyCode);
            ViewBag.ToCurrencyCode = new SelectList(db.Currencies, "CurrencyCode", "Name", currencyRate.ToCurrencyCode);
            return View(currencyRate);
        }

        // GET: CurrencyRates/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CurrencyRate currencyRate = db.CurrencyRates.Find(id);
            if (currencyRate == null)
            {
                return HttpNotFound();
            }
            return View(currencyRate);
        }

        // POST: CurrencyRates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var res = (from c in db.CurrencyRates
                       where c.CurrencyRateID == id
                       select c).FirstOrDefault();

            if (res != null)
            {
                res.isDeleted = true;
                db.SaveChanges();
                ViewBag.Message = string.Format("Congrats! Delete success");
            }

            CurrencyRate currencyRate = db.CurrencyRates.Find(id);



            return View(currencyRate);
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
