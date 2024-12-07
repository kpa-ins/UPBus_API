using System.ComponentModel.DataAnnotations.Schema;

namespace UPBus_API.DTOs
{
    public class DailyGateAccDto
    {
        public string GateCode { get; set; } = null!;
        public DateTime? AccDate { get; set; } = null!;
        public float? IncTotalAmt { get; set; }
        public float? ExpTotalAmt { get; set; }
        public float? IncCreditAmt { get; set; }
        public float? IncReceiveAmt { get; set; }
        public string? Remark { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedUser { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedUser { get; set; }
    }
}
