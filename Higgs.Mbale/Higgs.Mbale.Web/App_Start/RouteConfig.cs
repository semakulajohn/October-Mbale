﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Higgs.Mbale.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "supplier",
                url: "{Excel}/{SupplierSupply}/{reportTypeId}/{supplierId}",
                defaults: new
                {
                    controller = "Excel",
                    action = "SupplierSupply",
                    reportTypeId = UrlParameter.Optional,
                    supplierId = UrlParameter.Optional
                });
        }
    }
}
