using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UPBus_API.Entities
{
    public class ExpenseType
    {
        [Key]
        [Column(TypeName = "varchar(5)")]
        public string ExpCode { get; set; } = null!;
        [Column(TypeName = "nvarchar(500)")]
        public string? ExpName { get; set; }
        [Column(TypeName = "varchar(15)")]
        public string? ExpType { get; set; }
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
