using BreadCommunityWeb.EventNotificator.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BreadCommunityWeb.EventNotificator.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TelegramServiceController : ControllerBase
    {
        private readonly IMessangerService _messangerService;
        public TelegramServiceController(IMessangerService messangerService)
        {
            _messangerService = messangerService;
        }

        /// <summary>
        /// Отправить сообщение в чат
        /// </summary>
        /// <param name="message"></param>
        /// <param name="chatId"></param>
        /// <returns></returns>
        [HttpPost(nameof(SendMessageToChat))]
        public async Task<ActionResult> SendMessageToChat(string message,string chatId)
        {
            long.TryParse(chatId, out long convertedChatId);
            if(convertedChatId == null)
            {
                throw new Exception("Can't parse chat");
            }
            await _messangerService.SendMessage(convertedChatId, message);

            return Ok();
        }

        /// <summary>
        /// Отправить сообщение в несколько чатов
        /// </summary>
        /// <param name="message"></param>
        /// <param name="chatsId"></param>
        /// <returns></returns>
        [HttpPost(nameof(SendMessageToSomeChats))]
        public async Task<ActionResult> SendMessageToSomeChats(string message, List<string> chatsId)
        {
            var convertedChats = new List<long>();

            foreach (var chatId in chatsId)
            {
                long.TryParse(chatId, out long convertedChatId);
                if (convertedChatId == null)
                {
                    throw new Exception($"Can't parse chat {chatsId}");
                }
                convertedChats.Add(convertedChatId);
            }

            await _messangerService.SendMessage(convertedChats, message);

            return Ok();
        }

    }
}
