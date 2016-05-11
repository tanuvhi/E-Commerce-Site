using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltraShopBd.Domain.Abstract;
using UltraShopBd.Domain.Entities;

namespace UltraShopBd.Domain.Concrete
{
    public class EFOrderRepository : IOrderRepository
    {
        private readonly EFDbContext context = new EFDbContext();

        public IEnumerable<Order> Orders
        {
            get { return context.Orders; }
        }

       public IEnumerable<OrderDetail>OrderDetails
        {
            get { return context.OrderDetails; }
        }

       public IEnumerable<OrderStatus> OrderStatus
       {
           get { return context.OrderStatus_; }
       }
      public bool OrderProcess(Cart cart, string emial)
       {
          User useer = context.Users.FirstOrDefault(u => u.Email == emial);
          
          OrderDetail orderDetails= new OrderDetail();
          Order order = new Order();
          Order ordeeer = context.Orders.Where(a => a.CustomerId == useer.UserId).OrderBy(p => p.OrderId).SingleOrDefault();
         if (ordeeer ==null || order.OrderStatusId==2)
         {
             order.CustomerId = useer.UserId;
             order.OrderStatusId = 1;
             order.ProductPrice = cart.ComputeTotalValue();

             context.Orders.Add(order);
             context.SaveChanges();
             order = context.Orders.Where(a => a.CustomerId == useer.UserId).OrderByDescending(p => p.OrderId).SingleOrDefault();
         }
          else
         {
            order.OrderId=  ordeeer.OrderId;
         }
         foreach (var line in cart.Lines)
         {
             orderDetails.OrderId = order.OrderId;
             orderDetails.ProductId = line.Products.ProductId;
             orderDetails.Quantity = line.Quantity;
             orderDetails.Total =Convert.ToDecimal (line.Quantity * line.Products.Price);
             context.OrderDetails.Add(orderDetails);
             context.SaveChanges();
             
         }
         
           return true;
       }

   
    }
}
