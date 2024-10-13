using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using WebsiteBanCaPhe.Models;

namespace WebsiteBanCaPhe.Controllers
{
    public class CustomersController : Controller
    {
        private WebsiteCoffeeEntities1 db = new WebsiteCoffeeEntities1();

        // GET: Users
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Customer cust)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(cust.NameCus))
                    ModelState.AddModelError(string.Empty, "Tên đăng nhập không được để trống");
                if (string.IsNullOrEmpty(cust.PhoneCus))
                    ModelState.AddModelError(string.Empty, "Điện thoại không được để trống");
                if (string.IsNullOrEmpty(cust.PassCus))
                    ModelState.AddModelError(string.Empty, "Password không  được để trống");
                if (string.IsNullOrEmpty(cust.Address))
                    ModelState.AddModelError(string.Empty, "Địa chỉ  không được để trống");

                //Kiểm tra xem có người nào đã đăng kí với tên đăng nhập  này hay chưa
                var khachhang = db.Customers.FirstOrDefault(k =>
                k.NameCus == cust.NameCus);
                if (khachhang != null)
                    ModelState.AddModelError(string.Empty, "Đã có người  đăng kí tên này");
                if (ModelState.IsValid)
                {
                    db.Customers.Add(cust);
                   db.SaveChanges();

                }
                else
                {
                    return View();
                }
            }
            return RedirectToAction("Login");
        }
       
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Customer cust)
        {
            if (ModelState.IsValid)
            {

                var dao = new Customer();
                var re = dao.Login(cust.NameCus, cust.PassCus);
                if (string.IsNullOrEmpty(cust.NameCus))
                    ModelState.AddModelError(string.Empty, "Tên đăng nhập không được để trống");
                if (string.IsNullOrEmpty(cust.PassCus))
                    ModelState.AddModelError(string.Empty, "Mật khẩu không được để trống");


                if (ModelState.IsValid)
                    {
                  
                    if (re == 0)
                    {
                        ModelState.AddModelError("", "Ko co tai khoan");
                    }
                    else
                    {
                        if (re == 1)
                        {
                            //tìm khách hàng có tên đăng nhập và password hợp lệ trong CSDL 
                            var khachhang = db.Customers.FirstOrDefault(k => k.NameCus == cust.NameCus && k.PassCus == cust.PassCus);
                            if (khachhang != null)
                            {
                                ViewBag.ThongBao = "Chúc mừng đăng nhập thành công";
                                //lưu vào sesion
                                Session["Customer"] = khachhang;
                                Session["IDCus"]=khachhang.IDCus.ToString();
                                return RedirectToAction("Index", "Home");
                            }
                        }
                        else if(re==-1){ ModelState.AddModelError("", "Mật khẩu không đúng"); }
                     }
                     if (re == -2) { ModelState.AddModelError("", " bi khoa"); }


                }

            }
            

            return View();
        }
        public ActionResult Logout(Customer cust)
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");

        }
        public ActionResult Index()
        {
            Customer khach = Session["Customer"] as Customer;
            var customer= db.Customers.Where(c=>c.IDCus==khach.IDCus);
            return View(customer);
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
        public ActionResult Edit([Bind(Include = "IDCus,NameCus,PhoneCus,Address,PassCus,Status,DisplayName,Gender")] Customer customer, HttpPostedFileBase ImageCus)
        {
            if (ModelState.IsValid)
            {
                var CusDB = db.Customers.FirstOrDefault(p => p.IDCus == customer.IDCus);
                if (CusDB != null)
                {
                    CusDB.DisplayName = customer.DisplayName;
                    CusDB.PhoneCus = customer.PhoneCus;
                    CusDB.Gender = customer.Gender;
                    CusDB.Address = customer.Address;      
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }
    }
}
     
