using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UPBus_API.Entities
{
    public class DailyGateExpense
    {
        [Key]
        [Column(TypeName = "varchar(15)")]
        public string ExpNo { get; set; } = null!;
        [Column(TypeName = "datetime")]
        public DateTime? ExpDate { get; set; }
        [Column(TypeName = "varchar(6)")]
        public string? ExpCode { get; set; }
        [Column(TypeName = "varchar(6)")]
        public string? PaidType { get; set; }
        [Column(TypeName = "float")]
        public float? Amount { get; set; }
        [Column(TypeName = "nvarchar(500)")]
        public string? Description { get; set; }
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
