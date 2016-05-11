using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UltraShopBd.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace UltraShopBd.Domain.Concrete
{
    public class EFDbContext: DbContext
    {

     public   DbSet<Product> Products { get; set; }
     public   DbSet<Category> Categories { get; set; }
     public DbSet<User> Users { get; set; }

     public DbSet<Region> Regions { get; set; }

     public DbSet<City> Cities { get; set; }

     public DbSet<SubCategory> SubCategories { get; set; }

     public DbSet<OrderDetail> OrderDetails{ get; set; }
     public DbSet<Order> Orders { get; set; }
     public DbSet<OrderStatus> OrderStatus_ { get; set; }

   


    }
}
