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
    public class StateProvincesController : Controller
    {
        private AdventureWorks2008R2Entities db = new AdventureWorks2008R2Entities();

        // GET: StateProvinces
        public ActionResult Index()
        {
            return View(db.StateProvinces.ToList());
        }

        // GET: StateProvinces/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StateProvince stateProvince = db.StateProvinces.Find(id);
            if (stateProvince == null)
            {
                return HttpNotFound();
            }
            return View(stateProvince);
        }

        // GET: StateProvinces/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StateProvinces/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StateProvinceID,StateProvinceCode,CountryRegionCode,IsOnlyStateProvinceFlag,Name,TerritoryID,rowguid,ModifiedDate,isDeleted")] StateProvince stateProvince)
        {
            if (ModelState.IsValid)
            {
                db.StateProvinces.Add(stateProvince);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(stateProvince);
        }

        // GET: StateProvinces/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StateProvince stateProvince = db.StateProvinces.Find(id);
            if (stateProvince == null)
            {
                return HttpNotFound();
            }
            return View(stateProvince);
        }

        // POST: StateProvinces/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StateProvinceID,StateProvinceCode,CountryRegionCode,IsOnlyStateProvinceFlag,Name,TerritoryID,rowguid,ModifiedDate,isDeleted")] StateProvince stateProvince)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stateProvince).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(stateProvince);
        }

        // GET: StateProvinces/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StateProvince stateProvince = db.StateProvinces.Find(id);
            if (stateProvince == null)
            {
                return HttpNotFound();
            }
            return View(stateProvince);
        }


        public StateProvince GetById(long stateID)
        {
            return db
                .StateProvinces
                .FirstOrDefault(x => x.StateProvinceID == stateID);
        }
        // POST: StateProvinces/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            StateProvince stateProvince = db.StateProvinces.Find(id);
            //Address address = db.Addresses.Find(id);

            //if (stateProvince.isDeleted == true)
            //{
            //    //var salesOrderHeaders = db.SalesOrderHeaders.Where(x => x.BillToAddressID == address.AddressID && x.ShipToAddressID == address.AddressID);
            //    //db.SalesOrderHeaders.RemoveRange(salesOrderHeaders);
            //    var addresses = db.Addresses.Where(x => x.StateProvinceID == stateProvince.StateProvinceID);
            //    db.Addresses.RemoveRange(addresses);


            //}
            //db.StateProvinces.Remove(stateProvince);
            //using (var context = new AdventureWorks2008R2Entities())
            //{
            //    var address = new Address { StateProvince=stateProvince };
            //    context.StateProvinces.Remove(stateProvince);
            //    context.SaveChanges();
            //}

            var stateProvinces = GetById(id);
            stateProvinces.isDeleted = true;
            db.StateProvinces.Remove(stateProvince);
            db.Entry(stateProvince).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");
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
