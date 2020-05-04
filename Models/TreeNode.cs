using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestTrafficReports.Models
{
    public class TreeNode
    {
        public int ParentId { get; set; }
        public int CurrentId { get; set; }
        public int PlanCarriages { get; set; }
        public int FactCarriages { get; set; }
    }
}