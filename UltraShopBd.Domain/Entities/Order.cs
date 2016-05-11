using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltraShopBd.Domain.Entities
{
   public class Order
    {
    

       [Key]
        public int OrderId { get; set; }
       
        public int? CustomerId { get; set; }
        public string OrderDate { get; set; }
        public int OrderStatusId { get; set; }
        public decimal ProductPrice { get; set; }
        public decimal DeliveryCharge { get; set; }
        public decimal TotalAmount { get; set; }
        
    }
}
