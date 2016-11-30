using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using TCom.Model.Task;

namespace TNet.Models.Task
{
    [NotMapped]
    public class TaskRecverItem : TCom.EF.TaskRecver
    {

        public string status_t
        {
            get
            {
                return TaskRecverStatus.get(this.status);
            }
            set
            {

            }
        }



        public TaskRecverItem()
        {

        }

        public TaskRecverItem(TCom.EF.TaskRecver data)
        {
            this.idrecver = data.idrecver;
            this.idtask = data.idtask;
            this.mcode = data.mcode;
            this.mname = data.mname;
            this.cretime = data.cretime;
            this.stime = data.stime;
            this.entime = data.entime;
            this.works = data.works;
            this.donum = data.donum;
            this.smcode = data.smcode;
            this.smname = data.smname;
            this.status = data.status;
            this.inuse = data.inuse;
        }

        public static List<TaskRecverItem> gets(List<TCom.EF.TaskRecver> data)
        {
            if (data != null && data.Count > 0)
            {
                List<TaskRecverItem> t = new List<TaskRecverItem>(data.Count);
                for (int i = 0; i < data.Count; i++)
                {
                    t.Add(new TaskRecverItem(data[i]));
                }
                return t;
            }
            return null;
        }
    }
}