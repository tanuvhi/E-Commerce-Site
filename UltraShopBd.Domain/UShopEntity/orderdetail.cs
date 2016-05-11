using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltraShopBd.Domain.UShopEntity
{
   public class orderdetail
    {
         [Key]
       public int order_details_id { get; set; }
       public int order_id { get; set; }
       public int product_id { get; set; }
       public int quantity { get; set; }
       public int color_id { get; set; }
       public decimal size { get; set; }
       public decimal total { get; set; }
       public decimal price { get; set; }
       public int order_by_id { get; set; }
       public string order_by_id_id { get; set; }
       public int inserted_by { get; set; }

   }
}
