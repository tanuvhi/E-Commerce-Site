

using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UltraShopBd.Domain.Abstract;
using UltraShopBd.Domain.Entities;
using UltraShopBd.Domain.UShopAbstract;
using UltraShopBd.Domain.UShopConcrete;
using UltraShopBd.Domain.UShopEntity;
using UltraShopBd.WebUI.Models;

namespace UltraShopBd.WebUI.Controllers
{



    [Authorize(Roles = "1")]
    public class AdminController : Controller
    {
      private readonly IUProductRepository Urepository;
        private readonly IProductRepository repository;
        private readonly IOrderRepository orderRepository;
        private readonly IProductRepository productRepository;
        private readonly IUserRepository userRepository;
        string ProductName;
        private List<User> userP { get; set; }
        public FileInfo[] files { get; set; }
        public AdminController(IProductRepository repo, IOrderRepository orRepo, IProductRepository proRepo, IUserRepository uRepo, IUProductRepository IUrepo)
        {
            Urepository =IUrepo;
            orderRepository = orRepo;
            repository = repo;
            productRepository = proRepo;
            userRepository = uRepo;
        }
        public ActionResult Index()
        {
            return View(
                repository.Products.OrderByDescending(p => p.ProductId)
                .Where(P=>P.Activity==1)
                );
        }
        public ActionResult HideList()
        {
            return View(
              repository.Products.OrderByDescending(p => p.ProductId)
              .Where(P => P.Activity == 2)
              );
        }
        public ActionResult ActiveOrHide(int ProductID,int ActiveID)
        {
            productRepository.ActiveOrHide(ProductID, ActiveID);

            if(ActiveID==1)
            {
                 TempData["message"] = string.Format("Product Has Been Hide");
                     return RedirectToAction("Index");
               }
            else
            {
                TempData["message"] = string.Format("Product Has Been Activated");
                return RedirectToAction("HideList");
            }
            return View();
        }
        //        public ViewResult Create()
        //        {
        //            return View(new Product());
        //        }
        //        [HttpPost]
        //        public ActionResult Create(Product product)
        //        {
        //            if (ModelState.IsValid)
        //            {
        //                repository.SaveProduct(product);
        //                TempData["message"] = string.Format("{0} has been saved", product.Name);
        //                return RedirectToAction("Index");
        //            }
        //            else
        //            {
        //                // there is something wrong with the data values
        //                return View(product);
        //            }
        //        }
        
