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
    public class ProductDocumentsController : Controller
    {
        private AdventureWorks2008R2Entities db = new AdventureWorks2008R2Entities();

        // GET: ProductDocuments
        public ActionResult Index()
        {
            var productDocuments = db.ProductDocuments.Include(p => p.Product);
            return View(productDocuments.ToList());
        }

        // GET: ProductDocuments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductDocument productDocument = db.ProductDocuments.Find(id);
            if (productDocument == null)
            {
                return HttpNotFound();
            }
            return View(productDocument);
        }

        // GET: ProductDocuments/Create
        public ActionResult Create()
        {
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Name");
            return View();
        }

        // POST: ProductDocuments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,ModifiedDate,isDeleted")] ProductDocument productDocument)
        {
            if (ModelState.IsValid)
            {
                db.ProductDocuments.Add(productDocument);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Name", productDocument.ProductID);
            return View(productDocument);
        }

        // GET: ProductDocuments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductDocument productDocument = db.ProductDocuments.Find(id);
            if (productDocument == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Name", productDocument.ProductID);
            return View(productDocument);
        }

        // POST: ProductDocuments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductID,ModifiedDate,isDeleted")] ProductDocument productDocument)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productDocument).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Name", productDocument.ProductID);
            return View(productDocument);
        }

        // GET: ProductDocuments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductDocument productDocument = db.ProductDocuments.Find(id);
            if (productDocument == null)
            {
                return HttpNotFound();
            }
            return View(productDocument);
        }

        // POST: ProductDocuments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var res = (from c in db.ProductDocuments
                       where c.ProductID == id
                       select c).FirstOrDefault();

            if (res != null)
            {
                res.isDeleted = true;
                db.SaveChanges();
                ViewBag.Message = string.Format("Congrats! Delete success");
            }

            ProductDocument productCategory = db.ProductDocuments.Find(id);



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
