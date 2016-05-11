using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltraShopBd.Domain.UShopEntity
{
 public    class purchase
    {
        [Key]
        public int purchase_Id { get; set; }
        public int product_id { get; set; }
        public int color_id { get; set; }
        public int size_id { get; set; }
        public int quantity { get; set;  }
        public decimal selling_price { get; set;  }
        public decimal purchase_price { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime purchase_date { get; set; }
        public int  inserted_by { get; set; }

    }
}
