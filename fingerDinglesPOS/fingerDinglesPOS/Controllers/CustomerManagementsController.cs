using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using fingerDinglesPOS.Models;
using PagedList;

namespace fingerDinglesPOS.Controllers
{
    public class CustomerManagementsController : Controller
    {
        private FingerDinglesPosDB db = new FingerDinglesPosDB();

        // GET: CustomerManagements
        public ActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.FirstNameSortParm = String.IsNullOrEmpty(sortOrder) ? "FirstName_desc" :" ";
            ViewBag.LastNameSortParm = String.IsNullOrEmpty(sortOrder) ? "LastName_desc" : " ";
            var customerManagement = from s in db.customerManagement
                           select s;
            switch (sortOrder)
            {
                case "FirstName_desc":
                    customerManagement = customerManagement.OrderByDescending(s => s.FirstName);
                    break;
                case "LastName_desc":
                    customerManagement = customerManagement.OrderByDescending(s => s.LastName);
                    break;
                case "LastName_ord":
                    customerManagement = customerManagement.OrderBy(s => s.LastName);
                    break;
                default:
                    customerManagement = customerManagement.OrderBy(s => s.FirstName);
                    break;
            }
            if (!String.IsNullOrEmpty(searchString))
            {
                customerManagement = customerManagement.Where(s => s.LastName.Contains(searchString)
                                       || s.FirstName.Contains(searchString));
            }
            return View(customerManagement.ToList());
        }

        // GET: CustomerManagements/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerManagement customerManagement = db.customerManagement.Find(id);
            if (customerManagement == null)
            {
                return HttpNotFound();
            }
            return View(customerManagement);
        }

        // GET: CustomerManagements/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomerManagements/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FirstName,LastName,Email,Allergies,SpeicalNotes,PhoneNumber")] CustomerManagement customerManagement)
        {
            if (ModelState.IsValid)
            {
                db.customerManagement.Add(customerManagement);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customerManagement);
        }

        // GET: CustomerManagements/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerManagement customerManagement = db.customerManagement.Find(id);
            if (customerManagement == null)
            {
                return HttpNotFound();
            }
            return View(customerManagement);
        }

        // POST: CustomerManagements/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FirstName,LastName,Email,Allergies,SpeicalNotes,PhoneNumber")] CustomerManagement customerManagement)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customerManagement).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customerManagement);
        }

        // GET: CustomerManagements/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerManagement customerManagement = db.customerManagement.Find(id);
            if (customerManagement == null)
            {
                return HttpNotFound();
            }
            return View(customerManagement);
        }

        // POST: CustomerManagements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CustomerManagement customerManagement = db.customerManagement.Find(id);
            db.customerManagement.Remove(customerManagement);
            db.SaveChanges();
            return RedirectToAction("Index");
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
