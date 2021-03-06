﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TCom.EF;
using TNet.Util;

namespace TNet.Models
{
    [NotMapped]
    public class BusinessViewModel:TCom.EF.Business
    {
        [Display(Name = "商家ID")]
        [Required]
        public new string idbuss { get; set; }

        [Display(Name = "商家")]
        [Required]
        [StringLength(60)]
        public new string buss { get; set; }

        [Display(Name = "联系人")]
        [Required]
        [StringLength(50)]
        public new string contact { get; set; }

        [Display(Name = "手机号")]
        [Required]
        [StringLength(13)]
        public new string phone { get; set; }

        [Display(Name = "城市")]
        [Required]
        [StringLength(60)]
        public new string city { get; set; }

        [Display(Name = "城市代码")]
        [Required]
        [StringLength(60)]
        public new string citycode { get; set; }
        
        /// <summary>
        /// 城市_城市代码
        /// </summary>
        [Display(Name = "城市")]
        [Required]
        public string cityselectvalue { get; set; }
        

        [Display(Name = "地址")]
        [Required]
        [StringLength(100)]
        public new string addr { get; set; }

        [Display(Name = "卖点")]
        [Required]
        [StringLength(60)]
        public new string sellpt { get; set; }

        [Display(Name = "图片")]
        [StringLength(255)]
        public new string imgs { get; set; }

        [Display(Name = "创建时间")]
        [Required]
        public new DateTime? cretime { get; set; }

        [Display(Name = "成立时间")]
        [Required]
        public new DateTime? busstime { get; set; }

        [Display(Name = "价格")]
        [Required]
        public new double? price { get; set; }

        [Display(Name = "经度")]
        [Required]
        public new double? longitude { get; set; }

        [Display(Name = "纬度")]
        [Required]
        public new double? latitude { get; set; }

        [Display(Name = "营业时间")]
        [Required]
        [StringLength(50)]
        public new string runtime { get; set; }

        [Display(Name = "备注")]
        [StringLength(350)]
        public new string notes { get; set; }

        [Display(Name = "等级1-5")]
        [Required]
        public new int? blevel { get; set; }

        [Display(Name = "启用")]
        [Required]
        public new bool inuse { get; set; }

        [Display(Name = "排序")]
        public new int? sortno { get; set; }

        [CheckBoxRequiredValidation]
        [Display(Name = "城市")]
        public string[] idcitys { get; set; }

        public void CopyFromBase(TCom.EF.Business business)
        {
            this.idbuss = business.idbuss;
            this.buss = business.buss;
            this.contact = business.contact;
            this.phone = business.phone;
            this.city = business.city;
            this.citycode = business.citycode;
            this.addr = business.addr;
            this.sellpt = business.sellpt;
            this.cretime = business.cretime;
            this.busstime = business.busstime;
            this.price = business.price;
            this.longitude = business.longitude;
            this.latitude = business.latitude;
            this.runtime = business.runtime;
            this.notes = business.notes;
            this.imgs= business.imgs;
            this.blevel = business.blevel;
            this.inuse = business.inuse;
        }

        public void CopyToBase(TCom.EF.Business business)
        {
            business.idbuss = this.idbuss;
            business.buss = this.buss;
            business.contact = this.contact;
            business.phone = this.phone;
            business.city = this.city;
            business.citycode = this.citycode;
            business.addr = this.addr;
            business.sellpt = this.sellpt;
            business.cretime = this.cretime;
            business.busstime = this.busstime;
            business.price = this.price;
            business.longitude = this.longitude;
            business.latitude = this.latitude;
            business.runtime = this.runtime;
            business.notes = this.notes;
            business.imgs = this.imgs;
            business.blevel = this.blevel;
            business.inuse = this.inuse;
        }
    }
}