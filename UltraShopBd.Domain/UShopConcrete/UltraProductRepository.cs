using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltraShopBd.Domain.UShopAbstract;
using UltraShopBd.Domain.UShopEntity;

namespace UltraShopBd.Domain.UShopConcrete
{
   public class UltraProductRepository : IUProductRepository
    {
        private readonly UltraShopEFContex Ucontext = new UltraShopEFContex();

        public IEnumerable<product> products
        {
            get { return Ucontext.products; }
        }
        public IEnumerable<color> colors
        {
            get { return Ucontext.colors; }
        }
        public IEnumerable<purchase> purchases
        {
            get { return Ucontext.purchases; }
        }
        public IEnumerable<orderdetail> order_details
        {
            get { return Ucontext.order_details; }
        }
        public IEnumerable<otherexpense> otherexpenses
        {
            get { return Ucontext.otherexpenses; }
        }
        public IEnumerable<otherexpenselist> otherexpenselists
        {
            get { return Ucontext.otherexpenselists; }
        }
       public void SaveProduct(product AProduct)
        {
            if (AProduct.product_Id == 0)
            {
                Ucontext.products.Add(AProduct);
               
            }
            else
            {
                product produ = Ucontext.products.Find(AProduct.product_Id);

               produ.product_name=  AProduct.product_name;
               produ.sub_cat_id = AProduct.sub_cat_id;
               produ.description = AProduct.description;
               produ.sub_sub_cat_id = AProduct.sub_sub_cat_id;
               produ.cat_id = AProduct.cat_id;
            }
            Ucontext.SaveChanges();
        }
       public void UpdateStock(stock stock)
       {
          int StockId= Ucontext.stocks
               .Where(S => S.product_id == stock.product_id && S.color_id==stock.color_id && S.size_id==stock.size_id)
               .Select(S=>S.stock_Id)
               .FirstOrDefault();
           if(StockId==0)
           {
               Ucontext.stocks.Add(stock);

           }
           else
           {

               stock aStock = Ucontext.stocks.Find(StockId);
               aStock.purchase_price = stock.purchase_price;
               aStock.quantity += stock.quantity;
               aStock.selling_price = stock.selling_price;
              
           }
           Ucontext.SaveChanges();
       }
       public IEnumerable<stock> stocks
       {
           get { return Ucontext.stocks; }
       }
       public void DeleteProduct(int ProductID)
       {

           if (Ucontext.order_details.Where(P => P.product_id == ProductID).Select(S => S.product_id).FirstOrDefault()== 0)
           {
               var pro = Ucontext.products.Where(p => p.product_Id == ProductID).FirstOrDefault();

               Ucontext.products.Remove(pro);
               Ucontext.SaveChanges();

           }
        

       }
       public void SavePruchase(purchase APurchase)
       {

           Ucontext.purchases.Add(APurchase);
           Ucontext.SaveChanges();
       }

       public int AddOtherExpenseList(otherexpenselist oel)
       {
          Ucontext.otherexpenselists.Add(oel);
           return oel.other_expense_list_id;

       }

       public void SaveOtherExpense(otherexpense oe)
       {
         

       }

    }
}
