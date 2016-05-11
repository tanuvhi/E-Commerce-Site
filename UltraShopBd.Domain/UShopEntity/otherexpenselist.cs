using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltraShopBd.Domain.UShopEntity
{
   public class otherexpenselist
    {
       [Key]
        public int other_expense_list_id { get; set; }
        public string name { get; set; }
    }
}
