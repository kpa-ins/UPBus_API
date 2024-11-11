namespace UPBus_API.DTOs
{
    public class ResponseMessage
    {
        public bool Status { get; set; }
        public string? Message { get; set; }
        public string? MessageContent { get; set; }
    }
}
