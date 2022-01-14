using BreadCommunityWeb.EventNotificator.Application.Dto.ResponseDto;
using BreadCommunityWeb.EventNotificator.Application.Interfaces.Services;
using BreadCommunityWeb.EventNotificator.Domain.Enums;
using BreadCommunityWeb.EventNotificator.Infrastructure.Server.Extentions;
using BreadCommunityWeb.EventNotificator.Application.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NLog;
using System.Net.Http;
using BreadCommunityWeb.EventNotificator.Application.Dto.RequestDto;
using BreadCommunityWeb.EventNotificator.Application.Dto;

namespace BreadCommunityWeb.EventNotificator.Infrastructure.Server.Services
{
    public class ConnectNotificatorService : IConnectNotificatorService
    {
        protected bool _isStarted = false;
        private CancellationTokenSource _cts = null!;
        private readonly IConfigService<ConnectNotificatorServiceConfigDto, ConnectNotificatorServiceConfigResponseDto, int> _configurationService;
        private ConnectNotificatorServiceConfigResponseDto _config;
        private readonly IHttpService _httpService;
        private readonly ILogger _logger;
        private readonly IMessangerService _messengerService;

        private readonly IFactoryNotificationsInfoService _factoryNotifService;
        public ServiceState ServiceState { get; set; }

        public ConnectNotificatorService(
            IConfigService<ConnectNotificatorServiceConfigDto, ConnectNotificatorServiceConfigResponseDto, int> configurationService, 
            IFactoryNotificationsInfoService factoryNotifService, 
            IHttpService httpService,
            IMessangerService messengerService)
        {
            _configurationService = configurationService;
            _httpService = httpService;
            _factoryNotifService = factoryNotifService;
            _messengerService = messengerService;
            _logger = LogManager.GetCurrentClassLogger();
        }

        public async Task Restart(CancellationTokenSource cts)
        {
            _cts.Cancel();
            _isStarted = false;
            ServiceState = ServiceState.Stopped;

            Start(cts);

        }

        public void Start(CancellationTokenSource cts)
        {
            _cts = cts;
            if (_isStarted == true)
            {
                return;
            }

            _config = _configurationService.GetFirst().Result;
            if(_config == null)
            {
                ServiceState = ServiceState.Stopped;
                _logger.Warn("Service config is null. Service Didn't start!");
                return;
            }
            if(_config.IsEnable == false)
            {
                _logger.Warn("Service config is not Enabled. Service Didn't start!");
                return;
            }
            _isStarted = true;
            Task.Run(async() =>
            {
                await RunLongTask(_cts);
            }
            );
        }

        protected async Task RunLongTask(CancellationTokenSource cts)
        {
            while(!cts.IsCancellationRequested)
            {
                try
                {
                    if(DateTimeOffset.UtcNow.TimeOfDay> _config.StartCheckReportDayTime)
                    {
                        await CheckLastConnectionTimes();
                    }
                    else
                    {
                        _logger.Trace("Not time of day for reporting");
                    }
                    
                }
                catch(Exception e)
                {
                    _logger.Error(e);
                    await Task.Delay(5000);
                }
                

                await Task.Delay(_config.ConnectionCheckPeriod);
            }
        }

        protected async Task CheckLastConnectionTimes()
        {
            var factoryInfos = await _factoryNotifService.GetAll();
            foreach(var factory in factoryInfos)
            {
                if(factory.ConnectNotificationsStatistic.NotificationCounter < _config.MaxNotificationsCount)
                {
                    try
                    {
                        var factoryResponse = await _httpService.GetFactory(factory.FactoryExternalId);
                        if (factoryResponse == null)
                        {
                            _logger.Warn($"Attempt of getting factory actions is failed! FactoryExternalId: {factory.FactoryExternalId}");
                            continue;
                        }

                        var isConnectTimeOut = factoryResponse.EnterpriseActionsInfo.LastDateTimeConnectionOffset.DateTime.CheckTimeOut(_config.MaxLackConnectionTime.Ticks);
                        if (isConnectTimeOut)
                        {
                            var message = ConnectNotifMessages.BuildConnectTimeOutMessage(
                                factoryResponse.Description,
                                factoryResponse.ExternalId.ToString(),
                                factoryResponse.EnterpriseActionsInfo.LastDateTimeConnectionOffset.UtcDateTime);
                            var chatsId = factory.ConnectNotificationChats.Select(c => c.ChatId).ToList();

                            await SendMessage(chatsId, message);
                            _logger.Trace($"Notyfication by lack connection of factory {factory.FactoryExternalId} is sended");

                            var incrementResult = await _factoryNotifService.IncrementConnectNotificationsStatistic(factory.FactoryExternalId);
                            if (incrementResult == false)
                            {
                                _logger.Trace($"Can't increment ConnectNotifStat for factory external id : {factory.FactoryExternalId}");
                            }
                        }
                        else
                        {
                            await _factoryNotifService.ResetConnectNotificationsStatistic(factory.FactoryExternalId);
                        }

                       
                    }
                    catch (Exception e)
                    {
                        _logger.Error(e);
                    }
                }
                
                

            }
        }

        protected async Task SendMessage(List<long> chatsId,string message)
        {
            //TODO: Разобраться с исключением при отправки сообщений. Исключение вываливается, но сообщения доставляются.
            try
            {
                await _messengerService.SendMessage(chatsId, message);
            }
            catch(Exception e)
            {

            }
            
        }

     
        public void Stop()
        {
            _cts.Cancel();
            _isStarted = false;
            ServiceState = ServiceState.Stopped;

        }
    }
}
