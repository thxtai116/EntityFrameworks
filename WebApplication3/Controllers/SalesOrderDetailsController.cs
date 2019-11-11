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
    public class SalesOrderDetailsController : Controller
    {
        private AdventureWorks2008R2Entities db = new AdventureWorks2008R2Entities();

        // GET: SalesOrderDetails
        public ActionResult Index()
        {
            var salesOrderDetails = db.SalesOrderDetails.Include(s => s.SalesOrderHeader).Include(s => s.SpecialOfferProduct);
            return View(salesOrderDetails.ToList());
        }

        // GET: SalesOrderDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesOrderDetail salesOrderDetail = db.SalesOrderDetails.Find(id);
            if (salesOrderDetail == null)
            {
                return HttpNotFound();
            }
            return View(salesOrderDetail);
        }

        // GET: SalesOrderDetails/Create
        public ActionResult Create()
        {
            ViewBag.SalesOrderID = new SelectList(db.SalesOrderHeaders, "SalesOrderID", "SalesOrderNumber");
            ViewBag.SpecialOfferID = new SelectList(db.SpecialOfferProducts, "SpecialOfferID", "SpecialOfferID");
            return View();
        }

        // POST: SalesOrderDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SalesOrderID,SalesOrderDetailID,CarrierTrackingNumber,OrderQty,ProductID,SpecialOfferID,UnitPrice,UnitPriceDiscount,LineTotal,rowguid,ModifiedDate,isDeleted")] SalesOrderDetail salesOrderDetail)
        {
            if (ModelState.IsValid)
            {
                db.SalesOrderDetails.Add(salesOrderDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SalesOrderID = new SelectList(db.SalesOrderHeaders, "SalesOrderID", "SalesOrderNumber", salesOrderDetail.SalesOrderID);
            ViewBag.SpecialOfferID = new SelectList(db.SpecialOfferProducts, "SpecialOfferID", "SpecialOfferID", salesOrderDetail.SpecialOfferID);
            return View(salesOrderDetail);
        }

        // GET: SalesOrderDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesOrderDetail salesOrderDetail = db.SalesOrderDetails.Find(id);
            if (salesOrderDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.SalesOrderID = new SelectList(db.SalesOrderHeaders, "SalesOrderID", "SalesOrderNumber", salesOrderDetail.SalesOrderID);
            ViewBag.SpecialOfferID = new SelectList(db.SpecialOfferProducts, "SpecialOfferID", "SpecialOfferID", salesOrderDetail.SpecialOfferID);
            return View(salesOrderDetail);
        }

        // POST: SalesOrderDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SalesOrderID,SalesOrderDetailID,CarrierTrackingNumber,OrderQty,ProductID,SpecialOfferID,UnitPrice,UnitPriceDiscount,LineTotal,rowguid,ModifiedDate,isDeleted")] SalesOrderDetail salesOrderDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(salesOrderDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SalesOrderID = new SelectList(db.SalesOrderHeaders, "SalesOrderID", "SalesOrderNumber", salesOrderDetail.SalesOrderID);
            ViewBag.SpecialOfferID = new SelectList(db.SpecialOfferProducts, "SpecialOfferID", "SpecialOfferID", salesOrderDetail.SpecialOfferID);
            return View(salesOrderDetail);
        }

        // GET: SalesOrderDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesOrderDetail salesOrderDetail = db.SalesOrderDetails.Find(id);
            if (salesOrderDetail == null)
            {
                return HttpNotFound();
            }
            return View(salesOrderDetail);
        }

        // POST: SalesOrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var res = (from c in db.SalesOrderDetails
                       where c.ProductID == id
                       select c).FirstOrDefault();

            if (res != null)
            {
                res.isDeleted = true;
                db.SaveChanges();
                ViewBag.Message = string.Format("Congrats! Delete success");
            }

            SalesOrderDetail salesOrderDetail = db.SalesOrderDetails.Find(id);



            return View(salesOrderDetail);
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
