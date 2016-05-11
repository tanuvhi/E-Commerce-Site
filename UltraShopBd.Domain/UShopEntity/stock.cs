using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltraShopBd.Domain.UShopEntity
{
  public  class stock
    {
      [Key]
      public int stock_Id { get; set; }
      public int product_id { get; set; }
      public int color_id { get; set; }
      public int size_id { get; set; }
      public int  quantity     { get; set; }
      public decimal selling_price { get; set; }
      public decimal purchase_price { get; set; }
    }
}
