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
    public  class SpecViewModel:Spec
    {
        [Display(Name = "产品编号")]
        public new string idmerc { get; set; }

        [Display(Name = "产品名称")]
        public string merc { get; set; }

        [Display(Name = "规格号")]
        public new  string idspec { get; set; }

        [Display(Name = "规格")]
        [StringLength(60)]
        public new  string spec1 { get; set; }

        [Display(Name = "单价")]
        public new  double? price { get; set; }

        [Display(Name = "销量")]
        public new  int? sellcount { get; set; }

        [Display(Name = "月数")]
        public new  int? month { get; set; }

        [Display(Name = "单位/月")]
        public new  int? unit { get; set; }

        [Display(Name = "上行")]
        public new  int? up { get; set; }

        [Display(Name = "下行")]
        public new  int? down { get; set; }

        [Display(Name = "附送月份")]
        public new  int? attmonth { get; set; }

        [Display(Name = "初装费")]
        public new  double? stuprice { get; set; }

        [Display(Name = "移机费")]
        public new  double? moveprice { get; set; }

        [Display(Name = "产品类型")]
        [StringLength(50)]
        public new  string usertype { get; set; }

        [Display(Name = "备注")]
        [StringLength(100)]
        public new  string notes { get; set; }

        [Display(Name = "启用")]
        public new  bool inuse { get; set; }

        [Display(Name = "排序")]
        public new int? sortno { get; set; }

        public List<MercTypeViewModel> mercTypes { get; set; }

        public static List<SelectItemViewModel<string>> GetUserTypeSelectItems()
        {
            List<SelectItemViewModel<string>> list = new List<SelectItemViewModel<string>>();
            list.Add(new SelectItemViewModel<string>()
            {
                DisplayText = "家庭",
                DisplayValue = "家庭"
            });
            list.Add(new SelectItemViewModel<string>()
            {
                DisplayText = "企业",
                DisplayValue = "企业"
            });
            return list;
        }

        public   void CopyFromBase(Spec spec)
        {
            this.idmerc = spec.idmerc;
            this.idspec = spec.idspec;
            this.spec1 = spec.spec1;
            this.price = spec.price;
            this.sellcount = spec.sellcount;
            this.month = spec.month;
            this.unit = spec.unit;
            this.up = spec.up;
            this.down = spec.down;
            this.attmonth = spec.attmonth;
            this.stuprice = spec.stuprice;
            this.moveprice = spec.moveprice;
            this.usertype = spec.usertype;
            this.notes = spec.notes;
            this.inuse = spec.inuse;
        }

        public   void CopyToBase(Spec spec)
        {
            spec.idmerc = this.idmerc;
            spec.idspec = this.idspec;
            spec.spec1 = this.spec1;
            spec.price = this.price;
            spec.sellcount = this.sellcount;
            spec.month = this.month;
            spec.unit = this.unit;
            spec.up = this.up;
            spec.down = this.down;
            spec.attmonth = this.attmonth;
            spec.stuprice = this.stuprice;
            spec.moveprice = this.moveprice;
            spec.usertype = this.usertype;
            spec.notes = this.notes;
            spec.inuse = this.inuse;
        }
    }
}