namespace MyMessageApp.Data.MessageRepository.Dtos.Request
{
    public class MessageCreateRequest
    {
        public int? ReceiverId { get; set; }
        public string? Content { get; set; }
    }
}
