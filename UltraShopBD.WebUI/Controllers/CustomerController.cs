using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UltraShopBd.Domain.Abstract;
using UltraShopBd.Domain.UShopEntity;
using Microsoft.AspNet.Identity;
using UltraShopBd.WebUI.Models;
namespace UltraShopBd.WebUI.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {

        private readonly IOrderRepository orderRepositoy;
        private readonly IUserRepository userRepositoy;
        private readonly IProductRepository productRepository;
        // GET: /Customer/

        public CustomerController(IOrderRepository repo ,IUserRepository urepo,IProductRepository proRepo)
        {
            orderRepositoy = repo;
            userRepositoy = urepo;
            productRepository = proRepo;
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Details(int orderId, CustomerViewModel csModel)
        {
            

            csModel.orderDetails = orderRepositoy.OrderDetails.Where(x => x.OrderId == orderId).OrderBy(x=>x.ProductId);
           csModel.product = productRepository.Products.Join(csModel.orderDetails, p => p.ProductId, O => O.ProductId, (p, O) => new { p, O })
                .Select(s=>s.p)
                .OrderBy(s=>s.ProductId)
                .ToList();
            
          
            return View(csModel);
        }

        public ActionResult Purchase(CustomerViewModel  csModel )
        {
            
         csModel.order  =  orderRepositoy.Orders
                .Where(Or => Or.CustomerId == Convert.ToInt32(Session["MyId"]));
         csModel.orderStatus = orderRepositoy.OrderStatus.ToList(); 
            return View(csModel);
        }
	}
}