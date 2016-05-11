using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;
using UltraShopBd.Domain.Entities;


namespace UltraShopBd.WebUI.Models
{
    public class ProductDetailsViewModel
    {
        public Product product{get; set;}

       public FileInfo[] files{get;set;}
        
        [Display (Name="Quantity :")]
       public int Quantity { get; set; }

        public int count = 0;
    }
}