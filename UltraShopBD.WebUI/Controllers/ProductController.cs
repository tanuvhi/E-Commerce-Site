using ImageResizer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Helpers;
using System.Web.Mvc;
using UltraShopBd.Domain.Abstract;
using UltraShopBd.Domain.Concrete;
using UltraShopBd.Domain.UShopConcrete;
using UltraShopBd.Domain.UShopEntity;
using UltraShopBd.WebUI.Models;

using UltraShopBd.Domain.Entities;
using UltraShopBd.Domain.UShopAbstract;
namespace UltraShopBd.WebUI.Controllers
{
    public class ProductController : Controller
    {

        private readonly IProductRepository repository;
        private readonly IUProductRepository UPRepository;
        public int PageSize =48;


        public ProductController(IProductRepository repo, IUProductRepository Urepo)
        {
           
            UPRepository=Urepo;
            repository = repo;
        }
        public ViewResult List(int CategoryId, int page = 1, int SubCategoryId = 0)
        {

            //   string[] imageFiles = Directory.GetFiles(Server.MapPath("~/Content/Image/"+));
            ProductListViewModel model = new ProductListViewModel
            {
                Products =  repository.Products
               
                .Where(p => CategoryId == null || p.CategoryId == CategoryId)
               
                .Where(p => SubCategoryId == 0 || p.SubCategoryId == SubCategoryId )
               .Where(p=>p.Activity==1)
                .OrderByDescending(p => p.ProductId)
                 .Skip((page - 1) * PageSize)
                 .Take(PageSize),
                PagingInfo = new PagingInfo
                {

                    CurrentPage = page,
                    ItemsPerPages = PageSize,
                    TotalItems = CategoryId == null ?
                                repository.Products.Count() :
                                repository.Products.Where(p => p.CategoryId == CategoryId).Count()

                },
                CurrentCategoryId = CategoryId,
                CurrentSubCategoryId = SubCategoryId

            };

          
            

            return View(model);


        }
        public ViewResult ultraProList()
        {
            

            IEnumerable<product> Uproduct = UPRepository.products;
            return View(Uproduct);
        }



        [Authorize(Roles="1")]
        public ActionResult Upload()
        {

            ViewBag.Cetegory = new SelectList(repository.Categories, "CategoryId", "Name");
            ViewBag.SubCategory = new SelectList(repository.SubCategories(1), "CategoryId", "Name");

            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public string CheckName(string name)
        {

            var pName = repository.Products.FirstOrDefault(m => m.Name == name.Trim());

            if (pName != null)
            {
                return "Not Available";

            }
            else
                return "Available";
        }

         
        [HttpPost]
        public ActionResult Upload(ProductListViewModel model, EFProductRepositoy efP)
        {
            product Uproduct = new product();
         
            ViewBag.Cetegory = new SelectList(repository.Categories, "CategoryId", "Name");
            ViewBag.SubCategory = new SelectList(repository.SubCategories(1), "CategoryId", "Name");

           
            if (ModelState.IsValid)
            {
          


               model.ProductU.Activity = 1;
               Uproduct.cat_id = model.ProductU.CategoryId;
               Uproduct.sub_cat_id = model.ProductU.SubCategoryId;
               Uproduct.sub_sub_cat_id = model.ProductU.SubSubCategoryId;
               Uproduct.product_name = model.ProductU.Name;
               Uproduct.description = model.ProductU.Description;

               UPRepository.SaveProduct(Uproduct);
               int PId= efP.AddProduct(model.ProductU);

               
                string folderName = "p" + PId.ToString();
                var Cratepath = Server.MapPath("~/Content/Image/" + folderName);
                var versions = new Dictionary<string, string>();
                int count = 0;
                
                Directory.CreateDirectory(Cratepath);
                var path = Server.MapPath("~/Content/Image/" + folderName + "/");
                versions.Add(model.ProductU.Name, "maxwidth=1200&maxheight=1200&format=jpg");
                foreach (var file in model.File)
                {

                    count++;

                    //Declare a new dictionary to store the parameters for the image versions.
                    string fname = "1" + model.ProductU.Name + file.FileName + ".jpg";

                    //Define the versions to generate
                    if (count == 1)

                        model.ProductU.ImageUrl = "http://www.ekhaneishob.com/Content/Image/" + folderName + "/" + fname;


                    //Generate each version
                    foreach (var suffix in versions.Keys)
                    {
                        file.InputStream.Seek(0, SeekOrigin.Begin);

                        //Let the image builder add the correct extension based on the output file type

                        ImageBuilder.Current.Build(
                       new ImageJob(
                     file.InputStream,
                     path + count.ToString() + suffix + file.FileName,
                     new Instructions(versions[suffix]),
                     false,
                     true));
                        break;
                    }

                }

                efP.UpdateImageUrl(model.ProductU.ImageUrl,PId);
                TempData["message"] = string.Format("{0} Added Successfully", Uproduct.product_name);
                return Redirect(Url.Action("Index", "Admin"));
            }



           




            return View(model);
        }


        [HttpPost]
        [AllowAnonymous]
        public ActionResult GetSubCategoryByCategoryId(string catId)
        {
            if (String.IsNullOrEmpty(catId))
            {
                throw new ArgumentNullException("catId");
            }
            int id = 0;
            bool isValid = Int32.TryParse(catId, out id);
            var states = repository.SubCategories(id);
            var result = (from s in states
                          select new
                          {
                              id = s.SubCategoryId,
                              name = s.Name
                          }).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
