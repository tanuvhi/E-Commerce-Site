﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltraShopBd.Domain.Entities
{
   public class CartLine
    {
        public Product Products { get; set; }
        public int  Quantity { get; set; }

    }
}
