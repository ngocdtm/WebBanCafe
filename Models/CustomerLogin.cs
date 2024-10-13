using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebsiteBanCaPhe.Models;
namespace WebsiteBanCaPhe.Models
{
    public partial class Customer
    {
        WebsiteCoffeeEntities1 db = new WebsiteCoffeeEntities1();

        public int Login(string cusName, string passCus)
        {

            var re = db.Customers.SingleOrDefault(x => x.NameCus == cusName);
            if (re == null)//ko tài khoản
            {
                return 0;
            }
            else if (re.Status == true)
            {


                if (re.PassCus == passCus)
                { return -1; }
                else { return 1; }

            }
            //khóa
            else return -2;


        }
        //public int St(string Status)
        //{
        //    Customer khach = Session["customer"] as Customer;
        //    var orderProes = db.OrderProes.Where(c => c.IDCus == khach.IDCus);
        //    var re=db.OrderProes.SingleOrDefault()
        //}
    }
}