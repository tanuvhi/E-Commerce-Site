﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltraShopBd.Domain.UShopEntity
{
  public  class color
    {
      [Key]
      public int color_id { get; set; }
      public string color_name { get; set; }
    }
}
