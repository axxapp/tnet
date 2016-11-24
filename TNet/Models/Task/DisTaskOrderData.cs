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

        public string iduser { get; set; }

        public string uname { get; set; }

        public List<string> works { get; set; }
    }
}