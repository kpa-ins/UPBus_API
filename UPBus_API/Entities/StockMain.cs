using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UPBus_API.Entities
{
    public class StockMain
    {
        [Key]
        [Column(TypeName = "varchar(3)")]
        public string StockCode { get; set; } = null!;

        [Column(TypeName = "nvarchar(150)")]
        public string? StockName { get; set; }
        [Column(TypeName = "float")]
        public float? Balance { get; set; }
        [Column(TypeName = "float")]
        public float? LastPrice { get; set; }
       
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
