using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UPBus_API.Entities
{
    public class DailyGateIncome
    {
        [Key]
        [Column(TypeName = "varchar(15)")]
        public string IncNo { get; set; } = null!;
        [Column(TypeName = "datetime")]
        public DateTime? IncDate { get; set; }
        [Column(TypeName = "varchar(6)")]
        public string? IncCode { get; set; }
        [Column(TypeName = "varchar(6)")]
        public string? PaidType { get; set; }
        [Column(TypeName = "float")]
        public float? Amount { get; set; }
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
