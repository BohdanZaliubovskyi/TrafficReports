using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TestTrafficReports.Models;
using TestTrafficReports.Providers;

namespace TestTrafficReports.Controllers
{
    public class AccountController : Controller
    {
        CarriageContext db = new CarriageContext();
        public ActionResult Login()
        {
            return View();
        }     

        [HttpPost]
        [ValidateAntiForgeryToken]
		public ActionResult Login(LoginModel model)   
		{
			if(ModelState.IsValid)
			{
				User user = null;
				
				using(CarriageContext db = new CarriageContext())
				{
					user = db.Users.FirstOrDefault(u => u.Login == model.Login && u.Password == model.Password);
				}
				if(user != null)
				{
					FormsAuthentication.SetAuthCookie(model.Login, true);
					return RedirectToAction("Index", "Home");
				}
				else
				{
					ModelState.AddModelError("", "Пользователя с таким логином и паролем нет");
				}
			}
			
			return View(model);
		}

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "admin")]
        public ActionResult AllUsers()
        {
            UserViewModel userView = new UserViewModel { Users = db.Users.Where(u=>u.Login != "admin").ToList() };
            var usrs = db.Users.Where(u => u.Login != "admin").ToList();
            //ViewBag.Users = usrs;

            return View(userView);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult ChangeRoles(int? Id)
        {
            int id = Convert.ToInt32(Id);
            MyRoleProvider provider = new MyRoleProvider();
            User user = db.Users.FirstOrDefault(u => u.Id == id);            
            UserRolesChangeViewModel userRolesChangeViewModel = new UserRolesChangeViewModel
            { User = user, AllRoles = db.Roles.ToList(), UserRoles = provider.GetRolesForUser(user.Login).ToList()};

            return View(userRolesChangeViewModel);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult ChangeRoles(string userId, List<string> roles)
        {
            MyRoleProvider provider = new MyRoleProvider();
            // получаем пользователя
            int intUserId = Convert.ToInt32(userId);
            User user = db.Users.Where(u=>u.Id== intUserId).FirstOrDefault();
            if (user != null)
            {
                // получем список ролей пользователя
                var userRoles = db.Users.Include("Roles").Where(u => u.Id == user.Id).FirstOrDefault().Roles.Select(r=>r.Name).ToList();
                // получаем все роли
                var allRoles = db.Roles.ToList();
                // получаем список ролей, которые были добавлены
                var addedRoles = roles.Except(userRoles).ToArray();
                // получаем роли, которые были удалены
                var removedRoles = userRoles.Except(roles).ToArray();

                provider.AddUsersToRoles(user, addedRoles);
                provider.RemoveUsersFromRoles(user, removedRoles);

                return RedirectToAction("AllUsers", "Account");
            }

            return View();
        }
    }
}