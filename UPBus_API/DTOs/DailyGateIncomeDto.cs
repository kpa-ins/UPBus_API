
namespace UPBus_API.DTOs
{
    public class DailyGateIncomeDto
    {
        public string IncNo { get; set; } = null!;
        public DateTime? IncDate { get; set; }
        public string? IncCode { get; set; }
        public string? PaidType { get; set; }
        public float? Amount { get; set; }

        public string? Description { get; set; }
        public string? Remark { get; set; }
        public string? GateAccCode { get; set; }
        public string? AccDate { get; set; }
        public string? GateCode { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedUser { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedUser { get; set; }
    }
}
