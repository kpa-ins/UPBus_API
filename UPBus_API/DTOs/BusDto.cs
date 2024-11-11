
namespace UPBus_API.DTOs
{
    public class BusDto
    {
        public string BusNo { get; set; } = null!;
        public string? DriverName { get; set; }
        public int? NoOfSeat { get; set; }
        public string? Owner { get; set; }
        public bool? Active { get; set; }
        public string? Remark { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedUser { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedUser { get; set; }
    }
}
