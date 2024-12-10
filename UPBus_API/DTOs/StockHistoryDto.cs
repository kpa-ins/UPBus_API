using System.ComponentModel.DataAnnotations.Schema;

namespace UPBus_API.DTOs
{
    public class StockHistoryDto
    {    
        public string RegNo { get; set; } = null!;
        public string? StockCode { get; set; }
        public DateTime? StockDate { get; set; }
        public string? StockType { get; set; }
        public float? Qty { get; set; }
        public float? LastPrice { get; set; }
        public float? TotalAmt { get; set; }
        public string? BusNo { get; set; }
        public string? Remark { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedUser { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedUser { get; set; }
    }
}
