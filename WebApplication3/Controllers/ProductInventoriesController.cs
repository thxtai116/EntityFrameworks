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
    public class ProductInventoriesController : Controller
    {
        private AdventureWorks2008R2Entities db = new AdventureWorks2008R2Entities();

        // GET: ProductInventories
        public ActionResult Index()
        {
            var productInventories = db.ProductInventories.Include(p => p.Location).Include(p => p.Product);
            return View(productInventories.ToList());
        }

        // GET: ProductInventories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductInventory productInventory = db.ProductInventories.Find(id);
            if (productInventory == null)
            {
                return HttpNotFound();
            }
            return View(productInventory);
        }

        // GET: ProductInventories/Create
        public ActionResult Create()
        {
            ViewBag.LocationID = new SelectList(db.Locations, "LocationID", "Name");
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Name");
            return View();
        }

        // POST: ProductInventories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,LocationID,Shelf,Bin,Quantity,rowguid,ModifiedDate,isDeleted")] ProductInventory productInventory)
        {
            if (ModelState.IsValid)
            {
                db.ProductInventories.Add(productInventory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LocationID = new SelectList(db.Locations, "LocationID", "Name", productInventory.LocationID);
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Name", productInventory.ProductID);
            return View(productInventory);
        }

        // GET: ProductInventories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductInventory productInventory = db.ProductInventories.Find(id);
            if (productInventory == null)
            {
                return HttpNotFound();
            }
            ViewBag.LocationID = new SelectList(db.Locations, "LocationID", "Name", productInventory.LocationID);
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Name", productInventory.ProductID);
            return View(productInventory);
        }

        // POST: ProductInventories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductID,LocationID,Shelf,Bin,Quantity,rowguid,ModifiedDate,isDeleted")] ProductInventory productInventory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productInventory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LocationID = new SelectList(db.Locations, "LocationID", "Name", productInventory.LocationID);
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Name", productInventory.ProductID);
            return View(productInventory);
        }

        // GET: ProductInventories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductInventory productInventory = db.ProductInventories.Find(id);
            if (productInventory == null)
            {
                return HttpNotFound();
            }
            return View(productInventory);
        }

        // POST: ProductInventories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var res = (from c in db.ProductInventories
                       where c.ProductID == id
                       select c).FirstOrDefault();

            if (res != null)
            {
                res.isDeleted = true;
                db.SaveChanges();
                ViewBag.Message = string.Format("Congrats! Delete success");
            }

            ProductInventory productCategory = db.ProductInventories.Find(id);



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
