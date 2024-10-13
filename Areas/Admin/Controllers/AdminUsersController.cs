using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebsiteBanCaPhe.Models;

namespace WebsiteBanCaPhe.Areas.Admin.Controllers
{
    public class AdminUsersController : Controller
    {
        private WebsiteCoffeeEntities1 db = new WebsiteCoffeeEntities1();

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(AdminUser ad)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(ad.NameUser))
                    ModelState.AddModelError(string.Empty, "Tên đăng nhập không được để trống");
                if (string.IsNullOrEmpty(ad.PasswordUser))
                    ModelState.AddModelError(string.Empty, "Mật khẩu không được để trống");
                if (ModelState.IsValid)
                {
                    //tìm khách hàng có tên đăng nhập và password hợp lệ trong CSDL 
                    var khachhang =db.AdminUsers.FirstOrDefault(k => k.NameUser == ad.NameUser && k.PasswordUser == ad.PasswordUser);
                    if (khachhang != null)
                    {
                        ViewBag.ThongBao = "Chúc mừng đăng nhập thành công";
                        //lưu vào sesion
                        Session["TaiKhoan"] = khachhang;
                        return RedirectToAction("Index", "AdminHome");
                    }
                    else
                        ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng";
                }
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }

        // GET: Admin/
        public ActionResult Index()
        {
            return View(db.AdminUsers.ToList());
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdminUser a = db.AdminUsers.Find(id);
            if (a == null)
            {
                return HttpNotFound();
            }
            return View(a);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NameUser,PasswordUser,ImageAd")] AdminUser a)
        {
            if (ModelState.IsValid)
            {
                var AdDB = db.AdminUsers.FirstOrDefault(p => p.ID == a.ID);
                if (AdDB != null)
                {
                    AdDB.NameUser = a.NameUser;
                    AdDB.PasswordUser = a.PasswordUser;
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(a);
        }
        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdminUser c = db.AdminUsers.Find(id);
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
            AdminUser c = db.AdminUsers.Find(id);
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
            AdminUser c = db.AdminUsers.Find(id);
            db.AdminUsers.Remove(c);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(AdminUser a)
        {
            try
            {
                db.AdminUsers.Add(a);
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
