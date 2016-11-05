namespace TCom.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("WeiXinModule")]
    public partial class WeiXinModule
    {
        [Key]
        [StringLength(50)]
        public string idwxmodule { get; set; }

        [StringLength(50)]
        public string name { get; set; }

        [StringLength(50)]
        public string code { get; set; }

        [StringLength(50)]
        public string title { get; set; }

        public bool inuse { get; set; }
    }
}
