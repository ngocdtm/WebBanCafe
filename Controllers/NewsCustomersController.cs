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
    public class NewsCustomersController : Controller
    {
        private WebsiteCoffeeEntities1 db = new WebsiteCoffeeEntities1();

        // GET: NewsCustomers
        public ActionResult Index()
        {

            var s = db.Newspapers.OrderByDescending(p => p.date);
            return View(s.ToList());
        }

        // GET: NewsCustomers/Details/5
        public ActionResult Details(int? id)
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

      
    }
}
