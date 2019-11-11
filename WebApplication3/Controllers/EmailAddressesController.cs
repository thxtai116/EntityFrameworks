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
    public class EmailAddressesController : Controller
    {
        private AdventureWorks2008R2Entities db = new AdventureWorks2008R2Entities();

        // GET: EmailAddresses
        public ActionResult Index()
        {
            var emailAddresses = db.EmailAddresses.Include(e => e.Person);
            return View(emailAddresses.ToList());
        }

        // GET: EmailAddresses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmailAddress emailAddress = db.EmailAddresses.Find(id);
            if (emailAddress == null)
            {
                return HttpNotFound();
            }
            return View(emailAddress);
        }

        // GET: EmailAddresses/Create
        public ActionResult Create()
        {
            ViewBag.BusinessEntityID = new SelectList(db.People, "BusinessEntityID", "PersonType");
            return View();
        }

        // POST: EmailAddresses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BusinessEntityID,EmailAddressID,EmailAddress1,rowguid,ModifiedDate,isDeleted")] EmailAddress emailAddress)
        {
            if (ModelState.IsValid)
            {
                db.EmailAddresses.Add(emailAddress);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BusinessEntityID = new SelectList(db.People, "BusinessEntityID", "PersonType", emailAddress.BusinessEntityID);
            return View(emailAddress);
        }

        // GET: EmailAddresses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmailAddress emailAddress = db.EmailAddresses.Find(id);
            if (emailAddress == null)
            {
                return HttpNotFound();
            }
            ViewBag.BusinessEntityID = new SelectList(db.People, "BusinessEntityID", "PersonType", emailAddress.BusinessEntityID);
            return View(emailAddress);
        }

        // POST: EmailAddresses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BusinessEntityID,EmailAddressID,EmailAddress1,rowguid,ModifiedDate,isDeleted")] EmailAddress emailAddress)
        {
            if (ModelState.IsValid)
            {
                db.Entry(emailAddress).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BusinessEntityID = new SelectList(db.People, "BusinessEntityID", "PersonType", emailAddress.BusinessEntityID);
            return View(emailAddress);
        }

        // GET: EmailAddresses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmailAddress emailAddress = db.EmailAddresses.Find(id);
            if (emailAddress == null)
            {
                return HttpNotFound();
            }
            return View(emailAddress);
        }

        // POST: EmailAddresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var res = (from c in db.EmailAddresses
                       where c.EmailAddressID == id
                       select c).FirstOrDefault();

            if (res != null)
            {
                res.isDeleted = true;
                db.SaveChanges();
                ViewBag.Message = string.Format("Congrats! Delete success");
            }

            EmailAddress emailAddress = db.EmailAddresses.Find(id);



            return View(emailAddress);
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
