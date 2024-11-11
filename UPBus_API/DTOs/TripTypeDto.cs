
namespace UPBus_API.DTOs
{
    public class TripTypeDto
    {
        public string TripCode { get; set; } = null!;
        public string? TripName { get; set; }
        public string? TrpType { get; set; }
        public bool? Active { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedUser { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedUser { get; set; }
    }
}
