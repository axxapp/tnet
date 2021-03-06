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

        [Required]
        [StringLength(50)]
        public string code { get; set; }

        [Required]
        [StringLength(50)]
        public string name { get; set; }

        [Required]
        [StringLength(50)]
        public string title { get; set; }

        [StringLength(255)]
        public string ico { get; set; }

        public DateTime? cretime { get; set; }

        public DateTime? pubtime { get; set; }

        public DateTime? endtime { get; set; }

        [StringLength(50)]
        public string notes { get; set; }

        public bool inuse { get; set; }
    }
}
