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
    public   class MercImageViewModel:MercImage
    {
        [Display(Name ="图片编号")]
        [Required]
        public new  string MercImageId { get; set; }

        [Required]
        public new string idmerc { get; set; }

        [Display(Name ="产品图片")]
        [StringLength(500)]
        [Required]
        public new  string Path { get; set; }

        [Display(Name ="排序")]
        [Required]
        public new  int? SortID { get; set; }

        [Display(Name ="启用")]
        public new  bool InUse { get; set; }

        public   void CopyFromBase(MercImage mercImage)
        {
            this.MercImageId = mercImage.MercImageId;
            this.idmerc = mercImage.idmerc;
            this.Path = mercImage.Path;
            this.SortID = mercImage.SortID;
            this.InUse = mercImage.InUse;
            
        }

        public   void CopyToBase(MercImage mercImage)
        {
            mercImage.MercImageId = this.MercImageId;
            mercImage.idmerc = this.idmerc;
            mercImage.Path = this.Path;
            mercImage.SortID = this.SortID;
            mercImage.InUse = this.InUse;
        }
    }
}