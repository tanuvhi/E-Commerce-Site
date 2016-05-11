using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UltraShopBd.Domain.Entities;
using UltraShopBd.Domain.UShopEntity;

namespace UltraShopBd.WebUI.Models
{
    public class AdminViewModel
    {
        public List<Order> order { get; set; }

        public List<OrderStatus> orderStatus { get; set; }

        public IEnumerable<OrderDetail> orderDetails { get; set; }

        public List<Product> product { get; set; }

        public List<User> user { get; set; }

        public List<City> city { get; set; }
    }
}