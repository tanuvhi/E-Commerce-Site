using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UltraShopBd.Domain.Abstract;
using UltraShopBd.WebUI.Models;

namespace UltraShopBd.WebUI.Controllers
{
    public class NavController : Controller
    {
        //
        // GET: /Nav/
        private IProductRepository repository;

        public NavController(IProductRepository repo)
        {
            repository = repo;
        }


        public PartialViewResult Menu(NavViewModel model, int CategoryId = 0, int SubCategoryId=0)
        {
            ViewBag.SelectedCategory = CategoryId;
            ViewBag.SelectedSubCategory = SubCategoryId;
            model.Categories = repository.Categories.ToList();
            model.SubCategories = repository.SubCate.ToList();
          
            return PartialView(model);
        }

        public PartialViewResult Menu2(NavViewModel model, int CategoryId = 0)
        {
           
            model.Categories = repository.Categories.ToList();
         
            return PartialView(model);
        }
	}
}