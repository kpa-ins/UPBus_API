using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UPBus_API.Entities
{
    public class TripType
    {
        [Key]
        [Column(TypeName = "varchar(3)")]
        public string TripCode { get; set; } = null!;
        [Column(TypeName = "varchar(50)")]
        public string? TripName { get; set; }
        [Column(TypeName = "varchar(4)")]
        public string? TrpType { get; set; }
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
