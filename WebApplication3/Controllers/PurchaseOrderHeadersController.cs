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
    public class PurchaseOrderHeadersController : Controller
    {
        private AdventureWorks2008R2Entities db = new AdventureWorks2008R2Entities();

        // GET: PurchaseOrderHeaders
        public ActionResult Index()
        {
            var purchaseOrderHeaders = db.PurchaseOrderHeaders.Include(p => p.Employee).Include(p => p.ShipMethod).Include(p => p.Vendor);
            return View(purchaseOrderHeaders.ToList());
        }

        // GET: PurchaseOrderHeaders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseOrderHeader purchaseOrderHeader = db.PurchaseOrderHeaders.Find(id);
            if (purchaseOrderHeader == null)
            {
                return HttpNotFound();
            }
            return View(purchaseOrderHeader);
        }

        // GET: PurchaseOrderHeaders/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeID = new SelectList(db.Employees, "BusinessEntityID", "NationalIDNumber");
            ViewBag.ShipMethodID = new SelectList(db.ShipMethods, "ShipMethodID", "Name");
            ViewBag.VendorID = new SelectList(db.Vendors, "BusinessEntityID", "AccountNumber");
            return View();
        }

        // POST: PurchaseOrderHeaders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PurchaseOrderID,RevisionNumber,Status,EmployeeID,VendorID,ShipMethodID,OrderDate,ShipDate,SubTotal,TaxAmt,Freight,TotalDue,ModifiedDate,isDeleted")] PurchaseOrderHeader purchaseOrderHeader)
        {
            if (ModelState.IsValid)
            {
                db.PurchaseOrderHeaders.Add(purchaseOrderHeader);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeID = new SelectList(db.Employees, "BusinessEntityID", "NationalIDNumber", purchaseOrderHeader.EmployeeID);
            ViewBag.ShipMethodID = new SelectList(db.ShipMethods, "ShipMethodID", "Name", purchaseOrderHeader.ShipMethodID);
            ViewBag.VendorID = new SelectList(db.Vendors, "BusinessEntityID", "AccountNumber", purchaseOrderHeader.VendorID);
            return View(purchaseOrderHeader);
        }

        // GET: PurchaseOrderHeaders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseOrderHeader purchaseOrderHeader = db.PurchaseOrderHeaders.Find(id);
            if (purchaseOrderHeader == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeID = new SelectList(db.Employees, "BusinessEntityID", "NationalIDNumber", purchaseOrderHeader.EmployeeID);
            ViewBag.ShipMethodID = new SelectList(db.ShipMethods, "ShipMethodID", "Name", purchaseOrderHeader.ShipMethodID);
            ViewBag.VendorID = new SelectList(db.Vendors, "BusinessEntityID", "AccountNumber", purchaseOrderHeader.VendorID);
            return View(purchaseOrderHeader);
        }

        // POST: PurchaseOrderHeaders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PurchaseOrderID,RevisionNumber,Status,EmployeeID,VendorID,ShipMethodID,OrderDate,ShipDate,SubTotal,TaxAmt,Freight,TotalDue,ModifiedDate,isDeleted")] PurchaseOrderHeader purchaseOrderHeader)
        {
            if (ModelState.IsValid)
            {
                db.Entry(purchaseOrderHeader).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeID = new SelectList(db.Employees, "BusinessEntityID", "NationalIDNumber", purchaseOrderHeader.EmployeeID);
            ViewBag.ShipMethodID = new SelectList(db.ShipMethods, "ShipMethodID", "Name", purchaseOrderHeader.ShipMethodID);
            ViewBag.VendorID = new SelectList(db.Vendors, "BusinessEntityID", "AccountNumber", purchaseOrderHeader.VendorID);
            return View(purchaseOrderHeader);
        }

        // GET: PurchaseOrderHeaders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseOrderHeader purchaseOrderHeader = db.PurchaseOrderHeaders.Find(id);
            if (purchaseOrderHeader == null)
            {
                return HttpNotFound();
            }
            return View(purchaseOrderHeader);
        }

        // POST: PurchaseOrderHeaders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            var res = (from c in db.PurchaseOrderHeaders
                       where c.EmployeeID == id
                       select c).FirstOrDefault();

            if (res != null)
            {
                res.isDeleted = true;
                db.SaveChanges();
                ViewBag.Message = string.Format("Congrats! Delete success");
            }

            PurchaseOrderHeader purchaseOrderHeader = db.PurchaseOrderHeaders.Find(id);



            return View(purchaseOrderHeader);
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
