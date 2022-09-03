using ChatbotAdmin.Models;
using ChatbotAdmin.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatbotAdmin.Repository
{
    public interface IMessageRepository
    {
        ChatMessage GetInboxMessage(long id);
        List<ChatMessage> GetInboxMessages();
        bool DeleteInboxMessage(long id);


        OutboxMessage GetOutboxMessage(long id);
        List<OutboxMessage> GetOutboxMessages();
        bool DeleteOutboxMessage(long id);
        OutboxMessage SaveReplyToOutbox(OutboxMessage reply);
    }
}
