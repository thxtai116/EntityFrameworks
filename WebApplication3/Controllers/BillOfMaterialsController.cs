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
    public class BillOfMaterialsController : Controller
    {
        private AdventureWorks2008R2Entities db = new AdventureWorks2008R2Entities();

        // GET: BillOfMaterials
        public ActionResult Index()
        {
            var billOfMaterials = db.BillOfMaterials.Include(b => b.Product).Include(b => b.Product1);
            return View(billOfMaterials.ToList());
        }

        // GET: BillOfMaterials/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BillOfMaterial billOfMaterial = db.BillOfMaterials.Find(id);
            if (billOfMaterial == null)
            {
                return HttpNotFound();
            }
            return View(billOfMaterial);
        }

        // GET: BillOfMaterials/Create
        public ActionResult Create()
        {
            ViewBag.ComponentID = new SelectList(db.Products, "ProductID", "Name");
            ViewBag.ProductAssemblyID = new SelectList(db.Products, "ProductID", "Name");
            return View();
        }

        // POST: BillOfMaterials/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BillOfMaterialsID,ProductAssemblyID,ComponentID,StartDate,EndDate,UnitMeasureCode,BOMLevel,PerAssemblyQty,ModifiedDate,isDeleted")] BillOfMaterial billOfMaterial)
        {
            if (ModelState.IsValid)
            {
                db.BillOfMaterials.Add(billOfMaterial);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ComponentID = new SelectList(db.Products, "ProductID", "Name", billOfMaterial.ComponentID);
            ViewBag.ProductAssemblyID = new SelectList(db.Products, "ProductID", "Name", billOfMaterial.ProductAssemblyID);
            return View(billOfMaterial);
        }

        // GET: BillOfMaterials/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BillOfMaterial billOfMaterial = db.BillOfMaterials.Find(id);
            if (billOfMaterial == null)
            {
                return HttpNotFound();
            }
            ViewBag.ComponentID = new SelectList(db.Products, "ProductID", "Name", billOfMaterial.ComponentID);
            ViewBag.ProductAssemblyID = new SelectList(db.Products, "ProductID", "Name", billOfMaterial.ProductAssemblyID);
            return View(billOfMaterial);
        }

        // POST: BillOfMaterials/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BillOfMaterialsID,ProductAssemblyID,ComponentID,StartDate,EndDate,UnitMeasureCode,BOMLevel,PerAssemblyQty,ModifiedDate,isDeleted")] BillOfMaterial billOfMaterial)
        {
            if (ModelState.IsValid)
            {
                db.Entry(billOfMaterial).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ComponentID = new SelectList(db.Products, "ProductID", "Name", billOfMaterial.ComponentID);
            ViewBag.ProductAssemblyID = new SelectList(db.Products, "ProductID", "Name", billOfMaterial.ProductAssemblyID);
            return View(billOfMaterial);
        }

        // GET: BillOfMaterials/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BillOfMaterial billOfMaterial = db.BillOfMaterials.Find(id);
            if (billOfMaterial == null)
            {
                return HttpNotFound();
            }
            return View(billOfMaterial);
        }

        // POST: BillOfMaterials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var res = (from c in db.BillOfMaterials
                       where c.BillOfMaterialsID == id
                       select c).FirstOrDefault();

            if (res != null)
            {
                res.isDeleted = true;
                db.SaveChanges();
                ViewBag.Message = string.Format("Congrats! Delete success");
            }
            BillOfMaterial address = db.BillOfMaterials.Find(id);



            return View(address);
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
