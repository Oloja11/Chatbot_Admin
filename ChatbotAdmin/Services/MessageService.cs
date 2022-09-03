using ChatbotAdmin.Models;
using ChatbotAdmin.Models.ViewModel;
using ChatbotAdmin.Repository;
using ChatbotAdmin.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatbotAdmin.Services
{
    public class MessageService
    {
        private readonly ILogger<MessageService> _logger;
        private readonly EmailSenderService _emailSenderService;
        private readonly IMessageRepository _messageRepository;

        public MessageService(IMessageRepository messageRepository, ILogger<MessageService> logger, EmailSenderService emailSenderService)
        {
            _messageRepository = messageRepository;
            _logger = logger;
            _emailSenderService = emailSenderService;
        }

        
        //inbox
        public List< ChatMessage> GetInboxMessages()
        {
            try
            {
                return _messageRepository.GetInboxMessages();
            }
            catch (Exception ex)
            {
                _logger.LogError("error getting inbox", ex);
            }
            return new List<ChatMessage>();
        }        

        public ChatMessage GetInboxMessage(long messageId)
        {
            try
            {
                return _messageRepository.GetInboxMessage(messageId);
            }
            catch (Exception ex)
            {
                _logger.LogError("error fetching message by id", ex);
            }
            return null;
        }

        public bool DeleteMessageFromInbox(long messageId)
        {
            try
            {
                return _messageRepository.DeleteInboxMessage(messageId);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error deleting message {}", ex);
            }
            return false;
        }

        //outbox
        public List<OutboxMessage> GetOutboxMessages()
        {
            try
            {
                return _messageRepository.GetOutboxMessages();
            }
            catch (Exception ex)
            {
                _logger.LogError("error getting Outbox", ex);
            }
            return new List<OutboxMessage>();
        }

        public OutboxMessage GetOutboxMessage(long messageId)
        {
            try
            {
                return _messageRepository.GetOutboxMessage(messageId);
            }
            catch (Exception ex)
            {
                _logger.LogError("error fetching message by id", ex);
            }
            return null;
        }

        public bool DeleteMessageFromOutbox(long messageId)
        {
            try
            {
                return _messageRepository.DeleteOutboxMessage(messageId);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error deleting message {}", ex);
            }
            return false;
        }

        //reply chat

        public bool ReplyChat(OutboxMessage reply)
        {
            try
            {
                //var emailSent = _emailSenderService.Send(reply.SenderEmail, "Hullcity Inquiry", reply.ReplyMessage);
                var emailSent = true;
                if (emailSent)
                {
                    _messageRepository.SaveReplyToOutbox(reply);
                    return true;
                }

                return false;   
            }
            catch (Exception ex)
            {
                _logger.LogError("Error replying to message {}", ex);
            }
            return false;
        }

    }
}
