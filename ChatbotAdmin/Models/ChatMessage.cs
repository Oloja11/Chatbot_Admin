using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatbotAdmin.Models
{
    public class ChatMessage
    {
        public long Id { get; set; }
        public string SenderEmail { get; set; }
        //public string Subject { get; set; }
        public string Content { get; set; }
        public DateTime ReceivedDate { get; set; }
        public bool IsRead { get; set; }
        public string SenderName { get; set; }


    }

    public class OutboxMessage : ChatMessage
    {
        public string To { get; set; }
        public string ReplyMessage { get; set; }
        public DateTime ReplyDate { get; set; }

        public static OutboxMessage InstanceOf (ChatMessage chatMessage)
        {
            OutboxMessage message = new();
            message.SenderEmail = chatMessage.SenderEmail;
            message.Content = chatMessage.Content;
            message.ReceivedDate = chatMessage.ReceivedDate;
            message.SenderName = chatMessage.SenderName;
            return message;
        }

    }
}
