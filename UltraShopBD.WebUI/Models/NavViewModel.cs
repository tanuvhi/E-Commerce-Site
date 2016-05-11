using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UltraShopBd.Domain.Entities;


namespace UltraShopBd.WebUI.Models
{
    public class NavViewModel
    {

        public IEnumerable< SubCategory> SubCategories{get; set;}

        public IEnumerable< Category> Categories { get; set; }

       
    }
}