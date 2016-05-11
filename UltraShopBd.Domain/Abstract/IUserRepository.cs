using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltraShopBd.Domain.Entities;

namespace UltraShopBd.Domain.Abstract
{
   public interface IUserRepository
    {
       IEnumerable<User> Users { get;  }
       IEnumerable<Region> Regions { get; }

       IEnumerable<City> Citys(int regionId);

       IEnumerable<City> City { get; }

       void UpdateUser(User user);
       List<City> GetAllCitysByReigionId(int regionId);
       

   
    }
}
