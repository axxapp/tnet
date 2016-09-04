namespace TNet.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MyOrder")]
    public partial class MyOrder
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long orderno { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long iduser { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int idmerc { get; set; }

        [StringLength(60)]
        public string merc { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int idspec { get; set; }

        [StringLength(60)]
        public string spec { get; set; }

        public int? month { get; set; }

        public int? attmonth { get; set; }

        public double? price { get; set; }


        [StringLength(60)]
        public string contact { get; set; }
        

        [StringLength(100)]
        public string addr { get; set; }

        [StringLength(13)]
        public string phone { get; set; }

        public DateTime? cretime { get; set; }

        public DateTime? stime { get; set; }

        public DateTime? entime { get; set; }

        public int? otype { get; set; }

        public int? status { get; set; }

        [StringLength(50)]
        public string notes { get; set; }

        public bool? inuse { get; set; }
    }
}
