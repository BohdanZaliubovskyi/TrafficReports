using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestTrafficReports.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        // связь многие ко многим codefirst
        public ICollection<Role> Roles { get; set; }
        public User()
        {
            Roles = new List<Role>();
        }
    }
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // связь многие ко многим codefirst
        public ICollection<User> Users { get; set; }
        public Role()
        {
            Users = new List<User>();
        }
    }
    public class UserViewModel
    {
        public List<User> Users { get; set; }
    }

    public class UserRolesChangeViewModel
    {
        public User User { get; set; }
        public List<string> UserRoles { get; set; }
        public List<Role> AllRoles { get; set; }
    }
}