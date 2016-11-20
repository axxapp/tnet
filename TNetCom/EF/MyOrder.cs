namespace TCom.EF
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
        [StringLength(60)]
        public string orderno { get; set; }

        [Required]
        [StringLength(60)]
        public string iduser { get; set; }

        [Required]
        [StringLength(60)]
        public string idmerc { get; set; }

        [StringLength(60)]
        public string merc { get; set; }

        [Required]
        [StringLength(60)]
        public string idspec { get; set; }

        [StringLength(60)]
        public string spec { get; set; }

        [StringLength(60)]
        public string idtask { get; set; }

        public int? month { get; set; }

        public int? attmonth { get; set; }

        public int count { get; set; }

        public double? price { get; set; }

        public double? totalfee { get; set; }

        [StringLength(50)]
        public string contact { get; set; }

        [StringLength(100)]
        public string addr { get; set; }

        [StringLength(13)]
        public string phone { get; set; }

        public DateTime? cretime { get; set; }

        public DateTime? stime { get; set; }

        public DateTime? entime { get; set; }

        public int? otype { get; set; }

        [StringLength(10)]
        public string payway { get; set; }

        public int? status { get; set; }

        [StringLength(50)]
        public string paystatus { get; set; }

        [StringLength(255)]
        public string img { get; set; }

        [StringLength(50)]
        public string notes { get; set; }

        [StringLength(20)]
        public string idc { get; set; }

        [StringLength(255)]
        public string idc_img1 { get; set; }

        [StringLength(255)]
        public string idc_img2 { get; set; }

        [StringLength(255)]
        public string idc_img3 { get; set; }

        public bool inuse { get; set; }
    }
}
