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
    public class PasswordsController : Controller
    {
        private AdventureWorks2008R2Entities db = new AdventureWorks2008R2Entities();

        // GET: Passwords
        public ActionResult Index()
        {
            var passwords = db.Passwords.Include(p => p.Person);
            return View(passwords.ToList());
        }

        // GET: Passwords/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Password password = db.Passwords.Find(id);
            if (password == null)
            {
                return HttpNotFound();
            }
            return View(password);
        }

        // GET: Passwords/Create
        public ActionResult Create()
        {
            ViewBag.BusinessEntityID = new SelectList(db.People, "BusinessEntityID", "PersonType");
            return View();
        }

        // POST: Passwords/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BusinessEntityID,PasswordHash,PasswordSalt,rowguid,ModifiedDate,isDeleted")] Password password)
        {
            if (ModelState.IsValid)
            {
                db.Passwords.Add(password);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BusinessEntityID = new SelectList(db.People, "BusinessEntityID", "PersonType", password.BusinessEntityID);
            return View(password);
        }

        // GET: Passwords/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Password password = db.Passwords.Find(id);
            if (password == null)
            {
                return HttpNotFound();
            }
            ViewBag.BusinessEntityID = new SelectList(db.People, "BusinessEntityID", "PersonType", password.BusinessEntityID);
            return View(password);
        }

        // POST: Passwords/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BusinessEntityID,PasswordHash,PasswordSalt,rowguid,ModifiedDate,isDeleted")] Password password)
        {
            if (ModelState.IsValid)
            {
                db.Entry(password).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BusinessEntityID = new SelectList(db.People, "BusinessEntityID", "PersonType", password.BusinessEntityID);
            return View(password);
        }

        // GET: Passwords/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Password password = db.Passwords.Find(id);
            if (password == null)
            {
                return HttpNotFound();
            }
            return View(password);
        }

        // POST: Passwords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var res = (from c in db.Passwords
                       where c.BusinessEntityID == id
                       select c).FirstOrDefault();

            if (res != null)
            {
                res.isDeleted = true;
                db.SaveChanges();
                ViewBag.Message = string.Format("Congrats! Delete success");
            }

            Password password = db.Passwords.Find(id);



            return View(password);
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
