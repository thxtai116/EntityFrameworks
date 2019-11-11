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
    public class AddressTypesController : Controller
    {
        private AdventureWorks2008R2Entities db = new AdventureWorks2008R2Entities();

        // GET: AddressTypes
        public ActionResult Index()
        {
            return View(db.AddressTypes.ToList());
        }

        // GET: AddressTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AddressType addressType = db.AddressTypes.Find(id);
            if (addressType == null)
            {
                return HttpNotFound();
            }
            return View(addressType);
        }

        // GET: AddressTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AddressTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AddressTypeID,Name,rowguid,ModifiedDate,isDeleted")] AddressType addressType)
        {
            if (ModelState.IsValid)
            {
                db.AddressTypes.Add(addressType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(addressType);
        }

        // GET: AddressTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AddressType addressType = db.AddressTypes.Find(id);
            if (addressType == null)
            {
                return HttpNotFound();
            }
            return View(addressType);
        }

        // POST: AddressTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AddressTypeID,Name,rowguid,ModifiedDate,isDeleted")] AddressType addressType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(addressType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(addressType);
        }

        // GET: AddressTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AddressType addressType = db.AddressTypes.Find(id);
            if (addressType == null)
            {
                return HttpNotFound();
            }
            return View(addressType);
        }

        // POST: AddressTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var res = (from c in db.AddressTypes
                       where c.AddressTypeID == id
                       select c).FirstOrDefault();

            if (res != null)
            {
                res.isDeleted = true;
                db.SaveChanges();
                ViewBag.Message = string.Format("Congrats! Delete success");
            }
            AddressType address = db.AddressTypes.Find(id);



            return View(address);
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
