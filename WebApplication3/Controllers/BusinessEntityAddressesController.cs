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
    public class BusinessEntityAddressesController : Controller
    {
        private AdventureWorks2008R2Entities db = new AdventureWorks2008R2Entities();

        // GET: BusinessEntityAddresses
        public ActionResult Index()
        {
            var businessEntityAddresses = db.BusinessEntityAddresses.Include(b => b.Address);
            return View(businessEntityAddresses.ToList());
        }

        // GET: BusinessEntityAddresses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BusinessEntityAddress businessEntityAddress = db.BusinessEntityAddresses.Find(id);
            if (businessEntityAddress == null)
            {
                return HttpNotFound();
            }
            return View(businessEntityAddress);
        }

        // GET: BusinessEntityAddresses/Create
        public ActionResult Create()
        {
            ViewBag.AddressID = new SelectList(db.Addresses, "AddressID", "AddressLine1");
            return View();
        }

        // POST: BusinessEntityAddresses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BusinessEntityID,AddressID,AddressTypeID,rowguid,ModifiedDate")] BusinessEntityAddress businessEntityAddress)
        {
            if (ModelState.IsValid)
            {
                db.BusinessEntityAddresses.Add(businessEntityAddress);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AddressID = new SelectList(db.Addresses, "AddressID", "AddressLine1", businessEntityAddress.AddressID);
            return View(businessEntityAddress);
        }

        // GET: BusinessEntityAddresses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BusinessEntityAddress businessEntityAddress = db.BusinessEntityAddresses.Find(id);
            if (businessEntityAddress == null)
            {
                return HttpNotFound();
            }
            ViewBag.AddressID = new SelectList(db.Addresses, "AddressID", "AddressLine1", businessEntityAddress.AddressID);
            return View(businessEntityAddress);
        }

        // POST: BusinessEntityAddresses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BusinessEntityID,AddressID,AddressTypeID,rowguid,ModifiedDate")] BusinessEntityAddress businessEntityAddress)
        {
            if (ModelState.IsValid)
            {
                db.Entry(businessEntityAddress).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AddressID = new SelectList(db.Addresses, "AddressID", "AddressLine1", businessEntityAddress.AddressID);
            return View(businessEntityAddress);
        }

        // GET: BusinessEntityAddresses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BusinessEntityAddress businessEntityAddress = db.BusinessEntityAddresses.Find(id);
            if (businessEntityAddress == null)
            {
                return HttpNotFound();
            }
            return View(businessEntityAddress);
        }

        // POST: BusinessEntityAddresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
             var res = (from c in db.BusinessEntityAddresses
                         where c.BusinessEntityID == id
                         select c).FirstOrDefault();

            if (res != null)
            {
                res.isDeleted = true;
                db.SaveChanges();
                ViewBag.Message = string.Format("Congrats! Delete success");
            }

            BusinessEntityAddress businessEntity = db.BusinessEntityAddresses.Find(id);



            return View(businessEntity);
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
