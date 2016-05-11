using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltraShopBD.Domain.Abastract;
using UltraShopBD.Domain.Entities;

namespace UltraShopBD.Domain.Concrete
{
   public class EFProductRepository : IProductRepository
    {
       private readonly EFDbContext conntext = new EFDbContext();

       public IEnumerable<Product> Products
       {
           get { return conntext.Products; }
       }

       public IEnumerable<Category> Categories
       {
           get { return conntext.Categories; }
       }

    }
}
