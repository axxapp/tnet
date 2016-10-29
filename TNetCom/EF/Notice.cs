namespace TCom.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Notice")]
    public partial class Notice
    {
        [Key]
        [StringLength(100)]
        public string idnotice { get; set; }

        [Required]
        [StringLength(50)]
        public string publish { get; set; }

        [Required]
        [StringLength(100)]
        public string title { get; set; }

        public DateTime? publish_time { get; set; }

        public DateTime? start_time { get; set; }

        public DateTime? end_time { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string content { get; set; }

        public int? sortno { get; set; }
    }
}
