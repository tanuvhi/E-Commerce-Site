using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UltraShopBd.Domain.Abstract;
using UltraShopBd.Domain.Entities;

using UltraShopBd.WebUI.Models;

namespace UltraShopBd.WebUI.Controllers
{
    public class CartController : Controller
    {
       private readonly  IProductRepository repository;

        private readonly IOrderRepository orderRepository;

        private readonly IUserRepository userRepository;
       
        public CartController(IOrderRepository orde, IProductRepository repo, IUserRepository uRepo)
        {
            userRepository = uRepo;
            orderRepository = orde;
            repository = repo;
        }
        public ViewResult Index( Cart cart, string returnUrl)
        {
            return View(
                new CartIndexViewModel { Cart = cart, ReturnUrl = returnUrl });
        }
        public  PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);

        }
        [Authorize]
        public ViewResult Checkout()
        {
            User user = userRepository.Users.FirstOrDefault(u => u.Email == User.Identity.Name.ToString());
            return View(user);
            
        }
   

        [HttpPost]
        public ActionResult Checkout(Cart cart)
        {
            if(ModelState.IsValid)
            {
               
                orderRepository.OrderProcess(cart, User.Identity.Name.ToString());
                TempData["message"] = string.Format("Your order has been confirmed");
                cart.Clear();
                return RedirectToAction("Index","home");
              
            }

            return View();

        }
        public RedirectToRouteResult AddToCart(Cart cart, int? productId , string returnUrl,int quantity )
        {
         
            Product product = repository.Products.FirstOrDefault(p => p.ProductId == productId);

            if (product != null)
            {
                cart.AddItem(product, quantity);

            }
            else
                return RedirectToAction("list", new { returnUrl });
               
            return RedirectToAction("Index",new{returnUrl});
        }
        public RedirectToRouteResult RemoveFromCart(Cart cart,int productId,string returnUrl)
        {
            Product product = repository.Products.FirstOrDefault(p => p.ProductId == productId);
                if(product!=null)
                {
                   cart.RemoveLine(product);

                }
             return RedirectToAction("Index",new{returnUrl});
        }
       
	}
}