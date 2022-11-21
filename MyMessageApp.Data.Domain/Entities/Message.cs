using System;
using System.Collections.Generic;

namespace MyMessageApp.Data.Domain.Entities
{
    public partial class Message
    {
        public int Id { get; set; }
        public int? SenderId { get; set; }
        public int? ReceiverId { get; set; }
        public string? Content { get; set; }
        public DateTime? Date { get; set; }

        public virtual User? Receiver { get; set; }
        public virtual User? Sender { get; set; }
    }
}
