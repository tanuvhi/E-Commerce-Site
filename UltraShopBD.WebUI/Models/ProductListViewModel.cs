using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using UltraShopBd.Domain.Entities;
using UltraShopBd.Domain.UShopEntity;

namespace UltraShopBd.WebUI.Models
{
    public class ProductListViewModel
    {
        

        
       [Display(Name = "Add Photos")]
        public IEnumerable< HttpPostedFileBase> File { get; set; }

       public int CurrentCategoryId { get; set; }

       public int CurrentSubCategoryId { get; set; }
      public  IEnumerable<Product> Products { get; set; }
       
      public Product ProductU { get; set; }
       public PagingInfo PagingInfo { get; set; }

    }
}