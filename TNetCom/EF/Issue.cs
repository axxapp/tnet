namespace TCom.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Issue
    {
        [Key]
        [Column("issue")]
        [StringLength(60)]
        public string issue1 { get; set; }

        [Required]
        [StringLength(60)]
        public string iduser { get; set; }

        [StringLength(200)]
        public string context { get; set; }

        public DateTime? cretime { get; set; }

        public DateTime? booktime { get; set; }

        public double? lng { get; set; }

        public double? lat { get; set; }

        [StringLength(180)]
        public string address { get; set; }

        [StringLength(13)]
        public string phone { get; set; }

        [StringLength(50)]
        public string notes { get; set; }

        public int? tasktype { get; set; }

        [StringLength(60)]
        public string idtask { get; set; }

        public bool? inuse { get; set; }
    }
}
