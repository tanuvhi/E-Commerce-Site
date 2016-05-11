using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UltraShopBd.Domain.Abstract;
using UltraShopBd.Domain.UShopAbstract;
using UltraShopBd.Domain.UShopConcrete;
using UltraShopBd.Domain.UShopEntity;
using UltraShopBd.WebUI.Models;

namespace UltraShopBd.WebUI.Controllers
{
    public class UltraShopBDWFController : Controller
    {
        private readonly UltraProductRepository UPrepository;
        private readonly IProductRepository repository;
        private readonly IUProductRepository IUrepository;
        public UltraShopBDWFController(UltraProductRepository repo, IUProductRepository IUrepo, IProductRepository IPRepo)
        {
             UPrepository=repo;
             repository = IPRepo;
             IUrepository = IUrepo;


        }

       
        //
        // GET: /UltraShopBD/
        public ActionResult Index()
        {
            return View();
        }
        public ViewResult PurchaseCreate()
        {
            ViewBag.Color = new SelectList(UPrepository.colors, "color_id", "color_name");
            return View();
        }
        [HttpPost]
        public ActionResult PurchaseCreate(purchase pur)
        {
            stock sto = new stock();
            ViewBag.Color = new SelectList(UPrepository.colors, "color_id", "color_name");
            if(ModelState.IsValid)
            {
                sto.color_id = pur.color_id;
                sto.product_id = pur.product_id;
                sto.quantity = pur.quantity;
                sto.selling_price = pur.selling_price;
                sto.purchase_price = pur.purchase_price;
                sto.size_id = pur.size_id;
                pur.purchase_date = System.DateTime.Now.Date;
                
                UPrepository.UpdateStock(sto);
                IUrepository.SavePruchase(pur);
                TempData["message"] = string.Format("Added Successfully");
                return Redirect(Url.Action("PurchaseList", "UltraShopBDWF"));

            }
            return View();
        }
        
        [AllowAnonymous]
        public JsonResult PNameAutoComplite(string term)
        {

            var result = (from r in UPrepository.products
                           where r.product_name.ToLower().Contains(term.ToLower())
                           select new { r.product_name ,r.product_Id}).Distinct();
          
            return Json(result, JsonRequestBehavior.AllowGet);
        }
       
       

        public ViewResult StockList(UltraShopModel model )
        {
            model.Product = UPrepository.products.ToDictionary(row => row.product_Id,
                                row => row.product_name);
            model.Color = UPrepository.colors.ToDictionary(row => row.color_id,
                                row => row.color_name); 
            model.stock = UPrepository.stocks;
            
            return View(model);
        }
        public ViewResult PurchaseList(UltraShopModel model)
        {
            model.Purchase = UPrepository.purchases;
            model.Product = UPrepository.products.ToDictionary(row => row.product_Id,
                               row => row.product_name);
            model.ImageUrl = repository.Products.ToDictionary(r => r.ProductId, r => r.ImageUrl);
            model.Color = UPrepository.colors.ToDictionary(row => row.color_id,
                                row => row.color_name); 
          

            return View(model);
        }

        public JsonResult OExpAutoComplite(string term)
        {

            var result = (from r in UPrepository.otherexpenselists
                          where r.name.ToLower().Contains(term.ToLower())
                          select new { r.name, r.other_expense_list_id }).Distinct();

            return Json(result, JsonRequestBehavior.AllowGet);
        }
       
        public ViewResult OtherExpenseCreate()
        {

            return View();
        }
        [HttpPost]
        public ActionResult OtherExpenseCreate(otherexpense otherexp, string name)
        {
            if (ModelState.IsValid)
            {
                if (otherexp.other_expenses_list_id == 0)
                {
                    otherexpenselist oel = new otherexpenselist();
                    oel.name = name;
                    otherexp.other_expenses_list_id = UPrepository.AddOtherExpenseList(oel);
                }
                UPrepository.SaveOtherExpense(otherexp);
            }
            return View();
        }

        public ViewResult OtherExpenseList()
        {

            return View();
        }
	}
}