using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using TCom.Model.Task;

namespace TNet.Models.Task
{
    [NotMapped]
    public class TaskPressItem : TCom.EF.TaskPress
    {

        public string ptype_t
        {
            get
            {
                return TaskPressType.get(this.ptype);
            }
            set
            {

            }
        }
         
         

        public TaskPressItem()
        {

        }

        public TaskPressItem(TCom.EF.TaskPress data)
        {
            this.idpress = data.idpress;
            this.idrecver = data.idrecver;
            this.idtask = data.idtask;
            this.cretime = data.cretime;
            this.ptext = data.ptext;
            this.pdesc = data.pdesc;
            this.ptype = data.ptype;
            this.inuse = data.inuse; 
        }

        public static List<TaskPressItem> gets(List<TCom.EF.TaskPress> data)
        {
            if (data != null && data.Count > 0)
            {
                List<TaskPressItem> t = new List<TaskPressItem>(data.Count);
                for (int i = 0; i < data.Count; i++)
                {
                    t.Add(new TaskPressItem(data[i]));
                }
                return t;
            }
            return null;
        }
    }
}