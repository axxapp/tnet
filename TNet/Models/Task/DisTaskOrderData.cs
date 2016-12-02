using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TNet.Models.Mgr;

namespace TNet.Models.Task
{
    public class DisTaskOrderData
    {
        public string orderno { get; set; }

        public string mcode { get; set; }

        public string mname { get; set; }

        public List<string> works { get; set; }
    }
}