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
    public class PersonPhonesController : Controller
    {
        private AdventureWorks2008R2Entities db = new AdventureWorks2008R2Entities();

        // GET: PersonPhones
        public ActionResult Index()
        {
            var personPhones = db.PersonPhones.Include(p => p.Person).Include(p => p.PhoneNumberType);
            return View(personPhones.ToList());
        }

        // GET: PersonPhones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonPhone personPhone = db.PersonPhones.Find(id);
            if (personPhone == null)
            {
                return HttpNotFound();
            }
            return View(personPhone);
        }

        // GET: PersonPhones/Create
        public ActionResult Create()
        {
            ViewBag.BusinessEntityID = new SelectList(db.People, "BusinessEntityID", "PersonType");
            ViewBag.PhoneNumberTypeID = new SelectList(db.PhoneNumberTypes, "PhoneNumberTypeID", "Name");
            return View();
        }

        // POST: PersonPhones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BusinessEntityID,PhoneNumber,PhoneNumberTypeID,ModifiedDate,isDeleted")] PersonPhone personPhone)
        {
            if (ModelState.IsValid)
            {
                db.PersonPhones.Add(personPhone);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BusinessEntityID = new SelectList(db.People, "BusinessEntityID", "PersonType", personPhone.BusinessEntityID);
            ViewBag.PhoneNumberTypeID = new SelectList(db.PhoneNumberTypes, "PhoneNumberTypeID", "Name", personPhone.PhoneNumberTypeID);
            return View(personPhone);
        }

        // GET: PersonPhones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonPhone personPhone = db.PersonPhones.Find(id);
            if (personPhone == null)
            {
                return HttpNotFound();
            }
            ViewBag.BusinessEntityID = new SelectList(db.People, "BusinessEntityID", "PersonType", personPhone.BusinessEntityID);
            ViewBag.PhoneNumberTypeID = new SelectList(db.PhoneNumberTypes, "PhoneNumberTypeID", "Name", personPhone.PhoneNumberTypeID);
            return View(personPhone);
        }

        // POST: PersonPhones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BusinessEntityID,PhoneNumber,PhoneNumberTypeID,ModifiedDate,isDeleted")] PersonPhone personPhone)
        {
            if (ModelState.IsValid)
            {
                db.Entry(personPhone).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BusinessEntityID = new SelectList(db.People, "BusinessEntityID", "PersonType", personPhone.BusinessEntityID);
            ViewBag.PhoneNumberTypeID = new SelectList(db.PhoneNumberTypes, "PhoneNumberTypeID", "Name", personPhone.PhoneNumberTypeID);
            return View(personPhone);
        }

        // GET: PersonPhones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonPhone personPhone = db.PersonPhones.Find(id);
            if (personPhone == null)
            {
                return HttpNotFound();
            }
            return View(personPhone);
        }

        // POST: PersonPhones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var res = (from c in db.PersonPhones
                       where c.BusinessEntityID == id
                       select c).FirstOrDefault();

            if (res != null)
            {
                res.isDeleted = true;
                db.SaveChanges();
                ViewBag.Message = string.Format("Congrats! Delete success");
            }

            PersonPhone personPhone = db.PersonPhones.Find(id);



            return View(personPhone);
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
