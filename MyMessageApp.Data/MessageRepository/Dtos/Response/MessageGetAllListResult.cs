namespace MyMessageApp.Data.MessageRepository.Dtos.Response
{
    public class MessagesListAllResult
    {
        public int Id { get; set; }
        public int? ReceiverId { get; set; }
        public string? SenderEmail { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? Content { get; set; }
    }
}