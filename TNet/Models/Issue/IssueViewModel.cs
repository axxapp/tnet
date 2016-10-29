using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TCom.EF;

namespace TNet.Models
{
    [NotMapped]
    public class IssueViewModel:TCom.EF.Issue
    {
        [Display(Name = "问题编号")]
        [StringLength(60)]
        public new string issue1 { get; set; }

        [Display(Name = "用户编号")]
        [Required]
        [StringLength(60)]
        public new string iduser { get; set; }

        [Display(Name = "内容")]
        [StringLength(200)]
        public new string context { get; set; }

        [Display(Name = "创建时间")]
        public new DateTime? cretime { get; set; }

        [Display(Name = "预约时间")]
        public new DateTime? booktime { get; set; }

        [Display(Name = "纬度")]
        public new double? lng { get; set; }

        [Display(Name = "经度")]
        public new double? lat { get; set; }

        [Display(Name = "地址")]
        [StringLength(180)]
        public new string address { get; set; }

        [Display(Name = "电话")]
        [StringLength(13)]
        public new string phone { get; set; }

        [Display(Name = "备注")]
        [StringLength(50)]
        public new string notes { get; set; }

        [Display(Name = "工单类型")]
        public new int? tasktype { get; set; }

        [Display(Name = "工单编号")]
        [StringLength(60)]
        public new string idtask { get; set; }
        
        [Display(Name = "启用")]
        public new bool? inuse { get; set; }

        [Display(Name = "用户")]
        /// <summary>
        /// 用户
        /// </summary>
        public TCom.EF.User user { get; set; }

        [Display(Name = "姓名")]
        public string UserName { get{
                return user == null ? "" : user.name;
            } }

        public void CopyFromBase(TCom.EF.Issue issue)
        {
            this.issue1 = issue.issue1;
            this.iduser = issue.iduser;
            this.context = issue.context;
            this.cretime = issue.cretime;
            this.booktime = issue.booktime;
            this.lng = issue.lng;
            this.lat = issue.lat;
            this.address = issue.address;
            this.phone = issue.phone;
            this.notes = issue.notes;
            this.tasktype = issue.tasktype;
            this.idtask = issue.idtask;
            this.inuse = issue.inuse;
        }

        public void CopyToBase(TCom.EF.Issue issue)
        {
            issue.issue1 = this.issue1;
            issue.iduser = this.iduser;
            issue.context = this.context;
            issue.cretime = this.cretime;
            issue.booktime = this.booktime;
            issue.lng = this.lng;
            issue.lat = this.lat;
            issue.address = this.address;
            issue.phone = this.phone;
            issue.notes = this.notes;
            issue.tasktype = this.tasktype;
            issue.idtask = this.idtask;
            issue.inuse = this.inuse;
        }
    }
}