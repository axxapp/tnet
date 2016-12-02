using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using TCom.Model.Task;

namespace TNet.Models.Task
{
    [NotMapped]
    public class TaskDetailItem : TCom.EF.Task
    {
        public string tasktype_t
        {
            get
            {
                return TaskType.get(this.tasktype);
            }
            set
            {

            }
        }

        public TaskStatusItem statusObj
        {
            get
            {
                return TaskStatus.get(this.status);
            }
            set
            {

            }
        }

        public long workTime
        {
            get
            {
                if (this.revctime != null)
                {
                    return (long)(DateTime.Now - this.revctime.Value).TotalMinutes;
                }
                return 0;
            }
            set
            {

            }
        }
        

        [IgnoreDataMember]
        public TCom.EF.Task _task
        {
            get
            {
                return null;
            }
            set
            {
                setTaskItem(value);
            }
        }

        public void setTaskItem(TCom.EF.Task data)
        {
            this.idtask = data.idtask;
            this.iduser = data.iduser;
            this.name = data.name;
            this.idsend = data.idsend;
            this.send = data.send;
            this.orderno = data.orderno;
            this.accpeptime = data.accpeptime;
            this.cretime = data.cretime;
            this.revctime = data.revctime;
            this.dotime = data.dotime;
            this.finishtime = data.finishtime;
            this.echotime = data.echotime;
            this.status = data.status;
            this.contact = data.contact;
            this.addr = data.addr;
            this.phone = data.phone;
            this.title = data.title;
            this.text = data.text;
            this.tasktype = data.tasktype;
            this.score = data.score;
            this.notes = data.notes;
            this.inuse = data.inuse;
        }

    }
}