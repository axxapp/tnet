namespace TCom.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BussImage")]
    public partial class BussImage
    {
        [StringLength(60)]
        public string BussImageId { get; set; }

        [StringLength(60)]
        public string idbuss { get; set; }

        [StringLength(500)]
        public string Path { get; set; }

        public int? SortID { get; set; }

        public bool? InUse { get; set; }
    }
}
