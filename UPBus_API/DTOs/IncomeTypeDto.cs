
namespace UPBus_API.DTOs
{
    public class IncomeTypeDto
    {
        public string IncCode { get; set; } = null!;
        public string? IncName { get; set; }
        public string? IncType { get; set; }
        public bool? Active { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedUser { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedUser { get; set; }
    }
}
