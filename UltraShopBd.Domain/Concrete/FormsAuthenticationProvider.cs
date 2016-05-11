using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltraShopBd.Domain.Abstract;

namespace UltraShopBd.Domain.Concrete
{
   public class FormsAuthenticationProvider : IAuthentication
    {
       private readonly EFDbContext context = new EFDbContext();
       public int Authenticate(string email, string password)
        {
            var result = context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);

            if (result == null)
                return 10;
            else
            { 
                
                return result.UserStatusId;
            }
        }

        public bool Logout()
        {
            return true;
        }
    }
}
