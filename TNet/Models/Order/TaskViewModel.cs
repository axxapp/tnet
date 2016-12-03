﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TCom.EF;
using System.Collections;
using TCom.Model.Task;

namespace TNet.Models
{
    [NotMapped]
    public class TaskViewModel:TCom.EF.Task, IEqualityComparer<TaskViewModel>
    {
        [Display(Name = "工单编号")]
        [StringLength(60)]
        public new string idtask { get; set; }

        [Display(Name = "用户编号")]
        [Required]
        [StringLength(60)]
        public new string iduser { get; set; }

        [Display(Name = "用户名称")]
        [Required]
        [StringLength(50)]
        public new string name { get; set; }

        [Display(Name = "订单编号")]
        [StringLength(60)]
        public new string orderno { get; set; }

        [Display(Name = "受理日期")]
        public new DateTime? accpeptime { get; set; }

        [Display(Name = "创建时间")]
        public new DateTime? cretime { get; set; }

        [Display(Name = "发送者编号")]
        [StringLength(60)]
        public new string idsend { get; set; }

        [Display(Name = "发送者")]
        [StringLength(60)]
        public new string send { get; set; }

        [Display(Name = "接受时间")]
        public new DateTime? revctime { get; set; }

        [Display(Name = "开始工作")]
        public new DateTime? dotime { get; set; }

        [Display(Name = "完成工作")]
        public new DateTime? finishtime { get; set; }

        [Display(Name = "回复时间")]
        public new DateTime? echotime { get; set; }

        [Display(Name = "状态")]
        public new int? status { get; set; }

        [Display(Name = "联系人")]
        [StringLength(50)]
        public new string contact { get; set; }

        [Display(Name = "地址")]
        [StringLength(100)]
        public new string addr { get; set; }

        [Display(Name = "手机号")]
        [StringLength(13)]
        public new string phone { get; set; }

        [Display(Name = "标题")]
        [StringLength(150)]
        public new string title { get; set; }

        [Display(Name = "内容")]
        [StringLength(200)]
        public new string text { get; set; }

        [Display(Name = "工单类型")]
        public new int? tasktype { get; set; }

        [Display(Name = "分数")]
        public new int score { get; set; }

        [Display(Name = "备注")]
        [StringLength(50)]
        public new string notes { get; set; }

        [Display(Name = "启用")]
        public new bool inuse { get; set; }

        [Display(Name ="用户")]
        public TCom.EF.User user { get; set; }

        [Display(Name ="订单")]
        public TCom.EF.MyOrder Order { get; set; }

        [Display(Name ="任务接收者")]
        public List<TaskRecverViewModel> TaskRecvers { get; set; }

        [Display(Name = "派单类型")]
        public string tasktypeName { get {
               return TaskViewModel.TaskTypes.Where(en => en.DisplayValue == tasktype).First().DisplayText;
            } }

        /// <summary>
        /// 派单类型下拉选项
        /// </summary>
        [Display(Name= "派单类型")]
        public static List<SelectItemViewModel<int>> TaskTypes { get{
                List<SelectItemViewModel<int>> list = new List<SelectItemViewModel<int>>();
                foreach (var item in TaskType.s)
                {
                    list.Add(new SelectItemViewModel<int>() {
                         DisplayValue=item.Key,
                         DisplayText=item.Value
                    });
                }
                return list;
            } }

        public bool Equals(TaskViewModel x, TaskViewModel y)
        {
            return x.idtask == y.idtask;
        }

        public int GetHashCode(TaskViewModel model)
        {
            return model.ToString().GetHashCode();
        }

        public void CopyFromBase(TCom.EF.Task task)
        {
            this.idtask = task.idtask;
            this.iduser = task.iduser;
            this.name = task.name;
            this.orderno = task.orderno;
            this.cretime = task.cretime;
            this.idsend = task.idsend;
            this.send = task.send;
            this.accpeptime = task.accpeptime;
            this.revctime = task.revctime;
            this.dotime = task.dotime;
            this.finishtime = task.finishtime;
            this.echotime = task.echotime;
            this.status = task.status;
            this.contact = task.contact;
            this.addr = task.addr;
            this.phone = task.phone;
            this.title = task.title;
            this.text = task.text;
            this.tasktype = task.tasktype;
            this.score = task.score;
            this.notes = task.notes;
            this.inuse = task.inuse;
        }

        public void CopyToBase(TCom.EF.Task task)
        {
            task.idtask = this.idtask;
            task.iduser = this.iduser;
            task.name = this.name;
            task.orderno = this.orderno;
            task.cretime = this.cretime;
            task.idsend = this.idsend;
            task.send = this.send;
            task.accpeptime = this.accpeptime;
            task.revctime = this.revctime;
            task.dotime = this.dotime;
            task.finishtime = this.finishtime;
            task.echotime = this.echotime;
            task.status = this.status;
            task.contact = this.contact;
            task.addr = this.addr;
            task.phone = this.phone;
            task.title = this.title;
            task.text = this.text;
            task.tasktype = this.tasktype;
            task.score = this.score;
            task.notes = this.notes;
            task.inuse = this.inuse;
        }
    }
}