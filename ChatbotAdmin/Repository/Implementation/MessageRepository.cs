using ChatbotAdmin.Models;
using ChatbotAdmin.Models.ViewModel;
using ChatbotAdmin.Repository;
using ChatbotAdmin.Repository.Implementation;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ChatbotAdmin.Repository.Implementation
{
    public class MessageRepository : IMessageRepository
    {
        private readonly IConfiguration configuration;
        private readonly ILogger<LoginManager> logger;

        public MessageRepository(IConfiguration configuration, ILogger<LoginManager> logger)
        {
            this.configuration = configuration;
            this.logger = logger;
        }
        

        public ChatMessage GetInboxMessage(long id)
        {
            try
            {
                using (var conn = new SqlConnection(configuration.GetConnectionString("ChatAdminDatabase")))
                {
                    conn.Open();
                    var message = conn.Query<ChatMessage>("get_inbox_message_by_id", new { id }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                    return message;
                }
            }
            catch (Exception ex)
            {
                logger.LogError("Error getting club by id", ex);
            }
            return null;
        }

        public List<ChatMessage> GetInboxMessages()
        {
            try
            {
                using (var conn = new SqlConnection(configuration.GetConnectionString("ChatAdminDatabase")))
                {
                    conn.Open();
                    var messages = conn.Query<ChatMessage>("get_inbox_messages", new { }, commandType: CommandType.StoredProcedure).ToList();
                    return messages;
                }
            }
            catch (Exception ex)
            {
                logger.LogError("Error getting inbox messages", ex);
            }
            return new List<ChatMessage>();
        }

        public bool DeleteInboxMessage(long id)
        {
            try
            {
                using (var conn = new SqlConnection(configuration.GetConnectionString("ChatAdminDatabase")))
                {
                    conn.Open();
                    var row = conn.Execute("delete_inbox_message", new { id }, commandType: CommandType.StoredProcedure);
                    if (row > 0)
                    {
                        logger.LogInformation("category updated");

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError("Error updating club category {}", ex);
            }
            return false;
        }

        public OutboxMessage GetOutboxMessage(long id)
        {
            try
            {
                using (var conn = new SqlConnection(configuration.GetConnectionString("ChatAdminDatabase")))
                {
                    conn.Open();
                    var club = conn.Query<OutboxMessage>("get_outbox_message_by_id", new { id }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                    return club;
                }
            }
            catch (Exception ex)
            {
                logger.LogError("Error getting club by id", ex);
            }
            return null;
        }

        public List<OutboxMessage> GetOutboxMessages()
        {
            try
            {
                using (var conn = new SqlConnection(configuration.GetConnectionString("ChatAdminDatabase")))
                {
                    conn.Open();
                    var clubs = conn.Query<OutboxMessage>("get_outbox_messages", new { }, commandType: CommandType.StoredProcedure).ToList();
                    return clubs;
                }
            }
            catch (Exception ex)
            {
                logger.LogError("Error getting club subscriber", ex);
            }
            return new List<OutboxMessage>();
        }

        public bool DeleteOutboxMessage(long id)
        {
            try
            {
                
                using (var conn = new SqlConnection(configuration.GetConnectionString("ChatAdminDatabase")))
                {
                    conn.Open();
                    var row = conn.Execute("delete_outbox_message", new { id }, commandType: CommandType.StoredProcedure);
                    if (row > 0)
                    {
                        logger.LogInformation("outbox message deleted");

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError("Error updating club category {}", ex);
            }
            return false;
        }

        public OutboxMessage SaveReplyToOutbox(OutboxMessage reply)
        {
            try
            {
                logger.LogInformation("about to save outbox message {}", reply);
                using (var conn = new SqlConnection(configuration.GetConnectionString("ChatAdminDatabase")))
                {
                    conn.Open();
                    var id = conn.ExecuteScalar<int>("create_outbox", 
                        new { 
                            From = reply.SenderEmail,
                            reply.SenderName,
                            reply.Content,
                            reply.ReplyMessage,
                            reply.ReceivedDate
                        }, 
                        commandType: System.Data.CommandType.StoredProcedure);
                    if (id > 0)
                    {
                        logger.LogInformation("logger created ID: {}", id);
                        reply.Id = id;
                        return reply;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError("Error creating club", ex);
            }

            return null;
        }
    }
}
