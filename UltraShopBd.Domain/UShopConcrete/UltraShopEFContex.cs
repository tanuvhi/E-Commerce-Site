using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltraShopBd.Domain.UShopEntity;

namespace UltraShopBd.Domain.UShopConcrete

{
   public class UltraShopEFContex : DbContext
    {
       public DbSet<product> products { get; set; }
       public DbSet<color> colors { get; set; }
       public DbSet<stock> stocks { get; set; }
       public DbSet<orderdetail> order_details { get; set; }
       public DbSet<purchase> purchases { get; set; }
       public DbSet<otherexpense> otherexpenses { get; set;  }
       public DbSet<otherexpenselist> otherexpenselists { get; set; }
    }
}
