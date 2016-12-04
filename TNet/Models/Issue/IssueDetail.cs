using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TNet.Models.Task;

namespace TNet.Models.Issue
{
    public class IssueDetail
    {
        public TCom.EF.Issue issue { get; set; }

        public TaskDetailItem task { get; set; }

        public List<TaskPressItem> press { get; set; }

        public List<TaskRecverItem> recver { get; set; }

        public List<TaskImg> imgs { get; set; } 
    }
}