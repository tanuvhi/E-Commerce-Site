using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UltraShopBd.Domain.UShopEntity;

namespace UltraShopBd.WebUI.Models
{
    public class UltraShopModel
    {
       public  IEnumerable<stock> stock { get; set; }
        public Dictionary<int, string> Product { get; set; }
        public Dictionary<int, string> Color { get; set; }
        public IEnumerable<purchase> Purchase { get; set; }
        public Dictionary<int, string> ImageUrl { get; set; }
    }
}