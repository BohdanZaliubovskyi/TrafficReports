using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TestTrafficReports.Models
{
    public class CarriageContext : DbContext
    {
        public DbSet<City> Cities { get; set; }
        public DbSet<Сarriage> Сarriages { get; set; }
        public DbSet<CarriageCount> СarriageCount { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
    }

    public class CarriageDbInitializer : DropCreateDatabaseAlways<CarriageContext>
    {
        protected override void Seed(CarriageContext context)
        {
            context.Cities.Add(new City { Name = "Днепр" });
            context.Cities.Add(new City { Name = "Полтава" });
            context.Cities.Add(new City { Name = "Харьков" });
            context.Cities.Add(new City { Name = "Луганск" });
            context.Cities.Add(new City { Name = "Запорожье" });
            context.Cities.Add(new City { Name = "Херсон" });
            context.Cities.Add(new City { Name = "Одесса" });
            context.Cities.Add(new City { Name = "Николаев" });
            context.Cities.Add(new City { Name = "Кировоград" });
            context.SaveChanges();

            //-----------------------------------------------
            var cities = context.Cities.ToList();

            var carrCount = new Random().Next(8, 15);
            for (int i = 0; i < carrCount; ++i)
            {
                int sendingCityId = cities[new Random().Next(cities.Count)].Id;
                int arrivalCityId = -1;
                while (true)
                {
                    arrivalCityId = cities[new Random().Next(cities.Count)].Id;
                    if (arrivalCityId != sendingCityId)
                        break;
                }

                context.Сarriages.Add(new Сarriage() { SendingCityId = sendingCityId, ArrivalCityId = arrivalCityId, PlanCarriage = new Random().Next(15) });
            }
            context.SaveChanges();
            //-----------------------------------------------
            var carriages = context.Сarriages.ToList();

            int month = DateTime.Now.Month;
            foreach (var сarriage in carriages)
            {
                var dateForWork = new DateTime(DateTime.Now.Year, month, 1);
                while (month == dateForWork.Month)
                {
                    context.СarriageCount.Add(new CarriageCount() { CarriageId = сarriage.Id, CarrDate = dateForWork, CarrValue = new Random().Next(3) });
                    dateForWork = dateForWork.AddDays(1);
                }

            }
            Role r1 = new Role { Name = "admin" };
            Role r2 = new Role { Name = "userPlanFact" };
            Role r3 = new Role { Name = "userGroup" };
            context.Roles.AddRange(new List<Role> {r1,r2,r3});
            context.SaveChanges();

            User u1 = new User { Login = "admin", Password = "admin" };
            u1.Roles.Add(r1);
            context.Users.Add(u1);
            User u2 = new User { Login = "ugroup", Password = "ugroup" };
            u2.Roles.Add(r3);
            context.Users.Add(u2);
            User u3 = new User { Login = "uplan", Password = "uplan" };
            u3.Roles.Add(r2);
            context.Users.Add(u3);
            User u4 = new User { Login = "user", Password = "user" };
            context.Users.Add(u4);

            context.SaveChanges();

            base.Seed(context);
        }
    }
}