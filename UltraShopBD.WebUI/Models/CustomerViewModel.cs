using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UltraShopBd.Domain.Entities;


namespace UltraShopBd.WebUI.Models
{
    public class CustomerViewModel
    {
      public   IEnumerable<Order> order { get; set; }

      public  List<OrderStatus> orderStatus { get; set;  }

    public   IEnumerable<OrderDetail> orderDetails { get; set; }

    public List<Product> product { get; set; }    
    }
}