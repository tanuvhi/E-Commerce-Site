﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltraShopBd.Domain.Entities
{
   public class OrderStatus
    {
       [Key]
        public int OrderStatusId { get; set; }

        public string StatusName { get; set; }
    }
}
