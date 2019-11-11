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
    public class PersonCreditCardsController : Controller
    {
        private AdventureWorks2008R2Entities db = new AdventureWorks2008R2Entities();

        // GET: PersonCreditCards
        public ActionResult Index()
        {
            var personCreditCards = db.PersonCreditCards.Include(p => p.Person).Include(p => p.CreditCard);
            return View(personCreditCards.ToList());
        }

        // GET: PersonCreditCards/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonCreditCard personCreditCard = db.PersonCreditCards.Find(id);
            if (personCreditCard == null)
            {
                return HttpNotFound();
            }
            return View(personCreditCard);
        }

        // GET: PersonCreditCards/Create
        public ActionResult Create()
        {
            ViewBag.BusinessEntityID = new SelectList(db.People, "BusinessEntityID", "PersonType");
            ViewBag.CreditCardID = new SelectList(db.CreditCards, "CreditCardID", "CardType");
            return View();
        }

        // POST: PersonCreditCards/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BusinessEntityID,CreditCardID,ModifiedDate,isDeleted")] PersonCreditCard personCreditCard)
        {
            if (ModelState.IsValid)
            {
                db.PersonCreditCards.Add(personCreditCard);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BusinessEntityID = new SelectList(db.People, "BusinessEntityID", "PersonType", personCreditCard.BusinessEntityID);
            ViewBag.CreditCardID = new SelectList(db.CreditCards, "CreditCardID", "CardType", personCreditCard.CreditCardID);
            return View(personCreditCard);
        }

        // GET: PersonCreditCards/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonCreditCard personCreditCard = db.PersonCreditCards.Find(id);
            if (personCreditCard == null)
            {
                return HttpNotFound();
            }
            ViewBag.BusinessEntityID = new SelectList(db.People, "BusinessEntityID", "PersonType", personCreditCard.BusinessEntityID);
            ViewBag.CreditCardID = new SelectList(db.CreditCards, "CreditCardID", "CardType", personCreditCard.CreditCardID);
            return View(personCreditCard);
        }

        // POST: PersonCreditCards/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BusinessEntityID,CreditCardID,ModifiedDate,isDeleted")] PersonCreditCard personCreditCard)
        {
            if (ModelState.IsValid)
            {
                db.Entry(personCreditCard).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BusinessEntityID = new SelectList(db.People, "BusinessEntityID", "PersonType", personCreditCard.BusinessEntityID);
            ViewBag.CreditCardID = new SelectList(db.CreditCards, "CreditCardID", "CardType", personCreditCard.CreditCardID);
            return View(personCreditCard);
        }

        // GET: PersonCreditCards/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonCreditCard personCreditCard = db.PersonCreditCards.Find(id);
            if (personCreditCard == null)
            {
                return HttpNotFound();
            }
            return View(personCreditCard);
        }

        // POST: PersonCreditCards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var res = (from c in db.PersonCreditCards
                       where c.BusinessEntityID == id
                       select c).FirstOrDefault();

            if (res != null)
            {
                res.isDeleted = true;
                db.SaveChanges();
                ViewBag.Message = string.Format("Congrats! Delete success");
            }

            PersonCreditCard personCreditCard = db.PersonCreditCards.Find(id);



            return View(personCreditCard);
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
