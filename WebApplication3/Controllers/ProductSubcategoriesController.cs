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
    public class ProductSubcategoriesController : Controller
    {
        private AdventureWorks2008R2Entities db = new AdventureWorks2008R2Entities();

        // GET: ProductSubcategories
        public ActionResult Index()
        {
            var productSubcategories = db.ProductSubcategories.Include(p => p.ProductCategory);
            return View(productSubcategories.ToList());
        }

        // GET: ProductSubcategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductSubcategory productSubcategory = db.ProductSubcategories.Find(id);
            if (productSubcategory == null)
            {
                return HttpNotFound();
            }
            return View(productSubcategory);
        }

        // GET: ProductSubcategories/Create
        public ActionResult Create()
        {
            ViewBag.ProductCategoryID = new SelectList(db.ProductCategories, "ProductCategoryID", "Name");
            return View();
        }

        // POST: ProductSubcategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductSubcategoryID,ProductCategoryID,Name,rowguid,ModifiedDate,isDeleted")] ProductSubcategory productSubcategory)
        {
            if (ModelState.IsValid)
            {
                db.ProductSubcategories.Add(productSubcategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductCategoryID = new SelectList(db.ProductCategories, "ProductCategoryID", "Name", productSubcategory.ProductCategoryID);
            return View(productSubcategory);
        }

        // GET: ProductSubcategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductSubcategory productSubcategory = db.ProductSubcategories.Find(id);
            if (productSubcategory == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductCategoryID = new SelectList(db.ProductCategories, "ProductCategoryID", "Name", productSubcategory.ProductCategoryID);
            return View(productSubcategory);
        }

        // POST: ProductSubcategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductSubcategoryID,ProductCategoryID,Name,rowguid,ModifiedDate,isDeleted")] ProductSubcategory productSubcategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productSubcategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductCategoryID = new SelectList(db.ProductCategories, "ProductCategoryID", "Name", productSubcategory.ProductCategoryID);
            return View(productSubcategory);
        }

        // GET: ProductSubcategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductSubcategory productSubcategory = db.ProductSubcategories.Find(id);
            if (productSubcategory == null)
            {
                return HttpNotFound();
            }
            return View(productSubcategory);
        }

        // POST: ProductSubcategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var res = (from c in db.ProductSubcategories
                       where c.ProductCategoryID == id
                       select c).FirstOrDefault();

            if (res != null)
            {
                res.isDeleted = true;
                db.SaveChanges();
                ViewBag.Message = string.Format("Congrats! Delete success");
            }

            ProductSubcategory product = db.ProductSubcategories.Find(id);



            return View(product);
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
