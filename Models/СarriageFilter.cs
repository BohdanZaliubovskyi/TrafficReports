using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestTrafficReports.Models
{
	public enum CarriageFilterState
	{
		All = 1,
		SendingCity = 2,
		ArrivalCity = 3,
		SendingAndArrival = 4,		
	}
    public class СarriageFilter
    {
        //public int Id { get; set; }
        public int SendingCityId { get; set; }
        public int ArrivalCityId { get; set; }
        public CarriageFilterState FilterState {get; set;}
    }
}