using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestTrafficReports.Models
{
    public class Сarriage
    {
        public int Id { get; set; }
        public int SendingCityId { get; set; }
        public int ArrivalCityId { get; set; }
        public int PlanCarriage { get; set; }
        public virtual ICollection<CarriageCount> FactualCarriage { get; set; }

        public Сarriage()
        {
            this.FactualCarriage = new List<CarriageCount>();
        }
    }
}