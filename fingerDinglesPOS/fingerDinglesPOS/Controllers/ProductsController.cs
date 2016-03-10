using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using fingerDinglesPOS.Models;

namespace fingerDinglesPOS.Controllers
{
    public class ProductsController : Controller
    {
        private FingerDinglesPosDB db = new FingerDinglesPosDB();

        // GET: Products
        public ActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.ProductNameSortParm = String.IsNullOrEmpty(sortOrder) ? "ProductName_desc" : " ";
            var prod = from s in db.product where s.ProductImageName != null
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                prod = prod.Where(s => s.ProductName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "ProductName_desc":
                    prod = prod.OrderByDescending(s => s.ProductName);
                    break;
                default:
                    prod = prod.OrderBy(s => s.ProductName);
                    break;
            }

                    return View(prod.ToList());
        }
        //public ActionResult AddToInventory([Bind(Include = "Quantity")] Product product, int id)
        //{
        //    Edit([Bind(Include = "ID,SalesPrice,UnitPrice,ProductName,Description,Sku,Quantity")] Product product,
        //    Product prod = db.product.Find(id);
        //    int changeQuantity = prod.Quantity;
        //    return View(db.product.ToList());
        //}
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,SalesPrice,UnitPrice,ProductName,Description,Sku,Quantity")] Product product, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {

                
                string path = Server.MapPath("~/Image/" + file.FileName);
                file.SaveAs(path);
                product.ProductImageName = file.FileName;
                db.product.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,SalesPrice,UnitPrice,ProductName,Description,Sku,Quantity")] Product product, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                string path = Server.MapPath("~/Image/" + file.FileName);
                file.SaveAs(path);
                product.ProductImageName = file.FileName;
                db.product.Add(product);
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.product.Find(id);
            db.product.Remove(product);
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
