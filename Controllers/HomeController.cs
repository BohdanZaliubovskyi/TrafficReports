using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestTrafficReports.Models;

namespace TestTrafficReports.Controllers
{
    public class HomeController : Controller
    {
        CarriageContext db = new CarriageContext();
        public ActionResult Index()
        {
        	string rez = "Вы не авторизованы";
        	if(User.Identity.IsAuthenticated)
        	{
        		rez = string.Format("Привет, {0}!", User.Identity.Name);
        	}
        	ViewBag.Message = rez;
            return View();
        }        

        [Authorize(Roles = "admin,userPlanFact")]
        public ActionResult PlanFact(int? sendingCity, int? arrivalCity)
        {
            //формирование запроса а не данных
            IQueryable<Сarriage> сarriages = db.Сarriages/*.Include("FactualCarriage")*/;
            if (sendingCity != null && sendingCity != 0)
            {
                сarriages = сarriages.Where(c=> c.SendingCityId == sendingCity);
            }
            if (arrivalCity != null && arrivalCity != 0)
            {
                сarriages = сarriages.Where(c => c.ArrivalCityId == arrivalCity);
            }

            List<City> cities = db.Cities.ToList();
            // устанавливаем начальный элемент, который позволит выбрать всех
            cities.Insert(0, new City { Name = "Все", Id = 0 });

            CarriageFilterModel cfm = new CarriageFilterModel
            {
                CurСarriages = сarriages.ToList(),               

                Cities = new SelectList(cities, "Id", "Name"),                   
            };
            foreach (var c in cfm.CurСarriages) // почему-то не получилась ленивая загрузка
                c.FactualCarriage = db.СarriageCount.Where(cc => cc.CarriageId == c.Id).ToList();

            ViewBag.Month = DateTime.Now.Month;
            int dayCount = DateTime.Now.Day;
            var workDate = DateTime.Now;
            while (workDate.Month == DateTime.Now.Month)
            {
                dayCount = workDate.Day;
                workDate = workDate.AddDays(1);
            }
            ViewBag.DayCount = dayCount;
            var citiesDict = new Dictionary<int, City>();
            foreach (var c in cities)
                citiesDict.Add(c.Id, c);
            ViewBag.CitiesDict = citiesDict;
            
            ViewBag.FactCarrSum = 0;

            return View(cfm);
        }

        [Authorize(Roles = "admin,userGroup")]
        public ActionResult Group()
        {
            var carriages = db.Сarriages.ToList();
            var nodes = new List<TreeNode>();

            var cities = db.Cities.ToList();
            var citiesDict = new Dictionary<int, City>();
            foreach (var c in cities)
                citiesDict.Add(c.Id, c);
            //ViewBag.CitiesDict = citiesDict;

            foreach (var city in cities)
            {
                var tmpNodeLevel = carriages.Where(c => c.SendingCityId == city.Id).ToList();
                int planCarr = 0;
                int factCarr = 0;
                foreach (var carr in tmpNodeLevel)
                {
                    carr.FactualCarriage = db.СarriageCount.Where(cc => cc.CarriageId == carr.Id).ToList();
                    planCarr += carr.PlanCarriage;
                    foreach (var cc in carr.FactualCarriage)
                        factCarr += cc.CarrValue;
                }
                if (tmpNodeLevel != null && tmpNodeLevel.Count > 0)
                    nodes.Add(new TreeNode { CurrentId = tmpNodeLevel[0].SendingCityId, FactCarriages = factCarr, PlanCarriages = planCarr, ParentId = -1 });
            }

            foreach (var carr in carriages)
            {
                int factCarr = 0;
                foreach (var cc in carr.FactualCarriage)
                    factCarr += cc.CarrValue;

                nodes.Add(new TreeNode { CurrentId = carr.ArrivalCityId, FactCarriages = factCarr, PlanCarriages = carr.PlanCarriage, ParentId = carr.SendingCityId });
            }

            NodesViewModel nodesViewModel = new NodesViewModel { Nodes = nodes, Cities= citiesDict };

            return View(nodesViewModel);
        }
    }
}