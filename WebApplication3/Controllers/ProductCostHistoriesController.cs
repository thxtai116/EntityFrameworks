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
    public class ProductCostHistoriesController : Controller
    {
        private AdventureWorks2008R2Entities db = new AdventureWorks2008R2Entities();

        // GET: ProductCostHistories
        public ActionResult Index()
        {
            var productCostHistories = db.ProductCostHistories.Include(p => p.Product);
            return View(productCostHistories.ToList());
        }

        // GET: ProductCostHistories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductCostHistory productCostHistory = db.ProductCostHistories.Find(id);
            if (productCostHistory == null)
            {
                return HttpNotFound();
            }
            return View(productCostHistory);
        }

        // GET: ProductCostHistories/Create
        public ActionResult Create()
        {
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Name");
            return View();
        }

        // POST: ProductCostHistories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,StartDate,EndDate,StandardCost,ModifiedDate,isDeleted")] ProductCostHistory productCostHistory)
        {
            if (ModelState.IsValid)
            {
                db.ProductCostHistories.Add(productCostHistory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Name", productCostHistory.ProductID);
            return View(productCostHistory);
        }

        // GET: ProductCostHistories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductCostHistory productCostHistory = db.ProductCostHistories.Find(id);
            if (productCostHistory == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Name", productCostHistory.ProductID);
            return View(productCostHistory);
        }

        // POST: ProductCostHistories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductID,StartDate,EndDate,StandardCost,ModifiedDate,isDeleted")] ProductCostHistory productCostHistory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productCostHistory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Name", productCostHistory.ProductID);
            return View(productCostHistory);
        }

        // GET: ProductCostHistories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductCostHistory productCostHistory = db.ProductCostHistories.Find(id);
            if (productCostHistory == null)
            {
                return HttpNotFound();
            }
            return View(productCostHistory);
        }

        // POST: ProductCostHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var res = (from c in db.ProductCostHistories
                       where c.ProductID == id
                       select c).FirstOrDefault();

            if (res != null)
            {
                res.isDeleted = true;
                db.SaveChanges();
                ViewBag.Message = string.Format("Congrats! Delete success");
            }

            ProductCostHistory productCategory = db.ProductCostHistories.Find(id);



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
