
using ChatbotAdmin.Models;
using ChatbotAdmin.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace ChatbotAdmin.Controllers
{
    [Authorize()]
    public class InboxController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MessageService _messageService;

        public InboxController(ILogger<HomeController> logger, MessageService messageService)
        {
            _logger = logger;
            _messageService = messageService;
        }
        // GET: ClubController1
        public ActionResult Index()
        {
            //get inbox
            //var userId = Int32.Parse( Request.HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value.Trim());

            var inboxMessage = _messageService.GetInboxMessages();
            inboxMessage.ForEach(item => {
                if (item.Content.Length>40)
                {
                    item.Content = item.Content.Substring(0, 40) + " ...";
                }
            });
            return View(inboxMessage);

        }

        
        public ActionResult MessageDetails( long id)
        {
            try
            {
                var message = _messageService.GetInboxMessage(id);
                    return View(message);
                
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                _logger.LogError("Error create event controller {}", ex);
            }   
            return RedirectToAction(nameof(Index));
        }

        public ActionResult ReplyChat (long id)
        {
            try
            {
                var message = _messageService.GetInboxMessage(id);
                if (message != null)
                {
                    OutboxMessage outbox = OutboxMessage.InstanceOf(message);

                    return View(outbox);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("error replying message {}", ex);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public ActionResult ReplyChat(OutboxMessage message)
        {           
            try
            {
                if (ModelState.IsValid)
                {
                    var replied = _messageService.ReplyChat(message);
                    if (replied)
                    {
                        ViewData["Info"] = "Event created successfully";
                        return RedirectToAction(nameof(Index));
                    }
                    ModelState.AddModelError("", "Unable to create event.");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(message);
        }


        [HttpPost]
        public ActionResult DeleteInboxMessage(long id)
        {
            try
            {
                var message = _messageService.GetInboxMessage(id);
                if (message != null)
                {
                    _messageService.DeleteMessageFromInbox(message.Id);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("error deleting inbox message {}", ex);
            }
            return RedirectToAction(nameof(Index));
        }
        
    }
}
