using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.EntityClient;
namespace UltraShopBd.Domain.UShopEntity

{
    public class product
    {
        [Key]
        public int product_Id { get; set; }
        public string product_name { get; set; }
        public int cat_id { get; set; }
        public int sub_cat_id { get; set; }
        public int sub_sub_cat_id { get; set; }
        public string description { get; set; }
        public string color { get; set; }
        public int condition_id { get; set; }
    }
}
