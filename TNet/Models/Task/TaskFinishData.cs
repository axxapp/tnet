using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TNet.Models.Task
{
    public class TaskFinishData
    {
        public string idtask { get; set; }
        public string orderno { get; set; }
        public string mgcode { get; set; }
        public string context { get; set; }
        public List<string> imgs { get; set; }
    }
}