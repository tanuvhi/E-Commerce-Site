using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace UltraShopBd.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "home", action = "index", id = UrlParameter.Optional }
            );
            
            routes.MapRoute(
                name: "GetCityByRegionId",
                url: "Account/GetCityByRegionId/",
                defaults: new { controller = "Account", action = "GetCityByRegionId" }

                           );
            routes.MapRoute(
                name: "GetSubCategoryByCategoryId",
                url: "Product/GetSubCategoryByCategoryId/",
                defaults: new { controller = "Product", action = "GetSubCategoryByCategoryId" }

                           );

            
                   routes.MapRoute(
                name: "CustomerDetails",
                url: "Admin/CustomerDetails/",
                defaults: new { controller = "Admin", action = "CustomerDetails" }

                           );
            routes.MapRoute(
                name: null,
                url: "Page{page}",
                defaults: new { Controller="Product",Action="List"}
                );

            routes.MapRoute(null, "",
                new
                {
                    controller = "Product",
                    action = "List",
                    CategoryId = (string)null,
                     SubCategoryId= (string)null,
                    page = 1
                });

            routes.MapRoute(null, "{CategoryId}",
                new
                {
                    controller = "Product",
                    action = "List",
                   
                    page = 1
                });

            routes.MapRoute(null, "Page{page}",
                new
                {
                    controller = "Product",
                    action = "List",
                    CategoryId = (string)null,
                    SubCategoryId= (string)null
                },
                new { page=@"\d+"});

            routes.MapRoute(null, "{CategoryId}/{SubCategoryId}/Page{page}",
             new
             {
                 controller = "Product",
                 action = "List",
                 
             },
             new { page = @"\d+" });

               routes.MapRoute(
                name: "CheckName",
                url: "Product/CheckName/",
                defaults: new { controller = "Product", action = "CheckName" }

                           );
               routes.MapRoute("editProd", "product/Edit/{id}",
              new { controller = "Product", action = "Edit", id = 0 });
            
        }
    }
}
