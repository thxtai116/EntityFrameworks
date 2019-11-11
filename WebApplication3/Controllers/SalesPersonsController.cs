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
    public class SalesPersonsController : Controller
    {
        private AdventureWorks2008R2Entities db = new AdventureWorks2008R2Entities();

        // GET: SalesPersons
        public ActionResult Index()
        {
            var salesPersons = db.SalesPersons.Include(s => s.Employee).Include(s => s.SalesTerritory);
            return View(salesPersons.ToList());
        }

        // GET: SalesPersons/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesPerson salesPerson = db.SalesPersons.Find(id);
            if (salesPerson == null)
            {
                return HttpNotFound();
            }
            return View(salesPerson);
        }

        // GET: SalesPersons/Create
        public ActionResult Create()
        {
            ViewBag.BusinessEntityID = new SelectList(db.Employees, "BusinessEntityID", "NationalIDNumber");
            ViewBag.TerritoryID = new SelectList(db.SalesTerritories, "TerritoryID", "Name");
            return View();
        }

        // POST: SalesPersons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BusinessEntityID,TerritoryID,SalesQuota,Bonus,CommissionPct,SalesYTD,SalesLastYear,rowguid,ModifiedDate,isDeleted")] SalesPerson salesPerson)
        {
            if (ModelState.IsValid)
            {
                db.SalesPersons.Add(salesPerson);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BusinessEntityID = new SelectList(db.Employees, "BusinessEntityID", "NationalIDNumber", salesPerson.BusinessEntityID);
            ViewBag.TerritoryID = new SelectList(db.SalesTerritories, "TerritoryID", "Name", salesPerson.TerritoryID);
            return View(salesPerson);
        }

        // GET: SalesPersons/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesPerson salesPerson = db.SalesPersons.Find(id);
            if (salesPerson == null)
            {
                return HttpNotFound();
            }
            ViewBag.BusinessEntityID = new SelectList(db.Employees, "BusinessEntityID", "NationalIDNumber", salesPerson.BusinessEntityID);
            ViewBag.TerritoryID = new SelectList(db.SalesTerritories, "TerritoryID", "Name", salesPerson.TerritoryID);
            return View(salesPerson);
        }

        // POST: SalesPersons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BusinessEntityID,TerritoryID,SalesQuota,Bonus,CommissionPct,SalesYTD,SalesLastYear,rowguid,ModifiedDate,isDeleted")] SalesPerson salesPerson)
        {
            if (ModelState.IsValid)
            {
                db.Entry(salesPerson).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BusinessEntityID = new SelectList(db.Employees, "BusinessEntityID", "NationalIDNumber", salesPerson.BusinessEntityID);
            ViewBag.TerritoryID = new SelectList(db.SalesTerritories, "TerritoryID", "Name", salesPerson.TerritoryID);
            return View(salesPerson);
        }

        // GET: SalesPersons/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesPerson salesPerson = db.SalesPersons.Find(id);
            if (salesPerson == null)
            {
                return HttpNotFound();
            }
            return View(salesPerson);
        }

        // POST: SalesPersons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var res = (from c in db.SalesPersons
                       where c.BusinessEntityID == id
                       select c).FirstOrDefault();

            if (res != null)
            {
                res.isDeleted = true;
                db.SaveChanges();
                ViewBag.Message = string.Format("Congrats! Delete success");
            }

            SalesPerson salesPerson = db.SalesPersons.Find(id);



            return View(salesPerson);
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
