using BreadCommunityWeb.EventNotificator.Application.Interfaces.Services;
using BreadCommunityWeb.EventNotificator.Infrastructure.Server.Configurations;
using MihaZupan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace BreadCommunityWeb.EventNotificator.Infrastructure.Server.Services
{
    public class TelegramConnectionFactory : IMessengerConnectionFactory<TelegramBotClient>
    {
        private readonly TelegramConnectionConfig _connectionConfig;
        private TelegramBotClient _telegramBot;
        public TelegramConnectionFactory(TelegramConnectionConfig connectionConfig)
        {
            _connectionConfig = connectionConfig;
        }
        public void Dispose()
        {
            _telegramBot.CloseAsync().Wait();
        }

        public TelegramBotClient GetConnection()
        {
            //HttpToSocks5Proxy _proxy = new HttpToSocks5Proxy(_connectionConfig.IpProxy,_connectionConfig.PortProxy,_connectionConfig.Login,_connectionConfig.Password);
            if(_telegramBot == null)
            {
                _telegramBot = new TelegramBotClient(_connectionConfig.Token);
            }
            return _telegramBot; 
        }
    }
}
