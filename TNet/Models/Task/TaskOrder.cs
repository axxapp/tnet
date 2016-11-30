using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TNet.Models.Task
{
    public class TaskOrder
    {
        public string idmerc { get; set; }

        public string merc { get; set; }

        public string spec { get; set; }

        public double price { get; set; }

        public int count { get; set; }

        public string img { get; set; }

    }
}