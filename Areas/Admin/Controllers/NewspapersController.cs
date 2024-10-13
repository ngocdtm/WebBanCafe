using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebsiteBanCaPhe.Models;

namespace WebsiteBanCaPhe.Areas.Admin.Controllers
{
    public class NewspapersController : Controller
    {
        private WebsiteCoffeeEntities1 db = new WebsiteCoffeeEntities1();

        // GET: Admin/Newspapers
        public ActionResult Index(string orderSort)
        {
            var s = db.Newspapers.OrderByDescending(p => p.date);
          
        
            return View(s.ToList());
        }

        // GET: Admin/Newspapers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Newspaper newspaper = db.Newspapers.Find(id);
            newspaper.date = DateTime.Now;
            if (newspaper == null)
            {
                return HttpNotFound();
            }
            return View(newspaper);
        }

        // GET: Admin/Newspapers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Newspapers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDNew,Title,date,Content,Image,des,detail")] Newspaper newspaper, HttpPostedFileBase Image)
        {
          
            if (ModelState.IsValid)
            {
                if (Image != null)
                {
                    //Lấy tên file của hình được up lên

                    var fileName = Path.GetFileName(Image.FileName);

                    //Tạo đường dẫn tới file

                    var path = Path.Combine(Server.MapPath("~/Image"), fileName);
                    //Lưu tên

                    newspaper.Image = fileName;
                    //Save vào Images Folder
                    Image.SaveAs(path);

                }
                db.Newspapers.Add(newspaper);
                db.SaveChanges();
                return RedirectToAction("Index");

            }

            return View(newspaper);
        }

        // GET: Admin/Newspapers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Newspaper newspaper = db.Newspapers.Find(id);
            if (newspaper == null)
            {
                return HttpNotFound();
            }
            return View(newspaper);
        }

        // POST: Admin/Newspapers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDNew,Title,Content,Image,date,des,detail")] Newspaper newspaper, HttpPostedFileBase Image)
        {

            if (ModelState.IsValid)
            {
                var newspaperDB = db.Newspapers.FirstOrDefault(p => p.IDNew == newspaper.IDNew);
                if (newspaperDB != null)
                {
                    newspaperDB.Title = newspaper.Title;
                    newspaperDB.Content = newspaper.Content;
                    newspaperDB.date = newspaper.date;
                    newspaperDB.detail = newspaper.detail;
                    newspaperDB.des = newspaper.des;
                    if (Image != null)
                    {
                        //lấy tên file của hình được up lên
                        var fileName = Path.GetFileName(Image.FileName);
                        //tạo đường dẫn tới file
                        var path = Path.Combine(Server.MapPath("~/Image"), fileName);
                        //lưu tên
                        newspaperDB.Image = fileName;
                        //save vào Images folder
                        Image.SaveAs(path);
                    }
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(newspaper);
        }

        // GET: Admin/Newspapers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Newspaper newspaper = db.Newspapers.Find(id);
            if (newspaper == null)
            {
                return HttpNotFound();
            }
            return View(newspaper);
        }

        // POST: Admin/Newspapers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Newspaper newspaper = db.Newspapers.Find(id);
            db.Newspapers.Remove(newspaper);
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
