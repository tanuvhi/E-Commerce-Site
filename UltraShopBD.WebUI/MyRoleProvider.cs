using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using UltraShopBd.Domain.Abstract;

namespace UltraShopBd.WebUI
{
    public class MyRoleProvider : RoleProvider
    {
        private readonly IUserRepository Urepository;

        public MyRoleProvider(IUserRepository repo)
        {
            Urepository = repo; 
        }
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            string s = Urepository.Users.Where(u => u.Email == username).FirstOrDefault().UserStatusId.ToString();
            string[] resultS = { s };
            return resultS;
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}