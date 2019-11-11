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
    public class ProductDescriptionsController : Controller
    {
        private AdventureWorks2008R2Entities db = new AdventureWorks2008R2Entities();

        // GET: ProductDescriptions
        public ActionResult Index()
        {
            return View(db.ProductDescriptions.ToList());
        }

        // GET: ProductDescriptions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductDescription productDescription = db.ProductDescriptions.Find(id);
            if (productDescription == null)
            {
                return HttpNotFound();
            }
            return View(productDescription);
        }

        // GET: ProductDescriptions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductDescriptions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductDescriptionID,Description,rowguid,ModifiedDate,isDeleted")] ProductDescription productDescription)
        {
            if (ModelState.IsValid)
            {
                db.ProductDescriptions.Add(productDescription);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(productDescription);
        }

        // GET: ProductDescriptions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductDescription productDescription = db.ProductDescriptions.Find(id);
            if (productDescription == null)
            {
                return HttpNotFound();
            }
            return View(productDescription);
        }

        // POST: ProductDescriptions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductDescriptionID,Description,rowguid,ModifiedDate,isDeleted")] ProductDescription productDescription)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productDescription).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productDescription);
        }

        // GET: ProductDescriptions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductDescription productDescription = db.ProductDescriptions.Find(id);
            if (productDescription == null)
            {
                return HttpNotFound();
            }
            return View(productDescription);
        }

        // POST: ProductDescriptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var res = (from c in db.ProductDescriptions
                       where c.ProductDescriptionID == id
                       select c).FirstOrDefault();

            if (res != null)
            {
                res.isDeleted = true;
                db.SaveChanges();
                ViewBag.Message = string.Format("Congrats! Delete success");
            }

            ProductDescription productCategory = db.ProductDescriptions.Find(id);



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
