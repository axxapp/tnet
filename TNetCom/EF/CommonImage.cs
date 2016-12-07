namespace TCom.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CommonImage")]
    public partial class CommonImage
    {
        [Key]
        [StringLength(50)]
        public string idimg { get; set; }

        [StringLength(1000)]
        public string path { get; set; }

        [Required]
        [StringLength(50)]
        public string idmodule { get; set; }

        public int moduletype { get; set; }

        public int? sortno { get; set; }

        public DateTime? cretime { get; set; }
    }
}
