using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UPBus_API.Entities
{
    public class Gate
    {
        [Key]
        [Column(TypeName = "varchar(4)")]
        public string GateCode { get; set; } = null!;
        [Column(TypeName = "nvarchar(150)")]
        public string? GateName { get; set; }
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
