﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;
using System.ServiceModel.Activation;
using TNet.Service.Merc;
using TNet.Service.Addr;
using TNet.Service.Order;
using TNet.Service.Buss;
using TNet.Service.File;
using TNet.Service.Pay;
using TNet.Service.Task;
using TNet.Service.Notice;
using TNet.Service.City;
using TNet.Service.Issue;
using TNet.Service.User;
using TNet.Service.ToKey;
using TNet.Service.Ad;

namespace TNet.Service.Com
{
    public sealed class RouteService
    {
        public static void register()
        {
            RouteTable.Routes.Add(new ServiceRoute("Service/Merc/", new WebServiceHostFactory(), typeof(MercService)));
            // RouteTable.Routes.Add(new ServiceRoute("MercService.svc", new WebServiceHostFactory(), typeof(MercService)));

            RouteTable.Routes.Add(new ServiceRoute("Service/Addr/", new WebServiceHostFactory(), typeof(AddrService)));
            //RouteTable.Routes.Add(new ServiceRoute("AddrService.svc", new WebServiceHostFactory(), typeof(AddrService)));


            RouteTable.Routes.Add(new ServiceRoute("Service/Order/", new WebServiceHostFactory(), typeof(OrderService)));
            //RouteTable.Routes.Add(new ServiceRoute("OrderService.svc", new WebServiceHostFactory(), typeof(OrderService)));


            RouteTable.Routes.Add(new ServiceRoute("Service/Buss/", new WebServiceHostFactory(), typeof(BussService)));
            //RouteTable.Routes.Add(new ServiceRoute("BussService.svc", new WebServiceHostFactory(), typeof(BussService)));

            RouteTable.Routes.Add(new ServiceRoute("Service/File/", new WebServiceHostFactory(), typeof(FileService)));
            // RouteTable.Routes.Add(new ServiceRoute("FileService.svc", new WebServiceHostFactory(), typeof(FileService)));

            RouteTable.Routes.Add(new ServiceRoute("Service/Pay/", new WebServiceHostFactory(), typeof(PayService)));
            //RouteTable.Routes.Add(new ServiceRoute("PayService.svc", new WebServiceHostFactory(), typeof(FileService)));

            RouteTable.Routes.Add(new ServiceRoute("Service/Task/", new WebServiceHostFactory(), typeof(TaskService)));

            RouteTable.Routes.Add(new ServiceRoute("Service/Notice/", new WebServiceHostFactory(), typeof(NoticeService)));


            RouteTable.Routes.Add(new ServiceRoute("Service/City/", new WebServiceHostFactory(), typeof(CityService)));

            RouteTable.Routes.Add(new ServiceRoute("Service/Issue/", new WebServiceHostFactory(), typeof(IssueService)));

            RouteTable.Routes.Add(new ServiceRoute("Service/User/", new WebServiceHostFactory(), typeof(UserService)));

            RouteTable.Routes.Add(new ServiceRoute("Service/ToKey/", new WebServiceHostFactory(), typeof(ToKeyService)));

            RouteTable.Routes.Add(new ServiceRoute("Service/Ad/", new WebServiceHostFactory(), typeof(AdService)));




        }
    }
}
