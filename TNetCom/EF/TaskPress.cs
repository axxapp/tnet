namespace TCom.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TaskPress")]
    public partial class TaskPress
    {
        [Key]
        [StringLength(60)]
        public string idpress { get; set; }

        [Required]
        [StringLength(60)]
        public string idrecver { get; set; }

        [Required]
        [StringLength(60)]
        public string idtask { get; set; }

        public DateTime? cretime { get; set; }

        [StringLength(20)]
        public string ptext { get; set; }

        [StringLength(250)]
        public string pdesc { get; set; }

        public int? ptype { get; set; }

        public bool? inuse { get; set; }
    }
}