        public ActionResult DeleteProduct(int ProductID)
        {


            Urepository.DeleteProduct(ProductID);
            if (productRepository.DeleteProduct(ProductID) ==1)
            {
                
                string foderName = "p" + ProductID.ToString();


                var Cratepath = Server.MapPath("~/Content/Image/" + foderName);
                var path=Server.MapPath("~/Content/Image/" + foderName);
                DirectoryInfo dir = new DirectoryInfo(path);

                foreach (FileInfo fi in dir.GetFiles())
                {
                    fi.IsReadOnly = false;
                    fi.Delete();
                }

                foreach (DirectoryInfo di in dir.GetDirectories())
                {
                    
                    di.Delete();
                }
                Directory.Delete(Cratepath);
                TempData["message"] = string.Format("Product has been Deleted");
                return RedirectToAction("Index");
               
              
            }
            else
            {
                TempData["message"] = string.Format("Product has been Hide");
                return RedirectToAction("Index");
                
            }
            
        }
        public ViewResult Edit(int productId)
        {
            DirectoryInfo directory = new DirectoryInfo(Server.MapPath(@"~/Content/Image/" + "p" + productId));

            ViewBag.files = directory.GetFiles();
            ViewBag.FileCount = ViewBag.files.Length;
            Product product = repository.Products.FirstOrDefault(p => p.ProductId == productId);
            ProductName = product.Name;
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {
           
            if (ModelState.IsValid)
            {
                              
                repository.SaveProduct(product);

                product AProduct = new product();
                AProduct.product_Id = product.ProductId;
                AProduct.product_name =product.Name;
                AProduct.sub_cat_id= product.SubCategoryId;
                AProduct.cat_id= product.CategoryId;
                AProduct.sub_sub_cat_id= product.SubSubCategoryId;
                AProduct.description =product.Description;

                Urepository.SaveProduct(AProduct);
                TempData["message"] = string.Format("{0} has been saved", product.Name);
                return RedirectToAction("Index");
              
            }
            else
            {
                // there is something wrong with the data values
                return View(product);
            }
        }

        public ActionResult Details(int orderId, AdminViewModel adModel)
        {


            adModel.orderDetails = orderRepository.OrderDetails.Where(x => x.OrderId == orderId);
            adModel.product = productRepository.Products.Join(adModel.orderDetails, p => p.ProductId, O => O.ProductId, (p, O) => new { p, O })
                 .Select(s => s.p)
                 .OrderBy(s => s.ProductId)
                 .ToList();

                            

            return View(adModel);
        }

        public ActionResult PendingConfirmation(AdminViewModel adModel)
        {

            adModel.order = orderRepository.Orders
                           .Where(O=>O.OrderStatusId==1)
                           .OrderBy(O=>O.CustomerId)
                           .ToList();

            if (adModel.order.Count() > 0)
            {
                adModel.user = userRepository.Users.Join(adModel.order, U => U.UserId, O => O.CustomerId, (U, O) => new { U, O })
                                .Select(O => O.U)
                                .OrderBy(O => O.UserId)
                                .ToList();
           
            userP = adModel.user; 
            adModel.city = userRepository.City.Join(adModel.user, C => C.CityId, U => U.CityId, (U, C) => new { U, C })
                            .Select(s => s.U)
                            .OrderBy(O => O.CityId)
                            .ToList();

            adModel.orderStatus = orderRepository.OrderStatus.ToList();
            }
            return View(adModel);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult CustomerDetails(string userId)
        {
            if (String.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException("userId");
            }
            int id = 0;
            bool isValid = Int32.TryParse(userId, out id);
            var user = userP[id];

           

            return Json(user, JsonRequestBehavior.AllowGet);
           
        }

        public ViewResult CategoryCreate()
        {

            return View();
        }
        [HttpPost]
        public ActionResult CategoryCreate(Category cat)
        {

            if (ModelState.IsValid)
            {

                repository.UpdateCategory(cat);
                TempData["message"] = string.Format("{0} has been saved", cat.Name);
                return RedirectToAction("CategoryView");

            }
            else
            {

                return View(cat);
            }
        }
        public ViewResult CategoryEdit(int categoryId)
        {
            Category cate = productRepository.Categories.Where(c => c.CategoryId == categoryId).FirstOrDefault();

            return View(cate);
        }

        [HttpPost]
        public ActionResult CategoryEdit(Category cat)
        {
            if (ModelState.IsValid)
            {

                repository.UpdateCategory(cat);
                TempData["message"] = string.Format("{0} has been update", cat.Name);
                return RedirectToAction("CategoryView");

            }
            else
            {
                // there is something wrong with the data values
                return View(cat);
            }
        }
        public ViewResult CategoryView()
        {
            IEnumerable<Category> cate = productRepository.Categories;
            return View(cate);
        }
        public ViewResult SubCategoryView(CategoryModel cateModel)
        {
          cateModel.subCategoy = productRepository.SubCate.OrderBy(s=>s.CategoryId).ToList();
          cateModel.category = productRepository.Categories.ToDictionary(row => row.CategoryId,
                                row => row.Name); 
           
                           
            return View(cateModel);
        }

        public ViewResult SubCategoryCreate( )
        {


            ViewBag.Cetegory = new SelectList(repository.Categories, "CategoryId", "Name");
            return View();

        }
        [HttpPost]
        public ActionResult SubCategoryCreate(SubCategory subCategory)
        {
            
           if(ModelState.IsValid)
           {

               productRepository.UpdateSubCategory(subCategory);
               TempData["message"] = string.Format("{0} has been saved", subCategory.Name);
               return RedirectToAction("SubCategoryCreate");
           }
           
            return View();

        }

        public ViewResult SubCategoryEdit(int subCategoryId)
        {

            ViewBag.Cetegory = new SelectList(repository.Categories, "CategoryId", "Name");
            SubCategory subCate = productRepository.SubCate.Where(c => c.SubCategoryId == subCategoryId).FirstOrDefault();

            return View(subCate);
        }

        [HttpPost]
        public ActionResult SubCategoryEdit(SubCategory subCategory)
        {
            ViewBag.Cetegory = new SelectList(repository.Categories, "CategoryId", "Name");
            if (ModelState.IsValid)
            {

                repository.UpdateSubCategory(subCategory);
                TempData["message"] = string.Format("{0} has been update", subCategory.Name);
                return RedirectToAction("SubCategoryView");

            }
            else
            {
             
                return View(subCategory);
            }
        }
        public ViewResult UserView()
        {
            IEnumerable<User> user = userRepository.Users.OrderBy(U => U.UserId);

            return View(user);
        }
        public ViewResult UserEdit( int userId)
        {
            User user = userRepository.Users.Where(U => U.UserId == userId).FirstOrDefault();

            return View(user);
        }
        [HttpPost]
        public ActionResult UserEdit(User user)
        {
            if (ModelState.IsValid)
            {
                userRepository.UpdateUser(user);
                TempData["message"] = string.Format("{0} has been update", user.UserName);
                return RedirectToAction("UserView");

            }
            else
            {

                return View(user);
            }
           
        }
        //        [HttpPost]
        //        public ActionResult Delete(int productId)
        //        {
        //            Product deletedProduct = repository.DeleteProduct(productId);
        //            if (deletedProduct != null)
        //            {
        //                TempData["message"] = string.Format("{0} was deleted", deletedProduct.Name);
        //            }
        //            return RedirectToAction("Index");
        //        }
        //    }
        //}
        //    }
    }
}