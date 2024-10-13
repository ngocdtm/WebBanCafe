using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebsiteBanCaPhe.Models;

namespace WebsiteBanCaPhe.Areas.Admin.Controllers
{
    public class CustomersAdController : Controller
    {
        private WebsiteCoffeeEntities1 db = new WebsiteCoffeeEntities1();

        // GET: Admin/Customers
        public ActionResult Index()
        {
            return View(db.Customers.ToList());
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer cus = db.Customers.Find(id);
            if (cus == null)
            {
                return HttpNotFound();
            }
            return View(cus);
        }

     

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDCus,NameCus,PhoneCus,Address,PassCus,Status")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                var CusDB = db.Customers.FirstOrDefault(p => p.IDCus == customer.IDCus);
                if (CusDB != null)
                {
                    CusDB.NameCus = customer.NameCus;
                    CusDB.PhoneCus = customer.PhoneCus;
                    CusDB.Address = customer.Address;
                    CusDB.Status = customer.Status;
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }
        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           Customer c = db.Customers.Find(id);
            if (c == null)
            {
                return HttpNotFound();
            }
            return View(c);
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer c = db.Customers.Find(id);
            if (c == null)
            {
                return HttpNotFound();
            }
            return View(c);
        }
        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer c = db.Customers.Find(id);
            db.Customers.Remove(c);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Customer cus)
        {
            try
            {
                db.Customers.Add(cus);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("LOI TAO MOI CUSTOMERS");
            }
        }
        
    }
}
