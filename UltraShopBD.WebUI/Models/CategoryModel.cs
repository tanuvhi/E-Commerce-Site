using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages.Html;
using UltraShopBd.Domain.Entities;
using UltraShopBd.Domain.UShopEntity;

namespace UltraShopBd.WebUI.Models
{
    public class CategoryModel
    {
        public IEnumerable<SubCategory> subCategoy { get; set; }
        public Dictionary<int, string> category { get; set; }
     

    }
}