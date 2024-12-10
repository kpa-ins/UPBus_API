
namespace UPBus_API.DTOs
{
    public class StockMainDto
    {
        public string StockCode { get; set; } = null!;
        public string? StockName { get; set; }
        public float? Balance { get; set; }
        public float? LastPrice { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedUser { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedUser { get; set; }
    }
}
