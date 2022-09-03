using ChatbotAdmin.Models;
using ChatbotAdmin.Models.ViewModel;
using ChatbotAdmin.Repository;
using ChatbotAdmin.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ChatbotAdmin.Controllers
{
    [Authorize()]
    public class OutboxController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MessageService _messageService;

        public OutboxController(ILogger<HomeController> logger, MessageService messageService)
        {
            _logger = logger;
            _messageService = messageService;
        }
        // GET: ClubController1
        
        public ActionResult Index()
        {
            //get inbox
            //var userId = Int32.Parse( Request.HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value.Trim());

            var message = _messageService.GetOutboxMessages();
            message.ForEach(item =>
            {                
                if (item.Content.Length>40)
                {
                    item.Content = item.Content.Substring(0, 40) + " ...";
                }
                if (item.ReplyMessage.Length>40)
                {
                    item.ReplyMessage = item.ReplyMessage.Substring(0, 40) + " ...";
                }
                
            });
            return View(message);

        }

        
        public ActionResult MessageDetails( long id)
        {
            try
            {
                var message = _messageService.GetOutboxMessage(id);
                    return View(message);
                
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                _logger.LogError("Error create event controller {}", ex);
            }   
            return RedirectToAction(nameof(Index));
        }

        
        [HttpPost]
        public ActionResult DeleteOutboxMessage(long id)
        {
            try
            {
                var message = _messageService.GetOutboxMessage(id);
                if (message != null)
                {
                    _messageService.DeleteMessageFromOutbox(message.Id);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("error deleting outbox message {}", ex);
            }
            return RedirectToAction(nameof(Index));
        }
        
    }
}
