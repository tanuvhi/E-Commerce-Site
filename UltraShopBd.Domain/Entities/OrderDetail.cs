using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltraShopBd.Domain.Entities
{
   public class OrderDetail
    {
       [Key]
        public int Id{ get; set; }
        public int OrderId { get; set; }

        public int ProductId { get; set;  }

        public int ColorId { get; set; } 

        public int Quantity { get; set; }
        public decimal Total { get; set; }
    }
}
