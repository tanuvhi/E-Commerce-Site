using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltraShopBd.Domain.Entities;

namespace UltraShopBd.Domain.Abstract
{
  public  interface IOrderRepository
  {
      IEnumerable<Order> Orders { get; }
      IEnumerable<OrderDetail> OrderDetails { get; }

      IEnumerable<OrderStatus> OrderStatus { get; }

      bool OrderProcess(Cart cart, string email);
    }
}
