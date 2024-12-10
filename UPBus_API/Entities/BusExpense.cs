using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UPBus_API.Entities
{
    public class BusExpense
    {
        [Key]
        [Column(TypeName = "varchar(15)")]
        public string ExpNo { get; set; } = null!;
        [Column(TypeName = "datetime")]
        public DateTime? ExpDate { get; set; }
        [Column(TypeName = "varchar(6)")]
        public string? ExpCode { get; set; }

        [Column(TypeName = "varchar(7)")]
        public string? BusNo { get; set; }

        [Column(TypeName = "float")]
        public float? Qty { get; set; }
        [Column(TypeName = "float")]
        public float? Price { get; set; }
        [Column(TypeName = "float")]
        public float? TotalAmt { get; set; }
       
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
