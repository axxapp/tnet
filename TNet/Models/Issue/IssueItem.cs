﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using TNet.Models.Task;

namespace TNet.Models.Issue
{
    //[DataContract]
    public class IssueItem
    {
        public IssueItem()
        {
            Imgs = new List<string>();
        }

        public TCom.EF.Issue Issue { get; set; }

     
        [IgnoreDataMember]
        public TCom.EF.Task _Task
        {
            get
            {
                return null;
            }
            set
            {
                if(value != null){
                    Task = new TaskItem(value);
                }
                
            }
        }

        public TaskItem Task { get; set; }

        public List<string> Imgs { get; set; }
    }
}