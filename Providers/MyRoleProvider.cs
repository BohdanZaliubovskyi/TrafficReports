using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Security;
using TestTrafficReports.Models;

namespace TestTrafficReports.Providers
{
    public class MyRoleProvider : RoleProvider
    {
        CarriageContext db = new CarriageContext();
        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public void AddUsersToRoles(User user, string[] roleNames)
        {
            user = db.Users.Include("Roles").Where(u => u.Id == user.Id).FirstOrDefault();
            if (user != null)
            {
                foreach (var r in roleNames)
                {
                    Role rl = user.Roles.Where(ro => ro.Name == r).FirstOrDefault();
                    if (rl == null)
                    {
                        user.Roles.Add(db.Roles.Where(rl2=>rl2.Name == r).FirstOrDefault());
                    }
                }

                //db.Users.Where(u => u.Id == user.Id).SingleOrDefault().Roles = user.Roles;
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
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
            string[] roles = new string[] { };
            using (CarriageContext db = new CarriageContext())
            {
                User user = db.Users.Include("Roles").FirstOrDefault(u => u.Login == username);
                if(user != null)
                { 
                    if (user.Roles != null && user.Roles.Count > 0)
                        roles = user.Roles.Select(r=>r.Name).ToArray();
                }
            }

            return roles;
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            bool rez = false;

            using (CarriageContext db = new CarriageContext())
            {
                User user = db.Users.Include("Roles").FirstOrDefault(u => u.Login == username);
                
                if (user != null)
                {
                    Role role = user.Roles.Where(r => r.Name == roleName).FirstOrDefault();                    
                    if (role != null)
                        rez = true;
                }
            }

            return rez;
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }
        public void RemoveUsersFromRoles(User user, string[] roleNames)
        {
            user = db.Users.Include("Roles").Where(u => u.Id == user.Id).FirstOrDefault();
            if (user != null)
            {
                //List<Role> userRoles = db.Users.Where(u => u.Id == user.Id).FirstOrDefault().Roles.ToList();

                if(user.Roles != null && user.Roles.Count >0)
                    foreach (var r in roleNames)
                    {
                        Role rl = user.Roles.Where(ro => ro.Name == r).SingleOrDefault();
                        if (rl != null)
                        {
                            user.Roles.Remove(db.Roles.Where(rl2=>rl2.Name==r).FirstOrDefault());
                        }
                    }

                //db.Users.Where(u => u.Id == user.Id).SingleOrDefault().Roles = user.Roles;
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}