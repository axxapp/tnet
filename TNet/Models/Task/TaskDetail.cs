using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TNet.Models.Task
{
    public class TaskDetail
    {
        public TaskDetailItem task { get; set; }

        public TaskOrder order { get; set; }

        public List<TaskPressItem> press { get; set; }

        public List<TaskRecverItem> recver { get; set; }


        public List<TaskImg> imgs { get; set; }


    }
}