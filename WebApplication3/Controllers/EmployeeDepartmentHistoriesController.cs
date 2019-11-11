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
    public class EmployeeDepartmentHistoriesController : Controller
    {
        private AdventureWorks2008R2Entities db = new AdventureWorks2008R2Entities();

        // GET: EmployeeDepartmentHistories
        public ActionResult Index()
        {
            var employeeDepartmentHistories = db.EmployeeDepartmentHistories.Include(e => e.Department).Include(e => e.Employee).Include(e => e.Shift);
            return View(employeeDepartmentHistories.ToList());
        }

        // GET: EmployeeDepartmentHistories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeDepartmentHistory employeeDepartmentHistory = db.EmployeeDepartmentHistories.Find(id);
            if (employeeDepartmentHistory == null)
            {
                return HttpNotFound();
            }
            return View(employeeDepartmentHistory);
        }

        // GET: EmployeeDepartmentHistories/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name");
            ViewBag.BusinessEntityID = new SelectList(db.Employees, "BusinessEntityID", "NationalIDNumber");
            ViewBag.ShiftID = new SelectList(db.Shifts, "ShiftID", "Name");
            return View();
        }

        // POST: EmployeeDepartmentHistories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BusinessEntityID,DepartmentID,ShiftID,StartDate,EndDate,ModifiedDate,isDelete")] EmployeeDepartmentHistory employeeDepartmentHistory)
        {
            if (ModelState.IsValid)
            {
                db.EmployeeDepartmentHistories.Add(employeeDepartmentHistory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name", employeeDepartmentHistory.DepartmentID);
            ViewBag.BusinessEntityID = new SelectList(db.Employees, "BusinessEntityID", "NationalIDNumber", employeeDepartmentHistory.BusinessEntityID);
            ViewBag.ShiftID = new SelectList(db.Shifts, "ShiftID", "Name", employeeDepartmentHistory.ShiftID);
            return View(employeeDepartmentHistory);
        }

        // GET: EmployeeDepartmentHistories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeDepartmentHistory employeeDepartmentHistory = db.EmployeeDepartmentHistories.Find(id);
            if (employeeDepartmentHistory == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name", employeeDepartmentHistory.DepartmentID);
            ViewBag.BusinessEntityID = new SelectList(db.Employees, "BusinessEntityID", "NationalIDNumber", employeeDepartmentHistory.BusinessEntityID);
            ViewBag.ShiftID = new SelectList(db.Shifts, "ShiftID", "Name", employeeDepartmentHistory.ShiftID);
            return View(employeeDepartmentHistory);
        }

        // POST: EmployeeDepartmentHistories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BusinessEntityID,DepartmentID,ShiftID,StartDate,EndDate,ModifiedDate,isDelete")] EmployeeDepartmentHistory employeeDepartmentHistory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employeeDepartmentHistory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name", employeeDepartmentHistory.DepartmentID);
            ViewBag.BusinessEntityID = new SelectList(db.Employees, "BusinessEntityID", "NationalIDNumber", employeeDepartmentHistory.BusinessEntityID);
            ViewBag.ShiftID = new SelectList(db.Shifts, "ShiftID", "Name", employeeDepartmentHistory.ShiftID);
            return View(employeeDepartmentHistory);
        }

        // GET: EmployeeDepartmentHistories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeDepartmentHistory employeeDepartmentHistory = db.EmployeeDepartmentHistories.Find(id);
            if (employeeDepartmentHistory == null)
            {
                return HttpNotFound();
            }
            return View(employeeDepartmentHistory);
        }

        // POST: EmployeeDepartmentHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var res = (from c in db.EmployeeDepartmentHistories
                       where c.DepartmentID == id
                       select c).FirstOrDefault();

            if (res != null)
            {
                res.isDelete = true;
                db.SaveChanges();
                ViewBag.Message = string.Format("Congrats! Delete success");
            }

            EmployeeDepartmentHistory employeeDepartmentHistory = db.EmployeeDepartmentHistories.Find(id);



            return View(employeeDepartmentHistory);
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
