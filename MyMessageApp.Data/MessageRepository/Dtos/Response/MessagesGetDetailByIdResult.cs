namespace MyMessageApp.Data.MessageRepository.Dtos.Response
{
    public class MessagesGetDetailByIdResult
    {
        public int Id { get; set; }
        public string SenderEmail { get; set; }
        public string ReceiverEmail { get; set; }
        public string Content { get; set; }
    }
}