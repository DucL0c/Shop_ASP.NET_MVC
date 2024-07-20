﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebBanHangOnline
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Product",
                url: "san-pham",
                defaults: new { controller = "Product", action = "Index", alias = UrlParameter.Optional },
                namespaces: new[] { "WebBanHangOnline.Controllers" }
            );

            routes.MapRoute(
                name: "GioHang",
                url: "gio-hang",
                defaults: new { controller = "ShoppingCart", action = "Index", alias = UrlParameter.Optional },
                namespaces: new[] { "WebBanHangOnline.Controllers" }
            );
            routes.MapRoute(
                name: "ThanhToan",
                url: "thanh-toan",
                defaults: new { controller = "ShoppingCart", action = "CheckOut", alias = UrlParameter.Optional },
                namespaces: new[] { "WebBanHangOnline.Controllers" }
            );

            routes.MapRoute(
                name: "CategoryProduct",
                url: "danh-muc-san-pham/{alias}-{id}",
                defaults: new { controller = "Product", action = "CategoryProduct", id = UrlParameter.Optional },
                namespaces: new[] { "WebBanHangOnline.Controllers" }
            );

            routes.MapRoute(
                name: "Detail",
                url: "chi-tiet/{alias}-{id}",
                defaults: new { controller = "Product", action = "Detail", id = UrlParameter.Optional },
                namespaces: new[] { "WebBanHangOnline.Controllers" }
            );

            routes.MapRoute(
                name: "Contact",
                url: "lien-he",
                defaults: new { controller = "Contact", action = "Index", alias = UrlParameter.Optional },
                namespaces: new[] { "WebBanHangOnline.Controllers" }
            );
            routes.MapRoute(
                name: "New",
                url: "tin-tuc",
                defaults: new { controller = "News", action = "Index", alias = UrlParameter.Optional },
                namespaces: new[] { "WebBanHangOnline.Controllers" }
            );
            routes.MapRoute(
                name: "DetailNew",
                url: "{alias}-n{id}",
                defaults: new { controller = "News", action = "Detail", id = UrlParameter.Optional },
                namespaces: new[] { "WebBanHangOnline.Controllers" });


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "WebBanHangOnline.Controllers" }
            );
        }
    }
}
