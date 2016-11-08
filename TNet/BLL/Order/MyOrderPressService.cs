using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TCom.EF;

namespace TNet.BLL
{
    public class MyOrderPressService
    {
        public static MyOrderPress Add(MyOrderPress orderPress)
        {
            TN db = new TN();
            db.MyOrderPresses.Add(orderPress);
            db.SaveChanges();
            return orderPress;
        }
    }
}