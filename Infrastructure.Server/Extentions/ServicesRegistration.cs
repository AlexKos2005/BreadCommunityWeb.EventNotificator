using BreadCommunityWeb.EventNotificator.Application.Dto;
using BreadCommunityWeb.EventNotificator.Application.Dto.RequestDto;
using BreadCommunityWeb.EventNotificator.Application.Dto.ResponseDto;
using BreadCommunityWeb.EventNotificator.Application.Interfaces.Services;
using BreadCommunityWeb.EventNotificator.Infrastructure.Server.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;

namespace BreadCommunityWeb.EventNotificator.Infrastructure.Server.Extentions
{
    public static class ServicesRegistration
    {
       
        public static void RegistrateServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IHttpService, HttpService>();

            services.AddSingleton<IFactoryNotificationsInfoService, FactoryNotificationsInfoService>();

            services.AddSingleton<IConnectNotificatorService, ConnectNotificatorService>();
            services.AddSingleton<IConfigService<ConnectNotificatorServiceConfigDto, ConnectNotificatorServiceConfigResponseDto, int>, ConnectNotificatorConfigService>();

            services.AddSingleton<IReportNotificatorService, ReportNotificatorService>();
            services.AddSingleton<IConfigService<ReportNotificatorServiceConfigDto, ReportNotificatorServiceConfigResponseDto, int>, ReportNotificatorConfigService>();

            services.AddSingleton<IMessengerConnectionFactory<TelegramBotClient>, TelegramConnectionFactory>();
            services.AddSingleton<IMessangerService, TelegramService>();
            
        }

        private static void StartWorkerServices(IConnectNotificatorService connectNotificatorService, IReportNotificatorService reportNotificatorService)
        {
            connectNotificatorService.Start(new CancellationTokenSource());
            reportNotificatorService.Start(new CancellationTokenSource());
        }
    }
}
