using BreadCommunityWeb.EventNotificator.Application.Interfaces.Services;
using BreadCommunityWeb.EventNotificator.Infrastructure.Server.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using NLog;
using Telegram.Bot.Types;

namespace BreadCommunityWeb.EventNotificator.Infrastructure.Server.Services
{
    public class TelegramService : IMessangerService
    {
        private readonly TelegramConnectionConfig _connectionConfig;
        private readonly ILogger _logger;
        public TelegramService(TelegramConnectionConfig connectionConfig)
        {
            _connectionConfig = connectionConfig;
            _logger = LogManager.GetCurrentClassLogger();
        }
        public async Task SendMessage(long chatId, string message)
        {
            using var telegramFactory = new TelegramConnectionFactory(_connectionConfig);
            var telegramClient = telegramFactory.GetConnection();
            await telegramClient.SendTextMessageAsync(chatId, message);
            await Task.Delay(1500);
        }

        public async Task SendMessage(List<long> chatsId, string message)
        {
            using var telegramFactory = new TelegramConnectionFactory(_connectionConfig);
            var telegramClient = telegramFactory.GetConnection();
            foreach(var chatId in chatsId)
            {
                await telegramClient.SendTextMessageAsync(chatId, message);
                await Task.Delay(1500);
            }

        }
    }
}
