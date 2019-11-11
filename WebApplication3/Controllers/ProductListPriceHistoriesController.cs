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
    public class ProductListPriceHistoriesController : Controller
    {
        private AdventureWorks2008R2Entities db = new AdventureWorks2008R2Entities();

        // GET: ProductListPriceHistories
        public ActionResult Index()
        {
            var productListPriceHistories = db.ProductListPriceHistories.Include(p => p.Product);
            return View(productListPriceHistories.ToList());
        }

        // GET: ProductListPriceHistories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductListPriceHistory productListPriceHistory = db.ProductListPriceHistories.Find(id);
            if (productListPriceHistory == null)
            {
                return HttpNotFound();
            }
            return View(productListPriceHistory);
        }

        // GET: ProductListPriceHistories/Create
        public ActionResult Create()
        {
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Name");
            return View();
        }

        // POST: ProductListPriceHistories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,StartDate,EndDate,ListPrice,ModifiedDate,isDeleted")] ProductListPriceHistory productListPriceHistory)
        {
            if (ModelState.IsValid)
            {
                db.ProductListPriceHistories.Add(productListPriceHistory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Name", productListPriceHistory.ProductID);
            return View(productListPriceHistory);
        }

        // GET: ProductListPriceHistories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductListPriceHistory productListPriceHistory = db.ProductListPriceHistories.Find(id);
            if (productListPriceHistory == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Name", productListPriceHistory.ProductID);
            return View(productListPriceHistory);
        }

        // POST: ProductListPriceHistories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductID,StartDate,EndDate,ListPrice,ModifiedDate,isDeleted")] ProductListPriceHistory productListPriceHistory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productListPriceHistory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Name", productListPriceHistory.ProductID);
            return View(productListPriceHistory);
        }

        // GET: ProductListPriceHistories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductListPriceHistory productListPriceHistory = db.ProductListPriceHistories.Find(id);
            if (productListPriceHistory == null)
            {
                return HttpNotFound();
            }
            return View(productListPriceHistory);
        }

        // POST: ProductListPriceHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var res = (from c in db.ProductListPriceHistories
                       where c.ProductID == id
                       select c).FirstOrDefault();

            if (res != null)
            {
                res.isDeleted = true;
                db.SaveChanges();
                ViewBag.Message = string.Format("Congrats! Delete success");
            }

            ProductListPriceHistory productCategory = db.ProductListPriceHistories.Find(id);



            return View(productCategory);
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
