﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using TCom.EF;

namespace TNet.Models
{
    [NotMapped]
    public class CityViewModel:City
    {
        [Display(Name ="城市编号")]
        [StringLength(60)]
        public new string idcity { get; set; }

        [Display(Name = "城市")]
        [Required]
        [StringLength(60)]
        public new string city1 { get; set; }

        [Display(Name = "代码")]
        [StringLength(60)]
        [Required]
        public new string code { get; set; }

        [Display(Name = "备注")]
        [StringLength(60)]
        public new string notes { get; set; }

        [Display(Name = "排序")]
        public new int? sortno { get; set; }

        [Display(Name = "默认")]
        public new bool isdefault { get; set; }

        [Display(Name = "启用")]
        public new bool inuse { get; set; }

        /// <summary>
        /// 是否被选中
        /// </summary>
        public bool IsCheck { get; set; } 
        
        public void CopyFromBase(City city)
        {
            this.idcity = city.idcity;
            this.city1 = city.city1;
            this.code = city.code;
            this.notes = city.notes;
            this.sortno = city.sortno;
            this.inuse = city.inuse;
            this.isdefault = city.isdefault;
        }

        public void CopyToBase(City city)
        {
            city.idcity = this.idcity;
            city.city1 = this.city1;
            city.code = this.code;
            city.notes = this.notes;
            city.sortno = this.sortno;
            city.inuse = this.inuse;
            this.isdefault = city.isdefault;
        }
    }
}