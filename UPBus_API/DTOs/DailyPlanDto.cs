
namespace UPBus_API.DTOs
{
    public class DailyPlanDto
    {
        public int DailyPlanID { get; set; }
        public string? TripCode { get; set; }
        public DateTime? TripDate { get; set; }
        public string? TripTime { get; set; }
        public string? Track { get; set; }
        public string? BusNo { get; set; }
        public string? DriverName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedUser { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedUser { get; set; }
    }
}
