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
    public class JobCandidatesController : Controller
    {
        private AdventureWorks2008R2Entities db = new AdventureWorks2008R2Entities();

        // GET: JobCandidates
        public ActionResult Index()
        {
            var jobCandidates = db.JobCandidates.Include(j => j.Employee);
            return View(jobCandidates.ToList());
        }

        // GET: JobCandidates/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobCandidate jobCandidate = db.JobCandidates.Find(id);
            if (jobCandidate == null)
            {
                return HttpNotFound();
            }
            return View(jobCandidate);
        }

        // GET: JobCandidates/Create
        public ActionResult Create()
        {
            ViewBag.BusinessEntityID = new SelectList(db.Employees, "BusinessEntityID", "NationalIDNumber");
            return View();
        }

        // POST: JobCandidates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "JobCandidateID,BusinessEntityID,Resume,ModifiedDate,isDeleted")] JobCandidate jobCandidate)
        {
            if (ModelState.IsValid)
            {
                db.JobCandidates.Add(jobCandidate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BusinessEntityID = new SelectList(db.Employees, "BusinessEntityID", "NationalIDNumber", jobCandidate.BusinessEntityID);
            return View(jobCandidate);
        }

        // GET: JobCandidates/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobCandidate jobCandidate = db.JobCandidates.Find(id);
            if (jobCandidate == null)
            {
                return HttpNotFound();
            }
            ViewBag.BusinessEntityID = new SelectList(db.Employees, "BusinessEntityID", "NationalIDNumber", jobCandidate.BusinessEntityID);
            return View(jobCandidate);
        }

        // POST: JobCandidates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "JobCandidateID,BusinessEntityID,Resume,ModifiedDate,isDeleted")] JobCandidate jobCandidate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jobCandidate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BusinessEntityID = new SelectList(db.Employees, "BusinessEntityID", "NationalIDNumber", jobCandidate.BusinessEntityID);
            return View(jobCandidate);
        }

        // GET: JobCandidates/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobCandidate jobCandidate = db.JobCandidates.Find(id);
            if (jobCandidate == null)
            {
                return HttpNotFound();
            }
            return View(jobCandidate);
        }

        // POST: JobCandidates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var res = (from c in db.JobCandidates
                       where c.JobCandidateID == id
                       select c).FirstOrDefault();

            if (res != null)
            {
                res.isDeleted = true;
                db.SaveChanges();
                ViewBag.Message = string.Format("Congrats! Delete success");
            }

            JobCandidate jobCandidate = db.JobCandidates.Find(id);



            return View(jobCandidate);
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
