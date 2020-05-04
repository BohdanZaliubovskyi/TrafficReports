using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestTrafficReports.Models
{
    public class NodesViewModel
    {
        public List<TreeNode> Nodes { get; set; }
        public Dictionary<int, City> Cities { get; set; }
    }
}