
namespace UPBus_API.DTOs
{
    public class DailyPlanDto
    {
        public string RegNo { get; set; }
        public string? TrackCode { get; set; }
        public DateTime? TripDate { get; set; }
        public string? TripTime { get; set; }
        public string? Track { get; set; }
        public string? TrackType { get; set; }
        public string? BusNo { get; set; }
        public string? DriverName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedUser { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedUser { get; set; }
    }
}
