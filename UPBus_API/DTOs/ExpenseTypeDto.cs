
namespace UPBus_API.DTOs
{
    public class ExpenseTypeDto
    {
        public string ExpCode { get; set; } = null!;
        public string? ExpName { get; set; }
        public string? ExpType { get; set; }
        public bool? Active { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedUser { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedUser { get; set; }
    }
}
