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
    public class PeopleController : Controller
    {
        private AdventureWorks2008R2Entities db = new AdventureWorks2008R2Entities();

        // GET: People
        public ActionResult Index()
        {
            var people = db.People.Include(p => p.BusinessEntity).Include(p => p.Employee);
            return View(people.ToList());
        }

        // GET: People/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.People.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // GET: People/Create
        public ActionResult Create()
        {
            ViewBag.BusinessEntityID = new SelectList(db.BusinessEntities, "BusinessEntityID", "BusinessEntityID");
            ViewBag.BusinessEntityID = new SelectList(db.Employees, "BusinessEntityID", "NationalIDNumber");
            return View();
        }

        // POST: People/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BusinessEntityID,PersonType,NameStyle,Title,FirstName,MiddleName,LastName,Suffix,EmailPromotion,AdditionalContactInfo,Demographics,rowguid,ModifiedDate")] Person person)
        {
            if (ModelState.IsValid)
            {
                db.People.Add(person);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BusinessEntityID = new SelectList(db.BusinessEntities, "BusinessEntityID", "BusinessEntityID", person.BusinessEntityID);
            ViewBag.BusinessEntityID = new SelectList(db.Employees, "BusinessEntityID", "NationalIDNumber", person.BusinessEntityID);
            return View(person);
        }

        // GET: People/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.People.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            ViewBag.BusinessEntityID = new SelectList(db.BusinessEntities, "BusinessEntityID", "BusinessEntityID", person.BusinessEntityID);
            ViewBag.BusinessEntityID = new SelectList(db.Employees, "BusinessEntityID", "NationalIDNumber", person.BusinessEntityID);
            return View(person);
        }

        // POST: People/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BusinessEntityID,PersonType,NameStyle,Title,FirstName,MiddleName,LastName,Suffix,EmailPromotion,AdditionalContactInfo,Demographics,rowguid,ModifiedDate")] Person person)
        {
            if (ModelState.IsValid)
            {
                db.Entry(person).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BusinessEntityID = new SelectList(db.BusinessEntities, "BusinessEntityID", "BusinessEntityID", person.BusinessEntityID);
            ViewBag.BusinessEntityID = new SelectList(db.Employees, "BusinessEntityID", "NationalIDNumber", person.BusinessEntityID);
            return View(person);
        }

        // GET: People/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.People.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var res = (from c in db.People
                       where c.BusinessEntityID == id
                       select c).FirstOrDefault();

            if (res != null)
            {
                res.isDeleted = true;
                db.SaveChanges();
                ViewBag.Message = string.Format("Congrats! Delete success");
            }

            Person person = db.People.Find(id);



            return View(person);
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
