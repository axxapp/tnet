namespace TCom.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Discount")]
    public partial class Discount
    {
        [Required]
        [StringLength(60)]
        public string idmerc { get; set; }

        [Required]
        [StringLength(60)]
        public string idspec { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int iddisc { get; set; }

        public int? month { get; set; }

        [Column("discount")]
        public double? discount1 { get; set; }

        [StringLength(50)]
        public string notes { get; set; }

        public bool? inuse { get; set; }
    }
}
