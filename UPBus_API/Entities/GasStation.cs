using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UPBus_API.Entities
{
    public class GasStation
    {
        [Key]
        [Column(TypeName = "varchar(5)")]
        public string GSCode { get; set; } = null!;
        [Column(TypeName = "nvarchar(200)")]
        public string? GSName { get; set; }
        [Column(TypeName = "nvarchar(500)")]
        public string? Location { get; set; }
        [Column(TypeName = "float")]
        public float? TotalBalance { get; set; }
        [Column(TypeName = "varchar(5)")]
        public string? Unit { get; set; }
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
