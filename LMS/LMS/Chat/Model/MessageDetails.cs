using System;

namespace LMS.Chat.Model
{
    public class MessageDetails
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public string SenderUsername { get; set; }
        public int ReceiverId { get; set; }
        public string ReceiverUsername { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
    }
}