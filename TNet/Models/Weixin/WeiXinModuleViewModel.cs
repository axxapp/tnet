using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using TCom.EF;


namespace TNet.Models {
    [NotMapped]
    public class WeiXinModuleViewModel :WeiXinModule{
        [Display(Name ="模块编号")]
        [Required]
        [StringLength(50)]
        public new string idwxmodule { get; set; }

        [Display(Name = "代码")]
        [Required]
        [StringLength(50)]
        public new string code { get; set; }

        [Display(Name = "名称")]
        [Required]
        [StringLength(50)]
        public new string name { get; set; }
        
        [Display(Name = "标题")]
        [Required]
        [StringLength(50)]
        public new string title { get; set; }

        [Display(Name = "图标")]
        [StringLength(255)]
        public new string ico { get; set; }

        [Display(Name = "创建时间")]
        public new DateTime? cretime { get; set; }

        [Display(Name = "发布时间")]
        public new DateTime? pubtime { get; set; }

        [Display(Name = "结束时间")]
        public new DateTime? endtime { get; set; }

        [Display(Name = "备注")]
        [StringLength(50)]
        public new string notes { get; set; }

        [Display(Name = "启用")]
        public new bool inuse { get; set; }

        public void CopyFromBase(TCom.EF.WeiXinModule module) {
            this.idwxmodule = module.idwxmodule;
            this.code = module.code;
            this.name = module.name;
            this.title = module.title;
            this.ico = module.ico;
            this.cretime = module.cretime;
            this.pubtime = module.pubtime;
            this.endtime = module.endtime;
            this.notes = module.notes;
            this.inuse = module.inuse;
        }

        public void CopyToBase(TCom.EF.WeiXinModule module) {
            module.idwxmodule = this.idwxmodule;
            module.name = this.name;
            module.code = this.code;
            module.title = this.title;
            module.ico = this.ico;
            module.cretime = this.cretime;
            module.pubtime = this.pubtime;
            module.endtime = this.endtime;
            module.notes = this.notes;
            module.inuse = this.inuse;
        }
    }
}