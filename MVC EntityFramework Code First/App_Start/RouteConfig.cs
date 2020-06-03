using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MVC_EntityFramework_Code_First
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Account", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "getdata_dubao",
                url: "{DuLieuDuDoan}/{GetData}",
                defaults: new { controller = "DuLieuDuDoan", action = "GetData" }
            );
            routes.MapRoute(
                name: "getdata_lichsu",
                url: "{DuLieuLichSu}/{GetData}",
                defaults: new { controller = "DuLieuLichSu", action = "GetData" }
            );
            routes.MapRoute(
                name: "getdata_lichsu_yesterday",
                url: "{DuLieuLichSu}/{GetYesterdayCapacity}",
                defaults: new { controller = "DuLieuLichSu", action = "GetYesterdayCapacity" }
            );
            routes.MapRoute(
                name: "getdata_sosanh",
                url: "{DuLieuDuDoan}/{GetDataSoSanh}",
                defaults: new { controller = "DuLieuDuDoan", action = "GetDataSoSanh" }
            );
        }
    }
}
