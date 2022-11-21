namespace Data.MyMessageApp.Data.MessageRepositories.Dtos.Request
{
    public class MessageCreateDto
    {
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string Content { get; set; }
    }
}
