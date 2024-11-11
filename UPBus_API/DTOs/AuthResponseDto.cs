namespace UPBus_API.DTOs
{
    public class AuthResponseDto
    {
        public bool IsAuthSuccessful { get; set; }
        public string? ErrorMessage { get; set; }
        public string? Token { get; set; }
        public String? YardList { get; set; }
        public String? OwnerList { get; set; }
        public String? MenuList { get; set; }
    }
}
