using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UltraShopBd.Domain.Abstract;
using UltraShopBd.WebUI.Models;

namespace UltraShopBd.WebUI.Controllers
{
    public class HomeController : Controller
    {
       private readonly  IProductRepository repository ;

        public HomeController(IProductRepository repo)
        {

            repository = repo;
        }


        int PageSize = 200;

        public ViewResult ProductList()
        {
            ProductListViewModel model = new ProductListViewModel
            {
                Products = repository.Products
                .Where(p => p.Activity == 1)
                .OrderByDescending(p => p.CategoryId)
                .OrderByDescending(p=>p.ProductId)
           
                 .Take(PageSize)
                
               

            };
            return View(model);
        }






        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Test()
        {
            return View();
        }


     
    }
}