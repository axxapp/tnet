using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TNet.Models.Task
{
    public class DisTaskIssueData
    {
        public string issue { get; set; }

        public string mcode { get; set; }

        public string mname { get; set; }

        public List<string> works { get; set; }
    }
}