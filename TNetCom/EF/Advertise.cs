namespace TCom.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Advertise")]
    public partial class Advertise
    {
        [Key]
        [StringLength(50)]
        public string idav { get; set; }

        [Required]
        [StringLength(50)]
        public string idat { get; set; }

        [StringLength(50)]
        public string title { get; set; }

        [Required]
        [StringLength(255)]
        public string img { get; set; }

        [StringLength(2550)]
        public string link { get; set; }

        public DateTime? cretime { get; set; }

        public int? sortno { get; set; }

        public bool inuse { get; set; }

        [StringLength(20)]
        public string city { get; set; }
    }
}
