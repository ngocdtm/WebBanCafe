using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebsiteBanCaPhe.Models;

namespace WebsiteBanCaPhe.Controllers
{
    public class CustomerProductsController : Controller
    {
        private WebsiteCoffeeEntities1 db = new WebsiteCoffeeEntities1();

        // GET: CustomerProducts
     
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new
               HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
        public ActionResult GetProductsByCategory()
        {
            var categories = db.Categories.ToList();
            return PartialView("CategoriesPartialView", categories);
        }
        public ActionResult GetProductsByCateId(int id)
        {
            var products = db.Products.Where(p => p.Category1.Id ==
           id).ToList();
            return View("Index", products);
        }
     
        // GET: CustomerProducts
        public ActionResult Index(string searchString, string sortOrder)
        {
            var product = db.Products.Include(p => p.Category1);
            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToUpper();
                product = product.Where(p => p.NamePro.ToUpper().Contains(searchString));
                if (product == null)
                {
                    ViewBag.searchResult = "Không có sản phẩm này vui lòng tìm sản phẩm khác !";
                }
            }
            switch (sortOrder)
            {
                case "price_asc":
                    product = product.OrderBy(p => p.Price);
                    break;
                case "price_desc":
                    product = product.OrderByDescending(p => p.Price);
                    break;
                default:
                    product = product.OrderBy(p => p.NamePro);
                    break;
            }
            return View(product.ToList());
        }
        public ActionResult CoffeePartial()
        {
            var pro = db.Products.Where(n => n.Category1.NameCate == "Cà phê").Take(10).ToList();
            foreach (var item in pro)
            {
                string values = item.Price.ToString();
                System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("vn-VN");
                //decimal value = decimal.Parse(values, System.Globalization.NumberStyles.AllowThousands);
                //ViewBag.price = string.Format(culture, "{0:N0}", values); ;
            }
            return PartialView("CoffeePartial", pro);
        }
        public ActionResult TeaPartial()
        {
            var pro = db.Products.Where(n => n.Category1.NameCate == "Trà").Take(10).ToList();
            foreach (var item in pro)
            {
                string values = item.Price.ToString();
                System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-US");
                decimal value = decimal.Parse(values, System.Globalization.NumberStyles.AllowThousands);
                ViewBag.price = string.Format(culture, "{0:N0}", value); ;
            }
            return PartialView(pro);
        }
        public ActionResult CakePartial()
        {
            var pro = db.Products.Where(n => n.Category1.NameCate == "Bánh").Take(10).ToList();
            foreach (var item in pro)
            {
                string values = item.Price.ToString();
                System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-US");
                decimal value = decimal.Parse(values, System.Globalization.NumberStyles.AllowThousands);
                ViewBag.price = string.Format(culture, "{0:N0}", value); ;
            }
            return PartialView(pro);
        }
    }
}
