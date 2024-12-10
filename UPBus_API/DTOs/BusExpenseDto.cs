using System.ComponentModel.DataAnnotations.Schema;

namespace UPBus_API.DTOs
{
    public class BusExpenseDto
    {
        public string ExpNo { get; set; } = null!;
        public DateTime? ExpDate { get; set; }
        public string? ExpCode { get; set; }
        public string? BusNo { get; set; }
        public float? Qty { get; set; }
        public float? Price { get; set; }
        public float? TotalAmt { get; set; }
        public string? Remark { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedUser { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedUser { get; set; }
    }
}
