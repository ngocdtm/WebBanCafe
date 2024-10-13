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
    public class CategoriesController : Controller
    {
        private WebsiteCoffeeEntities1 db = new WebsiteCoffeeEntities1();
        public ActionResult Index()
        {
            var categories = db.Categories.ToList();
            return View(categories);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Category category)
        {
            try
            {
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("LOI TAO MOI CATEGORY");
            }
        }
        public ActionResult Details(int id)
        {
            var category = db.Categories.Where(c => c.Id == id).FirstOrDefault();
            return View(category);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var category = db.Categories.Where(c => c.Id == id).FirstOrDefault();
            return View(category);
        }
        [HttpPost]
        public ActionResult Edit(int id, Category category)
        {
            db.Entry(category).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");

        }
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var category = db.Categories.Where(c => c.Id == id).FirstOrDefault();
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var category = db.Categories.Where(c => c.Id == id).FirstOrDefault();
                db.Categories.Remove(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("không xóa được do có liên quan đến bảng khác");
            }
        }

    }
}
