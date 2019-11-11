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
    public class BusinessEntityContactsController : Controller
    {
        private AdventureWorks2008R2Entities db = new AdventureWorks2008R2Entities();

        // GET: BusinessEntityContacts
        public ActionResult Index()
        {
            var businessEntityContacts = db.BusinessEntityContacts.Include(b => b.BusinessEntity).Include(b => b.ContactType).Include(b => b.Person);
            return View(businessEntityContacts.ToList());
        }

        // GET: BusinessEntityContacts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BusinessEntityContact businessEntityContact = db.BusinessEntityContacts.Find(id);
            if (businessEntityContact == null)
            {
                return HttpNotFound();
            }
            return View(businessEntityContact);
        }

        // GET: BusinessEntityContacts/Create
        public ActionResult Create()
        {
            ViewBag.BusinessEntityID = new SelectList(db.BusinessEntities, "BusinessEntityID", "BusinessEntityID");
            ViewBag.ContactTypeID = new SelectList(db.ContactTypes, "ContactTypeID", "Name");
            ViewBag.PersonID = new SelectList(db.People, "BusinessEntityID", "PersonType");
            return View();
        }

        // POST: BusinessEntityContacts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BusinessEntityID,PersonID,ContactTypeID,rowguid,ModifiedDate,isDeleted")] BusinessEntityContact businessEntityContact)
        {
            if (ModelState.IsValid)
            {
                db.BusinessEntityContacts.Add(businessEntityContact);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BusinessEntityID = new SelectList(db.BusinessEntities, "BusinessEntityID", "BusinessEntityID", businessEntityContact.BusinessEntityID);
            ViewBag.ContactTypeID = new SelectList(db.ContactTypes, "ContactTypeID", "Name", businessEntityContact.ContactTypeID);
            ViewBag.PersonID = new SelectList(db.People, "BusinessEntityID", "PersonType", businessEntityContact.PersonID);
            return View(businessEntityContact);
        }

        // GET: BusinessEntityContacts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BusinessEntityContact businessEntityContact = db.BusinessEntityContacts.Find(id);
            if (businessEntityContact == null)
            {
                return HttpNotFound();
            }
            ViewBag.BusinessEntityID = new SelectList(db.BusinessEntities, "BusinessEntityID", "BusinessEntityID", businessEntityContact.BusinessEntityID);
            ViewBag.ContactTypeID = new SelectList(db.ContactTypes, "ContactTypeID", "Name", businessEntityContact.ContactTypeID);
            ViewBag.PersonID = new SelectList(db.People, "BusinessEntityID", "PersonType", businessEntityContact.PersonID);
            return View(businessEntityContact);
        }

        // POST: BusinessEntityContacts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BusinessEntityID,PersonID,ContactTypeID,rowguid,ModifiedDate,isDeleted")] BusinessEntityContact businessEntityContact)
        {
            if (ModelState.IsValid)
            {
                db.Entry(businessEntityContact).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BusinessEntityID = new SelectList(db.BusinessEntities, "BusinessEntityID", "BusinessEntityID", businessEntityContact.BusinessEntityID);
            ViewBag.ContactTypeID = new SelectList(db.ContactTypes, "ContactTypeID", "Name", businessEntityContact.ContactTypeID);
            ViewBag.PersonID = new SelectList(db.People, "BusinessEntityID", "PersonType", businessEntityContact.PersonID);
            return View(businessEntityContact);
        }

        // GET: BusinessEntityContacts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BusinessEntityContact businessEntityContact = db.BusinessEntityContacts.Find(id);
            if (businessEntityContact == null)
            {
                return HttpNotFound();
            }
            return View(businessEntityContact);
        }

        // POST: BusinessEntityContacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var res = (from c in db.BusinessEntityContacts
                       where c.BusinessEntityID == id
                       select c).FirstOrDefault();

            if (res != null)
            {
                res.isDeleted = true;
                db.SaveChanges();
                ViewBag.Message = string.Format("Congrats! Delete success");
            }

            BusinessEntityContact businessEntity = db.BusinessEntityContacts.Find(id);



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
