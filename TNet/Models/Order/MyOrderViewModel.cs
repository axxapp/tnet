﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TCom.EF;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace TNet.Models
{
    [NotMapped]
    public   class MyOrderViewModel:MyOrder
    {
        [Display(Name = "订单号")]
        [Required]
        public new  string orderno { get; set; }

        [Display(Name = "用户编号")]
        [Required]
        public new string iduser { get; set; }

        [Display(Name ="用户")]
        public string user_name { get; set; }

        [Display(Name = "产品")]
        [Required]
        public new string idmerc { get; set; }

        [Display(Name = "产品名称")]
        [Required]
        [StringLength(60)]
        public new  string merc { get; set; }

        [Display(Name = "规格id")]
        [Required]
        public new  string idspec { get; set; }

        [Display(Name = "规格")]
        [Required]
        [StringLength(60)]
        public new  string spec { get; set; }

        [Display(Name = "月份")]
        [Required]
        public new  int? month { get; set; }

        [Display(Name = "送的月份")]
        [Required]
        public new  int? attmonth { get; set; }

        [Display(Name = "数量")]
        [Required]
        public new  int count { get; set; }

        [Display(Name = "价格")]
        [Required]
        public new  double? price { get; set; }

        [Display(Name = "联系人")]
        [Required]
        [StringLength(50)]
        public new  string contact { get; set; }

        [Display(Name = "地址")]
        [Required]
        [StringLength(100)]
        public new  string addr { get; set; }

        [Display(Name = "手机号")]
        [Required]
        [StringLength(13)]
        public new  string phone { get; set; }

        [Display(Name = "下单时间")]
        [Required]
        public new  DateTime? cretime { get; set; }

        [Display(Name = "服务时间")]
        [Required]
        public new  DateTime? stime { get; set; }

        [Display(Name = "结服时间")]
        [Required]
        public new  DateTime? entime { get; set; }

        [Display(Name = "订单类型")]
        [Required]
        public new  int? otype { get; set; }

        [Display(Name = "订单状态")]
        [Required]
        public new  int? status { get; set; }

        [Display(Name = "图片")]
        [StringLength(255)]
        public new  string img { get; set; }

        [Display(Name = "备注")]
        [StringLength(50)]
        public new  string notes { get; set; }

        [Display(Name = "启用")]
        [Required]
        public new  bool inuse { get; set; }

        [Display(Name = "身份证")]
        [StringLength(20)]
        public new  string idc { get; set; }

        [Display(Name = "身份证正面")]
        [StringLength(255)]
        public new  string idc_img1 { get; set; }

        [Display(Name = "身份证反面")]
        [StringLength(255)]
        public new  string idc_img2 { get; set; }

        [Display(Name = "手持身份证照")]
        [StringLength(255)]
        public new string idc_img3 { get; set; }

        [Display(Name = "订单进度")]
        public List<MyOrderPressViewModel> OrderPresses { get; set; }

        public   void CopyFromBase(MyOrder order)
        {
            this.orderno = order.orderno;
            this.iduser = order.iduser;
            this.idmerc = order.idmerc;
            this.merc = order.merc;
            this.idspec = order.idspec;
            this.spec = order.spec;
            this.month = order.month;
            this.attmonth = order.attmonth;
            this.count = order.count;
            this.price = order.price;
            this.contact = order.contact;
            this.addr = order.addr;
            this.phone = order.phone;
            this.cretime = order.cretime;
            this.stime = order.stime;
            this.entime = order.entime;
            this.otype = order.otype;
            this.status = order.status;
            this.img = order.img;
            this.notes = order.notes;
            this.idc = order.idc;
            this.idc_img1 = order.idc;
            this.idc_img2 = order.idc_img2;
            this.idc_img3 = order.idc_img3;
            this.inuse = order.inuse;
        }

        public   void CopyToBase(MyOrder order)
        {
            order.orderno = this.orderno;
            order.iduser = this.iduser;
            order.idmerc = this.idmerc;
            order.merc = this.merc;
            order.idspec = this.idspec;
            order.spec = this.spec;
            order.month = this.month;
            order.attmonth = this.attmonth;
            order.count = this.count;
            order.price = this.price;
            order.contact = this.contact;
            order.addr = this.addr;
            order.phone = this.phone;
            order.cretime = this.cretime;
            order.stime = this.stime;
            order.entime = this.entime;
            order.otype = this.otype;
            order.status = this.status;
            order.img = this.img;
            order.notes = this.notes;
            order.idc = this.idc;
            order.idc_img1 = this.idc;
            order.idc_img2 = this.idc_img2;
            order.idc_img3 = this.idc_img3;
            order.inuse = this.inuse;
        }
    }
}