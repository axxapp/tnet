namespace TCom.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TaskMonitor")]
    public partial class TaskMonitor
    {
        [Key]
        [StringLength(50)]
        public string idmonitor { get; set; }

        [Required]
        [StringLength(60)]
        public string idtask { get; set; }

        [StringLength(50)]
        public string mgcode { get; set; }

        [Required]
        [StringLength(50)]
        public string rmgcode { get; set; }

        public DateTime? cretime { get; set; }

        public DateTime? review { get; set; }

        public bool? finish { get; set; }

        public bool? inuse { get; set; }
    }
}
