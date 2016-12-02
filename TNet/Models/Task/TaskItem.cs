using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using TCom.Model.Task;

namespace TNet.Models.Task
{ 
    public class TaskItem : TaskDetailItem
    {
        [IgnoreDataMember]
        public int? _rs { get; set; }
         
        public string idrecver { get; set; }

        public TaskRecverStatusItem recverStatusObj
        {
            get
            {
                return TaskRecverStatus.get(this._rs);
            }
            set
            {

            }
        }

    }
}