using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltraShopBd.Domain.UShopEntity
{
  public  class otherexpense
    {
       [Key]
        public int other_expense_id { get; set; }
        public int other_expenses_list_id { get; set; }
        public decimal prise { get; set; }
        public DateTime purses_date { get; set; }
    }
}
