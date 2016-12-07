using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TCom.EF;

namespace TNet.Models.Image
{
    public class CommonImageViewModel:CommonImage
    {
        [Display(Name ="图片编号")]
        [StringLength(50)]
        public new string idimg { get; set; }

        [Display(Name = "图片")]
        [StringLength(1000)]
        public new string path { get; set; }

        [Display(Name = "模块编号")]
        [StringLength(50)]
        public new string idmodule { get; set; }

        [Display(Name = "模块类型")]
        public new int moduletype { get; set; }

        [Display(Name = "排序")]
        public new int? sortno { get; set; }

        [Display(Name = "创建时间")]
        public new DateTime? cretime { get; set; }
    }
}