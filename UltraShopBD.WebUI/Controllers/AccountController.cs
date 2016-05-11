
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using UltraShopBd.Domain.Abstract;
using UltraShopBd.Domain.Concrete;
using UltraShopBd.Domain.Entities;
using UltraShopBd.WebUI.Models;

namespace UltraShopBd.WebUI.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {


        private readonly IUserRepository repository;
        IAuthentication authentication;

        private string email="";

        public AccountController(IUserRepository repo, IAuthentication authentication)
        {

            repository = repo;

            this.authentication = authentication;
        }


        [AllowAnonymous]
        public ActionResult Login()
        {
           
            return View();
        }
        

 

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginViewModel model,string returnUrl)
        {
            if(ModelState.IsValid)
            {
               int UserStatusId= authentication.Authenticate(model.Email, model.Password);
               if (UserStatusId < 3)
               {
                   Session["MyKey"] = UserStatusId;
                   User user = repository.Users.FirstOrDefault(u => u.Email == model.Email);
                   Session["MyId"] = user.UserId;
                   Session["Name"] = user.UserName;
                   email = model.Email;
               }
               if (UserStatusId == 1) //1 means admin
                {

                    FormsAuthentication.SetAuthCookie(model.Email, false);
                    if (!Roles.RoleExists("1"))
                        Roles.CreateRole("1");
                    
                    if (!Roles.IsUserInRole(model.Email, "1"))
                        Roles.AddUserToRole(model.Email, "1");
                    return Redirect(returnUrl ?? Url.Action("Index", "Admin"));
                   

                }
               else if (UserStatusId == 0) // 0 means customer
                {
                    if (Roles.IsUserInRole(email, "1"))
                        Roles.RemoveUserFromRole(email, "1");
                    FormsAuthentication.SetAuthCookie(model.Email, false);
                    
                    return Redirect(returnUrl ?? Url.Action("Index", "Home"));


                }
                   else if(UserStatusId==2)
               {
                   ModelState.AddModelError("", "Your account has been deactivated. Please contact our Customer Services for help ");
                   return View();
               }
                else
                {
                    ModelState.AddModelError("","Incorect userhname or password ");
                    return View();
                }
            }
            else
            {
                return View();
            }


        }
 
        public ActionResult Logout()
        {
            if (Roles.IsUserInRole(email, "1"))
                Roles.RemoveUserFromRole(email, "1");
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Admin");
        }
     
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            
            ViewBag.Region = new SelectList(repository.Regions, "RegionId", "Name");
            ViewBag.City = new SelectList(repository.Citys(1), "CityId", "Name");

            
            return View();
            
          
           
        }

       [HttpPost]
        [AllowAnonymous]
        public ActionResult GetCityByRegionId(string rgnId)
        {
            if (String.IsNullOrEmpty(rgnId))
            {
                throw new ArgumentNullException("regionId");
            }
            int id = 0;
            bool isValid = Int32.TryParse(rgnId, out id);
            var states = repository.Citys(id);
            var result = (from s in states
                          select new
                          {
                              id = s.CityId,
                              name = s.Name
                          }).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        } 
  

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel users, EFUserRepository ef)
        {
            ViewBag.Region = new SelectList(repository.Regions, "RegionId", "Name");

            if (ModelState.IsValid)
            {
                users.User.UserName.Trim();
                users.User.Address1.Trim();
                
                

                ef.AddUser(users.User);
               
            
                Session["MyKey"] = 0;
                Session["MyId"] = repository.Users.FirstOrDefault(u=>u.Email==users.User.Email).UserId;
                Session["Name"] = users.User.UserName;
                if (Roles.IsUserInRole(users.User.Email, "1"))
                    Roles.RemoveUserFromRole(users.User.Email, "1");
                FormsAuthentication.SetAuthCookie(users.User.Email, false);
                ModelState.AddModelError("", "Registration Successfully ");
                return Redirect(Url.Action("Index", "Home"));
            }

            // If we got this far, something failed, redisplay form
            return View(users);
        }




    }
}