using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;
using WebsiteBanCaPhe.Models;

namespace WebsiteBanCaPhe.Controllers
{
    public class FavouritesController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
       [HttpPost]
        public ActionResult Wishlist(int proID) {
            Customer khach = Session["Customer"] as Customer;
            var item = new Favourite();
            item.IDPro = proID;
            item.CusID =  khach.IDCus;
            db.Favourites.Add(item);
            db.SaveChanges();
            return Json(new { Success = true });
        }
        //public ActionResult AddSongInLibrary(int ID_Song, int ID_AdminUser)
        //{
        //    // Kiểm tra xem bài hát đã tồn tại trong thư viện chưa
        //    var songExist = db.CheckSongExist(ID_Song, ID_AdminUser);

        //    // Nếu bài hát chưa tồn tại trong thư viện
        //    if (songExist.Count == 0)
        //    {
        //        // Thêm bài hát vào thư viện
        //        db.Favourites(ID_Song, ID_AdminUser);

        //        // Trả về thông báo thành công
        //        return Json(new { success = true, message = "Cập nhật thành công" });
        //    }

        //    // Nếu bài hát đã tồn tại trong thư viện
        //    else
        //    {
        //        // Xóa bài hát khỏi thư viện
        //        cnn.DeleteSongInLibrary(songExist[0].ID_Library);

        //        // Thêm bài hát vào thư viện
        //        cnn.AddLibrary(ID_Song, ID_AdminUser);

        //        // Trả về thông báo thành công
        //        return Json(new { success = true, message = "Cập nhật thành công" });
        //    }
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        //[HttpPost]
        //public ActionResult Favorite(int ID_pro, int ID_cus)
        //{
        //    var checkproExist = db.checkproExist(ID_pro, ID_cus);
        //    if (checkproExist.Count == 0)
        //    {
        //    }
        //}
        //public List<Favorite> GetFavorite()
        //{
        //    List<Favorite> myFavorite = Session["Favorite"] as List<Favorite>;
        //    //Nếu giỏ Favorite chưa tồn tại thì tạo mới và đưa vào Session
        //    if (myFavorite == null)
        //    {
        //        myFavorite = new List<Favorite>();
        //        Session["Favorite"] = myFavorite;
        //    }
        //    return myFavorite;

        //}
        private WebsiteCoffeeEntities1 db = new WebsiteCoffeeEntities1();
        ////Hàm thêm một sản phẩm vào giỏ
        //public ActionResult AddToFavorite(int id)
        //{
        //    //Lấy giỏ hàng hiện tại
        //    List<Favorite> myFavorite = GetFavorite();
        //    var produc = db.Products.FirstOrDefault(x => x.ProductID == id);



        //    Favorite currentProduct = myFavorite.FirstOrDefault(p => p.ProductID == id);
        //    if (currentProduct == null)
        //    {
        //        currentProduct = new Favorite(id);
        //        myFavorite.Add(currentProduct);           
        //    }
        //    else
        //    {
        //        currentProduct.Number++; //Sản phẩm đã có trong giỏ thì  tăng số lượng lên 1
        //    }


        //    return RedirectToAction("Index", "CustomerProducts", new
        //    {
        //        id = id
        //    });

        //}

        //public ActionResult Index()
        //{
        //    Customer khach = Session["customer"] as Customer;
        //    var orderProes = db.Favourites.Where(c => c.CusID == khach.IDCus);
        //    return View(orderProes.ToList());
        //}


        ////Tính tổng số lượng mặt hàng được mua
        //private int GetTotalNumber()
        //{
        //    int totalNumber = 0;
        //    List<Favorite> myFavorite = GetFavorite();
        //    if (myFavorite != null)
        //        totalNumber = myFavorite.Sum(sp => sp.Number);
        //    return totalNumber;
        //}

        ////Xây dựng hàm hiển thị thông tin bên trong giỏ hàng
        //public ActionResult GetFavoriteInfo()
        //{

        //    Customer khach = Session["Customer"] as Customer; //Khách 
        //    List<Favorite> myFavorite = GetFavorite();//Giỏ Favorite
        //    Favourite f = new Favourite(); //Tạo mới đơn đặt hàng
        //    f.CusID = khach.IDCus;
        //    f.IDPro = myFavorite.;
        //    db.Favourites.Add(f);
        //    db.SaveChanges();
        //    //Lần lượt thêm từng chi tiết cho đơn hàng trên

        //    foreach (var product in myFavorite)
        //    {
        //        Favourite chitiet = new Favourite();
        //        chitiet.IDPro = product.ProductID;
        //        db.Favourites.Add(chitiet);
        //    }
        //    db.SaveChanges();
        //    return RedirectToAction("Index", "CustomerProducts");
        //}
        //public ActionResult FavoritePartial()
        //{
        //    ViewBag.TotalNumber = GetTotalNumber();
        //    return PartialView();
        //}
        //public ActionResult DeleteFavorite(int id)
        //{
        //    List<Favorite> myCart = GetFavorite();
        //    //Lấy sản phẩm trong giỏ hàng
        //    var currentProduct = myCart.FirstOrDefault(p => p.ProductID == id);
        //    if (currentProduct != null)
        //    {
        //        myCart.RemoveAll(p => p.ProductID == id);
        //        return RedirectToAction("GetCartInfo"); //Quay về trang giỏ hàng
        //    }
        //    if (myCart.Count == 0) //Quay về trang chủ nếu giỏ hàng không có gì
        //        return RedirectToAction("Index", "CustomerProducts");
        //    return RedirectToAction("GetCartInfo"); //Quay về trang giỏ hàng
        //}





    }
}

