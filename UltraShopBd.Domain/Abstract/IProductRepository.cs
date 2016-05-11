using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltraShopBd.Domain.Entities;

namespace UltraShopBd.Domain.Abstract
{
   public interface IProductRepository
    {
      IEnumerable<Product> Products { get; }

      IEnumerable<Category> Categories { get; }
      IEnumerable<SubCategory> SubCategories(int CategoryID);

      IEnumerable<SubCategory> SubCate{ get; }
      void UpdateCategory(Category category);

      int DeleteProduct(int ProductID);
      void UpdateSubCategory(SubCategory subCateroy);
      void SaveProduct(Product product);
      void ActiveOrHide(int ProductId, int ActiveId);
      void UpdateImageUrl(string url, int ProductId);
    }
}
