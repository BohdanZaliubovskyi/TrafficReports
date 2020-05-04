using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestTrafficReports.Models
{
    public class CarriageFilterModel
    {
        public IEnumerable<Сarriage> CurСarriages { get; set; }
        public SelectList Cities { get; set; }
        //public SelectList Positions { get; set; }
    }
}