using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestTrafficReports.Models
{
    public class CarriageCount
    {
        public int Id { get; set; }
        public int CarriageId { get; set; }
        public DateTime CarrDate { get; set; }
        public int CarrValue { get; set; }
    }
}