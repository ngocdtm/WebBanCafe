using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebsiteBanCaPhe.Models;

namespace WebsiteBanCaPhe.Controllers
{
    public class OrderProController : Controller
    {
        private WebsiteCoffeeEntities1 db = new WebsiteCoffeeEntities1();

        // GET: OrderPro
        public ActionResult Index()
        {
            Customer khach = Session["customer"] as Customer;
            var orderProes = db.OrderProes.Where(c => c.IDCus == khach.IDCus);




            return View(orderProes.ToList());
        }

        // GET: OrderPro/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderPro orderPro = db.OrderProes.Find(id);
            if (orderPro == null)
            {
                return HttpNotFound();
            }
            return View(orderPro);
        }
        // GET: Admin/OrderProes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderPro orderPro = db.OrderProes.Find(id);
            if (orderPro == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDCus = new SelectList(db.Customers, "IDCus", "NameCus", "PhoneCus", orderPro.IDCus);
            return View(orderPro);
        }

        // POST: Admin/OrderProes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,DateOrder,IDCus,AddressDeliverry,IDVoucher,IDPay,TotalPrice,Status")] OrderPro orderPro)
        {
            if (ModelState.IsValid)
            {
                var orderDB = db.OrderProes.FirstOrDefault(p => p.ID == orderPro.ID);
                if (orderDB != null)
                {

                    orderDB.Status = orderPro.Status;
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDCus = new SelectList(db.Customers, "IDCus", "NameCus", "PhoneCus", orderPro.IDCus);
            return View(orderPro);
        }




    }
}
