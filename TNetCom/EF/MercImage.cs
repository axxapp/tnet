namespace TCom.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MercImage")]
    public partial class MercImage
    {
        [StringLength(60)]
        public string MercImageId { get; set; }

        [Required]
        [StringLength(60)]
        public string idmerc { get; set; }

        [StringLength(500)]
        public string Path { get; set; }

        public int? SortID { get; set; }

        public bool InUse { get; set; }
    }
}
