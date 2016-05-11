using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Management;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using UltraShopBd.Domain.Abstract;
using UltraShopBd.Domain.Concrete;
using UltraShopBd.Domain.Entities;
using UltraShopBd.Domain.UShopConcrete;
using UltraShopBd.Domain.UShopEntity;
using UltraShopBd.WebUI.BInders;
using WebApplication6;

namespace UltraShopBd.WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            
            ModelBinders.Binders.Add(typeof(Cart) ,new CartModelBinder());
            Database.SetInitializer<EFDbContext>(null);
            Database.SetInitializer<UltraShopEFContex>(null);
           
        }
    
    


       

    }
}
