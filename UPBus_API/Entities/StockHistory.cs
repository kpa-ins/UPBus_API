using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UPBus_API.Entities
{
    public class StockHistory
    {
        [Key]
        [Column(TypeName = "varchar(15)")]
        public string RegNo { get; set; } = null!;

        [Column(TypeName = "varchar(3)")]
        public string? StockCode { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? StockDate { get; set; }
        [Column(TypeName = "varchar(8)")]
        public string? StockType { get; set; }
        [Column(TypeName = "float")]
        public float? Qty { get; set; }
        [Column(TypeName = "float")]
        public float? LastPrice { get; set; }
        [Column(TypeName = "float")]
        public float? TotalAmt { get; set; }
        [Column(TypeName = "varchar(7)")]
        public string? BusNo { get; set; }
        [Column(TypeName = "bit")]
        public bool? IsCancel { get; set; }
        [Column(TypeName = "nvarchar(500)")]
        public string? Remark { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string? CreatedUser { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string? UpdatedUser { get; set; }
    }
}
