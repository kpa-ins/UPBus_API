using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UPBus_API.Entities
{
    public class IncomeType
    {
        [Key]
        [Column(TypeName = "varchar(5)")]
        public string IncCode { get; set; } = null!;
        [Column(TypeName = "nvarchar(500)")]
        public string? IncName { get; set; }
        [Column(TypeName = "varchar(15)")]
        public string? IncType { get; set; }
        [Column(TypeName = "bit")]
        public bool? Active { get; set; }
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
