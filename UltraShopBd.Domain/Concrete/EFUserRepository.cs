using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltraShopBd.Domain.Abstract;
using UltraShopBd.Domain.Entities;

namespace UltraShopBd.Domain.Concrete
{
   public class EFUserRepository : IUserRepository
    {

        private readonly EFDbContext context = new EFDbContext();

        
        public IEnumerable<User> Users
        {
            get { return context.Users; }
        }
       public IEnumerable<Region>Regions
        {
            get { return context.Regions; }
        }

       public IEnumerable<City> Citys(int regionId)
       {
           return context.Cities.Where(m=> m.RegionId==regionId); 
       }

       public IEnumerable<City>City
       {
           get { return context.Cities; }
       }
     
      
       public void AddUser(User users)
        {
            context.Users.Add(users);
            context.SaveChanges();


        }
       public List<City> GetAllCitysByReigionId(int regionId)
       {
           List<City> CityCollection = new List<City>();
        City c=   CityCollection.Where(m => m.RegionId == regionId).FirstOrDefault();
        CityCollection.Add(c);
           return CityCollection;
       }

       public void UpdateUser(User user)
       {
           if (user.UserId== 0)
           {
               context.Users.Add(user);
           }
           else
           {
               User aUser = context.Users.Find(user.UserId);
               if (aUser != null)
               {
                   aUser.UserId = user.UserId;
                   aUser.UserName = user.UserName;
                   aUser.UserStatusId = user.UserStatusId;
                   aUser.ShipperId = user.ShipperId;
                   aUser.RegionId = user.RegionId;
                   aUser.Phone2 = user.Phone2;
                   aUser.Phone1 = user.Phone1;
                   aUser.Password = user.Password;
                   aUser.Email = user.Email;
                   aUser.CityId = user.CityId;
                   aUser.BrunchId = user.BrunchId;
                   aUser.Address2 = user.Address2;
                   aUser.Address1 = user.Address1;
               }
           }
           context.SaveChanges();
       }

    }
}
