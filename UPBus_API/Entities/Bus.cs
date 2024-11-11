using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UPBus_API.Entities
{
    public class Bus
    {
        [Key]
        [Column(TypeName = "varchar(7)")]
        public string BusNo { get; set; } = null!;
        [Column(TypeName = "nvarchar(50)")]
        public string? DriverName { get; set; }
        [Column(TypeName = "int")]
        public int? NoOfSeat { get; set; } 
        
        [Column(TypeName = "varchar(5)")]
        public string? Owner { get; set; }
        [Column(TypeName = "bit")]
        public bool? Active { get; set; }
        [Column(TypeName = "nvarchar(150)")]
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
