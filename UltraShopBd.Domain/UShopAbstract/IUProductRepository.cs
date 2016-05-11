using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltraShopBd.Domain.UShopEntity;

namespace UltraShopBd.Domain.UShopAbstract
{
  public  interface IUProductRepository
    {
        IEnumerable<product> products { get; }
        IEnumerable<color> colors { get; }
        IEnumerable<stock> stocks { get; }
        IEnumerable<orderdetail> order_details { get; }
        IEnumerable<purchase> purchases { get; }
        void SavePruchase(purchase APurchase);
        void SaveProduct(product AProduct);
        void UpdateStock(stock stock);
        void DeleteProduct(int ProductID);
        int AddOtherExpenseList(otherexpenselist oel);
    }
}
