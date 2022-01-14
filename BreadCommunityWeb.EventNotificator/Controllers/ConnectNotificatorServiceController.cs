using BreadCommunityWeb.EventNotificator.Application.Dto;
using BreadCommunityWeb.EventNotificator.Application.Dto.RequestDto;
using BreadCommunityWeb.EventNotificator.Application.Dto.ResponseDto;
using BreadCommunityWeb.EventNotificator.Application.Interfaces.Services;
using BreadCommunityWeb.EventNotificator.Infrastructure.Server.Extentions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BreadCommunityWeb.EventNotificator.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConnectNotificatorServiceController : ControllerBase
    {
        private readonly IConnectNotificatorService _notificatorService;
        private readonly IConfigService<ConnectNotificatorServiceConfigDto, ConnectNotificatorServiceConfigResponseDto, int> _configService;
        public ConnectNotificatorServiceController(
            IConnectNotificatorService notificatorService, 
            IConfigService<ConnectNotificatorServiceConfigDto, ConnectNotificatorServiceConfigResponseDto, int> configService)
        {
            _notificatorService = notificatorService;
            _configService = configService;
        }
        
        /// <summary>
        /// Получить текущую конфигурацию сервиса
        /// </summary>
        /// <returns></returns>
        [HttpGet(nameof(GetServiceConfig))]
        public async Task<ActionResult<ConnectNotificatorServiceConfigResponseDto>> GetServiceConfig()
        {
            return Ok(await _configService.GetFirst());
        }

        /// <summary>
        /// Сохранить или обновить текущую конфигурацию сервиса
        /// </summary>
        /// <returns></returns>
        [HttpPut(nameof(PutServiceConfig))]
        public async Task<ActionResult> PutServiceConfig(ConnectNotificatorServiceConfigRequestDto config)
        {
            var convertedConfig = config.ConvertServiceConfig();
            await _configService.SaveOrUpdateByFirst(convertedConfig);
            return Ok();
        }

        /// <summary>
        /// Запустить сервис
        /// </summary>
        /// <returns></returns>
        [HttpPost(nameof(StartService))]
        public ActionResult StartService()
        {
            _notificatorService.Start(new CancellationTokenSource());
            return Ok();
        }

        /// <summary>
        /// Остановить сервис
        /// </summary>
        /// <returns></returns>
        [HttpPost(nameof(StopService))]
        public ActionResult StopService()
        {
            _notificatorService.Stop();
            return Ok();
        }

        /// <summary>
        /// Перезапустить сервис
        /// </summary>
        /// <returns></returns>
        [HttpPost(nameof(RestartService))]
        public ActionResult RestartService()
        {
            _notificatorService.Restart(new CancellationTokenSource());
            return Ok();
        }
    }
}
