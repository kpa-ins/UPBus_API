using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UPBus_API.Entities
{
    public class DailyPlan
    {
        [Key]
        [Column(TypeName = "varchar(8)")]
        public string RegNo { get; set; } = null!;
        [Column(TypeName = "varchar(15)")]
        public string? TrackCode { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? TripDate { get; set; }
        [Column(TypeName = "nvarchar(5)")]
        public string? TripTime { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string? Track { get; set; }
        [Column(TypeName = "varchar(7)")]
        public string? BusNo { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string? DriverName { get; set; }

        [Column(TypeName = "varchar(4)")]
        public string? TrackType { get; set; }

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
