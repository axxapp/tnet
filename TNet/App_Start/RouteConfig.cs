﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using TNet.Service.Com;

namespace TNet
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{resource}.js.map/{*pathInfo}");
            //参与这种路由参数 兼容 WCF路由
            routes.MapRoute(
                 "User",
                 "User/",
                  new { controller = "User", action = "Index" },
                  new { controller = "^(?!Service).*" }
            );

            routes.MapRoute(
                 "Merc",
                 "Merc/List/{tag}",
                  new { controller = "Merc", action = "List", tag = UrlParameter.Optional },
                  new { controller = "^(?!Service).*" }
              );

            routes.MapRoute(
                 "Order_Pay",
                 "Order/Pay",
                  new { controller = "Order", action = "Pay", orderno = UrlParameter.Optional },
                  new { controller = "^(?!Service).*" }
              );


            routes.MapRoute(
                 "Order_Detail",
                 "Order/Detail/{orderno}",
                  new { controller = "Order", action = "Detail", orderno = UrlParameter.Optional },
                  new { controller = "^(?!Service).*" }
              );


            routes.MapRoute(
                "Buss_Detail",
                "Buss/Detail/{idbuss}",
                 new { controller = "Buss", action = "Detail", idbuss = UrlParameter.Optional },
                 new { controller = "^(?!Service).*" }
             );

            routes.MapRoute(
                 "Manage_MercEdit",
                 "Manage/MercEdit/{idmerc}",
                  new { controller = "Manage", action = "MercEdit", idmerc = UrlParameter.Optional },
                  new { controller = "^(?!Service).*" }
            );
            
            routes.MapRoute(
               "Manage_MercList",
               "Manage/MercList/{pageIndex}",
                new { controller = "Manage", action = "MercList", pageIndex = UrlParameter.Optional },
                new { controller = "^(?!Service).*" }
            );

            routes.MapRoute(
               "Manage",
               "Manage/{action}/{id}",
                new { controller = "Manage", action = "Login", id = UrlParameter.Optional },
                new { controller = "^(?!Service).*" }
           );

            routes.MapRoute(
               "Default",
               "{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new { controller = "^(?!Service).*" }
            );
            RouteService.register();
        }
    }
}
