using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UltraShopBd.Domain.Entities;
using UltraShopBd.Domain.UShopEntity;

namespace UltraShopBd.WebUI.BInders
{
    public class CartModelBinder : IModelBinder
    {
        private const string sessionkey = "Cart";
    
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            Cart cart = null;
            if(controllerContext.HttpContext.Session !=null)
            {
                cart = (Cart)controllerContext.HttpContext.Session[sessionkey];

            }
            if(cart== null)
            {
                cart = new Cart();
                if (controllerContext.HttpContext.Session != null)
                    controllerContext.HttpContext.Session[sessionkey] = cart;
            }
            return cart;
        }
}
}