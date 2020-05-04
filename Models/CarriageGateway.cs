using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestTrafficReports.Models
{
    public static class CarriageGateway
    {
        public static List<Сarriage> GetAllCarriages()
        {
            var rez = new List<Сarriage>();

            using (var db = new CarriageContext())
            {
                rez = db.Сarriages.Include("FactualCarriage").ToList();
            }

            return rez;
        }
        public static List<Сarriage> GetCarriagesBySendingCityId(int sendingCityId)
        {
            var rez = new List<Сarriage>();

            using (var db = new CarriageContext())
            {
                rez = db.Сarriages.Include("FactualCarriage").Where(c => c.SendingCityId == sendingCityId).ToList();
            }

            return rez;
        }
        public static List<Сarriage> GetCarriagesByArrivalCityId(int arrivalCityId)
        {
            var rez = new List<Сarriage>();

            using (var db = new CarriageContext())
            {
                rez = db.Сarriages.Include("FactualCarriage").Where(c => c.ArrivalCityId == arrivalCityId).ToList();
            }

            return rez;
        }
         public static List<Сarriage> GetCarriagesBySendingAndArrivalCityId(int sendingCityId, int arrivalCityId)
        {
            var rez = new List<Сarriage>();

            using (var db = new CarriageContext())
            {
                rez = db.Сarriages.Include("FactualCarriage").Where(c => c.ArrivalCityId == arrivalCityId).Where(c => c.SendingCityId == sendingCityId).ToList();
            }

            return rez;
        }
    }
}