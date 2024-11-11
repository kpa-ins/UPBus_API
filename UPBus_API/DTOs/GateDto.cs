using System.ComponentModel.DataAnnotations.Schema;

namespace UPBus_API.DTOs
{
    public class GateDto
    {
        public string GateCode { get; set; } = null!;
        public string? GateName { get; set; }
        public bool? Active { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedUser { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedUser { get; set; }
    }
}
