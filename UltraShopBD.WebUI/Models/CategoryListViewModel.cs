using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UltraShopBD.Domain.Entities;

namespace UltraShopBD.WebUI.Models
{
    public class CategoryListViewModel
    {
        public IEnumerable<Category> Categories { get; set; }
    }
}