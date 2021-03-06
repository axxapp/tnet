namespace TCom.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Spec")]
    public partial class Spec
    {
        [Key]
        [StringLength(60)]
        public string idspec { get; set; }

        [Required]
        [StringLength(60)]
        public string idmerc { get; set; }

        [Column("spec")]
        [StringLength(60)]
        public string spec1 { get; set; }

        public double? price { get; set; }

        public int? sellcount { get; set; }

        public int? month { get; set; }

        public int? unit { get; set; }

        public int? up { get; set; }

        public int? down { get; set; }

        public int? attmonth { get; set; }

        public double? stuprice { get; set; }

        public double? moveprice { get; set; }

        [StringLength(50)]
        public string usertype { get; set; }

        public int? sortno { get; set; }

        [StringLength(100)]
        public string notes { get; set; }

        public bool inuse { get; set; }
    }
}
