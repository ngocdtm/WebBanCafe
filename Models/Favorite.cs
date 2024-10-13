using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebsiteBanCaPhe.Models
{
    public class Favorite
    {
        WebsiteCoffeeEntities1 db = new WebsiteCoffeeEntities1();
        public int ProductID { get; set; }
        public string NamePro { get; set; }
        public string ImagePro { get; set; }
        public decimal Price { get; set; }
        public int Number { get; set; }

        public int Count{ get; set; }
        public Favorite(int ProductID)
        {
            this.ProductID = ProductID;

            var productDB = db.Products.Single(s => s.ProductID ==
           this.ProductID);
            this.NamePro = productDB.NamePro;
            this.ImagePro = productDB.ImagePro;
            this.Price = (decimal)productDB.Price;
      
            this.Number = 1;
            this.Count = 1;

        }
    }
}
