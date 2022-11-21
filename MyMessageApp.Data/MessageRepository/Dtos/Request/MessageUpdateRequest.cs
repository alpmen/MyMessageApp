namespace MyMessageApp.Data.MessageRepository.Dtos.Request
{
    public class MessageUpdateRequest
    {
        public int Id { get; set; }
        public int? ReceiverId { get; set; }
        public string? Content { get; set; }
    }
}