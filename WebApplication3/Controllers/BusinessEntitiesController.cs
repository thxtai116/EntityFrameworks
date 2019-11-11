using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication3;
using WebApplication3.MessageBox;

namespace WebApplication3.Controllers
{
    public class BusinessEntitiesController : Controller
    {
        private AdventureWorks2008R2Entities db = new AdventureWorks2008R2Entities();

        // GET: BusinessEntities
        public ActionResult Index()
        {
            var businessEntities = db.BusinessEntities.Include(b => b.Person);
            
            return View(businessEntities.ToList());
        }

        // GET: BusinessEntities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BusinessEntity businessEntity = db.BusinessEntities.Find(id);
            if (businessEntity == null)
            {
                return HttpNotFound();
            }
            return View(businessEntity);
        }

        // GET: BusinessEntities/Create
        public ActionResult Create()
        {
            ViewBag.BusinessEntityID = new SelectList(db.People, "BusinessEntityID", "FirstName");
            return View();
        }

        // POST: BusinessEntities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BusinessEntityID,rowguid,ModifiedDate,isDeleted")] BusinessEntity businessEntity)
        {
            if (ModelState.IsValid)
            {
                db.BusinessEntities.Add(businessEntity);
                db.SaveChanges();
                return RedirectToAction("Index");

            }

            ViewBag.BusinessEntityID = new SelectList(db.People, "BusinessEntityID", "FirstName", businessEntity.BusinessEntityID);
            return View(businessEntity);
        }

        // GET: BusinessEntities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BusinessEntity businessEntity = db.BusinessEntities.Find(id);
            if (businessEntity == null)
            {
                return HttpNotFound();
            }
            ViewBag.BusinessEntityID = new SelectList(db.People, "BusinessEntityID", "FirstName", businessEntity.BusinessEntityID);
            return View(businessEntity);
        }

        // POST: BusinessEntities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BusinessEntityID,rowguid,ModifiedDate,isDeleted")] BusinessEntity businessEntity)
        {
            if (ModelState.IsValid)
            {
                db.Entry(businessEntity).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BusinessEntityID = new SelectList(db.People, "BusinessEntityID", "FirstName", businessEntity.BusinessEntityID);
            return View(businessEntity);
        }
     
        // GET: BusinessEntities/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BusinessEntity businessEntity = db.BusinessEntities.Find(id);
            if (businessEntity == null)
            {
                return HttpNotFound();
            }
            return View(businessEntity);
        }

        // POST: BusinessEntities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var res = (from c in db.BusinessEntities
                       where c.BusinessEntityID == id
                       select c).FirstOrDefault();
          
            if (res != null)
            {
                res.isDeleted = true;
                db.SaveChanges();
                ViewBag.Message = string.Format("Congrats! Delete success");
            }
            BusinessEntity businessEntity = db.BusinessEntities.Find(id);



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
