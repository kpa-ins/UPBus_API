using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UPBus_API.Entities
{
    public class DailyPlan
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DailyPlanID { get; set; }
        [Column(TypeName = "varchar(3)")]
        public string? TripCode { get; set; } 
        [Column(TypeName = "datetime")]
        public DateTime? TripDate { get; set; }
        [Column(TypeName = "nvarchar(5)")]
        public string? TripTime { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string? Track { get; set; }
        [Column(TypeName = "varchar(7)")] 
        public string? BusNo { get; set; }
        [Column(TypeName = "varchar(7)")] 
        public string? DriverName { get; set; }
      
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
