using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltraShopBd.Domain.Abstract;
using UltraShopBd.Domain.Entities;


namespace UltraShopBd.Domain.Concrete
{
    public class EFProductRepositoy : IProductRepository
    {
        private readonly EFDbContext context = new EFDbContext();

        public IEnumerable<Product> Products
        {
            get { return context.Products; }
        }

        public IEnumerable<Category> Categories
        {
            get { return context.Categories; }
        }
        public IEnumerable<SubCategory> SubCategories(int categoryId)
        {
            return context.SubCategories.Where(c => c.CategoryId == categoryId);
        }

        public IEnumerable<SubCategory> SubCate
        {
            get { return context.SubCategories; }
        }

        public int AddProduct(Product product)
        {
            context.Products.Add(product);
            context.SaveChanges();
           return product.ProductId;
        }
        public void UpdateCategory(Category category)
        {
            if (category.CategoryId == 0)
            {
                context.Categories.Add(category);
            }
            else
            {
                Category cat = context.Categories.Find(category.CategoryId);
                if (cat != null)
                    cat.Name = category.Name;
            }
            context.SaveChanges();
        }

        public void UpdateSubCategory(SubCategory subCategory)
        {
            if (subCategory.SubCategoryId == 0)
            {
                context.SubCategories.Add(subCategory);
            }
            else
            {
                SubCategory cat = context.SubCategories.Find(subCategory.SubCategoryId);
                if (cat != null)
                {
                    cat.CategoryId = subCategory.CategoryId;
                    cat.Name = subCategory.Name;
                }
            }
            context.SaveChanges();
        }
        
        public void SaveProduct(Product product)
        {

            if (product.ProductId == 0)
            {
                context.Products.Add(product);
                
            }
            else
            {
                Product dbEntry = context.Products.Find(product.ProductId);
                if (dbEntry != null)
                {
                    dbEntry.Name = product.Name;
                    dbEntry.Description = product.Description;
                    dbEntry.Price = product.Price;
                    dbEntry.CategoryId = product.CategoryId;
                    dbEntry.SubCategoryId = product.SubCategoryId;
                    dbEntry.ImageUrl = product.ImageUrl;

                }
            }
            context.SaveChanges();
   
        }
        public int DeleteProduct(int ProductID)
        {

          int proID= context.OrderDetails.Where(p => p.ProductId == ProductID).Select(P=>P.ProductId).FirstOrDefault();
            if(proID==0)
            {
                var pro = context.Products.Where(p => p.ProductId == ProductID).FirstOrDefault();
             
                context.Products.Remove(pro);
                context.SaveChanges();
                return 1;
            }
            else
            {
                Product pro = context.Products.Find(ProductID);
                pro.Activity = 2;
                     context.SaveChanges();
                return 2;
            }
           
        }

      public  void ActiveOrHide(int ProductId ,int ActiveId)
        {

          if(ActiveId==2)
          {
              Product pro = context.Products.Find(ProductId);
              pro.Activity = 1;
              context.SaveChanges();
          }
          else
          {
              Product pro = context.Products.Find(ProductId);
              pro.Activity = 2;
              context.SaveChanges();
          }
        }

      public void UpdateImageUrl(string url, int ProductId)
      {
          Product pro = context.Products.Find(ProductId);
          pro.ImageUrl = url;
          context.SaveChanges();


      }

    }

}
