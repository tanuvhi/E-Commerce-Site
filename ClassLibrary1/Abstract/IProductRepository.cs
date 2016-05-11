using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltraShopBD.Domain.Entities;

namespace UltraShopBD.Domain.Abastract
{
   public interface IProductRepository
    {
        IEnumerable<Product> Products { get; }

        IEnumerable<Category> Categories { get; }
    }
}
