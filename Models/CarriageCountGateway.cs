using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestTrafficReports.Models
{
    public static class CarriageCountGateway
    {
        public static List<CarriageCount> GetCarrCountByCarrId(int carrId)
        {
            var rez = new List<CarriageCount>();

            using (var db = new CarriageContext())
            {
                rez = db.СarriageCount.Where(c => c.CarriageId == carrId).ToList();
            }

            return rez;
        }
    }
}