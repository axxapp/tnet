using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TNet.Models.Task
{
    public class IssueData
    {
        public string iduser { get; set; }


        public string context { get; set; }

        public string booktime { get; set; }

        public double lng { get; set; }

        public double lat { get; set; }

        public string contact { get; set; }

        public string addr { get; set; }


        public string phone { get; set; }


        public string notes { get; set; }

        public int tasktype { get; set; }


        public List<string> imgs { get; set; }

    }
}