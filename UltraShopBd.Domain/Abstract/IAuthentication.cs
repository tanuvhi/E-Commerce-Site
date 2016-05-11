using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltraShopBd.Domain.Abstract
{
  public  interface IAuthentication
    {
      int  Authenticate(string email, string password);
      bool Logout();

    }
}
