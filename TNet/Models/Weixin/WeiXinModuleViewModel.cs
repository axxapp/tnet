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

        [Display(Name = "名称")]
        [Required]
        [StringLength(50)]
        public new string name { get; set; }

        [Display(Name = "代码")]
        [Required]
        [StringLength(50)]
        public new string code { get; set; }

        [Display(Name = "标题")]
        [Required]
        [StringLength(50)]
        public new string title { get; set; }

        [Display(Name = "启用")]
        public new bool inuse { get; set; }

        public void CopyFromBase(TCom.EF.WeiXinModule module) {
            this.idwxmodule = module.idwxmodule;
            this.code = module.code;
            this.name = module.name;
            this.title = module.title;
            this.inuse = module.inuse;
        }

        public void CopyToBase(TCom.EF.WeiXinModule module) {
            module.idwxmodule = this.idwxmodule;
            module.name = this.name;
            module.code = this.code;
            module.title = this.title;
            module.inuse = this.inuse;
        }
    }
}