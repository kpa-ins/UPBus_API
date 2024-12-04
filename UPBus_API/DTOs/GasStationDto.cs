
namespace UPBus_API.DTOs
{
    public class GasStationDto
    {
        public string GSCode { get; set; } = null!;
        public string? GSName { get; set; }
        public string? Location { get; set; }
        public float? TotalBalance { get; set; }
        public string? Unit { get; set; }
        public bool? Active { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedUser { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedUser { get; set; }
    }
}
