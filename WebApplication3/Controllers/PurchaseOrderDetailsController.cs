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
    public class PurchaseOrderDetailsController : Controller
    {
        private AdventureWorks2008R2Entities db = new AdventureWorks2008R2Entities();

        // GET: PurchaseOrderDetails
        public ActionResult Index()
        {
            var purchaseOrderDetails = db.PurchaseOrderDetails.Include(p => p.Product).Include(p => p.PurchaseOrderHeader);
            return View(purchaseOrderDetails.ToList());
        }

        // GET: PurchaseOrderDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseOrderDetail purchaseOrderDetail = db.PurchaseOrderDetails.Find(id);
            if (purchaseOrderDetail == null)
            {
                return HttpNotFound();
            }
            return View(purchaseOrderDetail);
        }

        // GET: PurchaseOrderDetails/Create
        public ActionResult Create()
        {
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Name");
            ViewBag.PurchaseOrderID = new SelectList(db.PurchaseOrderHeaders, "PurchaseOrderID", "PurchaseOrderID");
            return View();
        }

        // POST: PurchaseOrderDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PurchaseOrderID,PurchaseOrderDetailID,DueDate,OrderQty,ProductID,UnitPrice,LineTotal,ReceivedQty,RejectedQty,StockedQty,ModifiedDate,isDeleted")] PurchaseOrderDetail purchaseOrderDetail)
        {
            if (ModelState.IsValid)
            {
                db.PurchaseOrderDetails.Add(purchaseOrderDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Name", purchaseOrderDetail.ProductID);
            ViewBag.PurchaseOrderID = new SelectList(db.PurchaseOrderHeaders, "PurchaseOrderID", "PurchaseOrderID", purchaseOrderDetail.PurchaseOrderID);
            return View(purchaseOrderDetail);
        }

        // GET: PurchaseOrderDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseOrderDetail purchaseOrderDetail = db.PurchaseOrderDetails.Find(id);
            if (purchaseOrderDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Name", purchaseOrderDetail.ProductID);
            ViewBag.PurchaseOrderID = new SelectList(db.PurchaseOrderHeaders, "PurchaseOrderID", "PurchaseOrderID", purchaseOrderDetail.PurchaseOrderID);
            return View(purchaseOrderDetail);
        }

        // POST: PurchaseOrderDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PurchaseOrderID,PurchaseOrderDetailID,DueDate,OrderQty,ProductID,UnitPrice,LineTotal,ReceivedQty,RejectedQty,StockedQty,ModifiedDate,isDeleted")] PurchaseOrderDetail purchaseOrderDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(purchaseOrderDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Name", purchaseOrderDetail.ProductID);
            ViewBag.PurchaseOrderID = new SelectList(db.PurchaseOrderHeaders, "PurchaseOrderID", "PurchaseOrderID", purchaseOrderDetail.PurchaseOrderID);
            return View(purchaseOrderDetail);
        }

        // GET: PurchaseOrderDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseOrderDetail purchaseOrderDetail = db.PurchaseOrderDetails.Find(id);
            if (purchaseOrderDetail == null)
            {
                return HttpNotFound();
            }
            return View(purchaseOrderDetail);
        }

        // POST: PurchaseOrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var res = (from c in db.PurchaseOrderDetails
                       where c.ProductID == id
                       select c).FirstOrDefault();

            if (res != null)
            {
                res.isDeleted = true;
                db.SaveChanges();
                ViewBag.Message = string.Format("Congrats! Delete success");
            }

            PurchaseOrderDetail purchaseOrderDetail = db.PurchaseOrderDetails.Find(id);



            return View(purchaseOrderDetail);
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
