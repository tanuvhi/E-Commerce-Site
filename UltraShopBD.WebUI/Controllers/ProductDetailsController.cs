using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UltraShopBd.Domain.Abstract;
using UltraShopBd.Domain.UShopEntity;
using UltraShopBd.WebUI.Models;

namespace UltraShopBd.WebUI.Controllers
{
    public class ProductDetailsController : Controller
    {
        //
        // GET: /ProductDetails/
        
        private readonly IProductRepository repository;


        public ProductDetailsController(IProductRepository repo)
        {

            repository = repo;
        }
        public ViewResult Details(int productIdu)
        {
           // Console.WriteLine(productId);
          
            ProductDetailsViewModel model = new ProductDetailsViewModel();

            model.product = repository.Products.FirstOrDefault(x => x.ProductId == productIdu);

            DirectoryInfo directory = new DirectoryInfo(Server.MapPath(@"~/Content/Image/"+"p"+model.product.ProductId));

          
           model.files = directory.GetFiles();
           ViewBag.FileCount = model.files.Length;
            
            
            return View(model);
        }
	}
}