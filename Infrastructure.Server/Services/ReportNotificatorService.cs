using BreadCommunityWeb.EventNotificator.Application.Dto.ResponseDto;
using BreadCommunityWeb.EventNotificator.Application.Interfaces.Services;
using BreadCommunityWeb.EventNotificator.Application.Interfaces.Services.Base;
using BreadCommunityWeb.EventNotificator.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NLog;
using BreadCommunityWeb.EventNotificator.Application.Messages;
using BreadCommunityWeb.EventNotificator.Application.Dto.RequestDto;
using BreadCommunityWeb.EventNotificator.Application.Dto;

namespace BreadCommunityWeb.EventNotificator.Infrastructure.Server.Services
{
    public class ReportNotificatorService : IReportNotificatorService
    {
        protected bool _isStarted = false;
        private CancellationTokenSource _cts = null!;
        private readonly IConfigService<ReportNotificatorServiceConfigDto, ReportNotificatorServiceConfigResponseDto, int> _configurationService;
        private ReportNotificatorServiceConfigResponseDto _config;
        private readonly IHttpService _httpService;
        private readonly ILogger _logger;
        private readonly IMessangerService _messengerService;
        private DateTimeOffset _lastNotifDate;

        private readonly IFactoryNotificationsInfoService _factoryNotifService;
        public ServiceState ServiceState { get; set; }

        public ReportNotificatorService(
            IConfigService<ReportNotificatorServiceConfigDto, ReportNotificatorServiceConfigResponseDto, int> configurationService,
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
        public Task Restart(CancellationTokenSource cts)
        {
            throw new NotImplementedException();
        }

        public void Start(CancellationTokenSource cts)
        {
            _cts = cts;
            if (_isStarted == true)
            {
                return;
            }

            _config = _configurationService.GetFirst().Result;
            if (_config == null)
            {
                ServiceState = ServiceState.Stopped;
                _logger.Warn("Service config is null. Service Didn't start!");
                return;
            }
            if (_config.IsEnable == false)
            {
                _logger.Warn("Service config is not Enabled. Service Didn't start!");
                return;
            }
            _isStarted = true;
            _lastNotifDate = DateTimeOffset.Now;
            Task.Run(async () =>
            {
                await RunLongTask(_cts);
            }
            );
        }

        protected async Task RunLongTask(CancellationTokenSource cts)
        {
            while (!cts.IsCancellationRequested)
            {
                if (_lastNotifDate.Date != DateTimeOffset.UtcNow.Date)
                {
                    await ResetNotifCountByNewDayStart();
                    _lastNotifDate = DateTimeOffset.Now;
                }
                try
                {
                    if (DateTimeOffset.UtcNow.TimeOfDay > _config.StartCheckReportDayTime)
                    {
                        await CheckLastReportTimes();
                    }
                    else
                    {
                        _logger.Trace("Not time of day for reporting");
                    }
                }
                catch (Exception e)
                {
                    _logger.Error(e);
                    await Task.Delay(5000);
                }

                await Task.Delay(_config.ReportCheckPeriod);
            }
        }

        protected async Task CheckLastReportTimes()
        {
            var factoryInfos = await _factoryNotifService.GetAll();
            foreach (var factory in factoryInfos)
            {
                if (factory.ReportNotificationsStatistic.DayNotificationCounter <= _config.MaxNotificationsCount)
                {
                    try
                    {
                        var factoryResponse = await _httpService.GetFactory(factory.FactoryExternalId);
                        if (factoryResponse == null)
                        {
                            _logger.Warn($"Attempt of getting factory actions is failed! FactoryExternalId: {factory.FactoryExternalId}");
                            continue;
                        }

                        if (factoryResponse.EnterpriseActionsInfo.LastDateTimeReportSendingOffset.LocalDateTime.Date < DateTime.UtcNow.Date)
                        {
                            var message = ReportNotifMessages.BuildReportOutMessage(
                               factoryResponse.Description,
                               factoryResponse.ExternalId.ToString(),
                               factoryResponse.EnterpriseActionsInfo.LastDateTimeConnectionOffset.LocalDateTime);
                            var chatsId = factory.ConnectNotificationChats.Select(c => c.ChatId).ToList();
                            await _messengerService.SendMessage(chatsId, message);

                            _logger.Trace($"Notyfication by lack reporting of factory {factory.FactoryExternalId} is sended");

                            var incrementResult = await _factoryNotifService.IncrementReportNotificationsStatistic(factory.FactoryExternalId);
                            if (incrementResult == false)
                            {
                                _logger.Trace($"Can't increment ReportNotifStat for factory external id : {factory.FactoryExternalId}");
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

        protected async Task ResetNotifCountByNewDayStart()
        {
            var factoryInfos = await _factoryNotifService.GetAll();
            foreach (var factory in factoryInfos)
            {
                try
                {
                    await _factoryNotifService.ResetConnectNotificationsStatistic(factory.FactoryExternalId);
                }
                catch (Exception e)
                {
                    _logger.Error($"Can't reset ReportNotifStat for factory external id : {factory.FactoryExternalId}");
                    _logger.Error(e);
                }
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
