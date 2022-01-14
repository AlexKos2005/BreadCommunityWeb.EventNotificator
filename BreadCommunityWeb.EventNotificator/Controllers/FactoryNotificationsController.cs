using BreadCommunityWeb.EventNotificator.Application.Dto.RequestDto;
using BreadCommunityWeb.EventNotificator.Application.Dto.ResponseDto;
using BreadCommunityWeb.EventNotificator.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BreadCommunityWeb.EventNotificator.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FactoryNotificationsController : ControllerBase
    {
        private readonly IFactoryNotificationsInfoService _factoryNotifService;
        public FactoryNotificationsController(IFactoryNotificationsInfoService factoryNotifService)
        {
            _factoryNotifService = factoryNotifService;
        }

        /// <summary>
        /// Сохранить предприятие для отправки уведомлений
        /// </summary>
        /// <returns></returns>
        [HttpPost(nameof(SaveFactoryInfo))]
        public async Task<ActionResult> SaveFactoryInfo(FactoryNotificationsInfoRequestDto factoryNotificationsInfo)
        {
            await _factoryNotifService.Save(factoryNotificationsInfo);
            return Ok();
        }

        /// <summary>
        /// Получить информацию об уведомлениях предприятия
        /// </summary>
        /// <returns></returns>
        [HttpGet(nameof(GetFactoryInfo))]
        public async Task<ActionResult<FactoryNotificationsInfoResponseDto>> GetFactoryInfo(int factoryExternalId)
        {
            return Ok(await _factoryNotifService.GetByExternalId(factoryExternalId));
        }

        /// <summary>
        /// Удалить информацию об уведомлениях предприятия
        /// </summary>
        /// <returns></returns>
        [HttpDelete(nameof(DeleteFactoryInfo))]
        public async Task<ActionResult> DeleteFactoryInfo(int factoryExternalId)
        {
            await _factoryNotifService.DeleteByExternalId(factoryExternalId);
            return Ok();
        }

        /// <summary>
        /// Добавить чат для уведомлений при отсутствии связи с предприятием
        /// </summary>
        /// <returns></returns>
        [HttpPost(nameof(PostConnectNotificationChatInfo))]
        public async Task<ActionResult> PostConnectNotificationChatInfo(int factoryExternalId,ConnectNotificationChatInfoRequestDto chatInfo)
        {
            await _factoryNotifService.AddConnectNotifInfo(factoryExternalId,chatInfo);
            return Ok();
        }

        /// <summary>
        /// Удалить чат для уведомлений при отсутствии связи с предприятием
        /// </summary>
        /// <returns></returns>
        [HttpPost(nameof(DeleteConnectNotificationChatInfo))]
        public async Task<ActionResult> DeleteConnectNotificationChatInfo(int chatId)
        {
            await _factoryNotifService.DeleteConnectNotifInfo(chatId);
            return Ok();
        }

        /// <summary>
        /// Добавить чат для уведомлений при отсутствии отчета предприятия
        /// </summary>
        /// <returns></returns>
        [HttpPost(nameof(PostReportNotificationChatInfo))]
        public async Task<ActionResult> PostReportNotificationChatInfo(int factoryExternalId, ReportNotificationChatInfoRequestDto chatInfo)
        {
            await _factoryNotifService.AddReportNotifInfo(factoryExternalId, chatInfo);
            return Ok();
        }

        /// <summary>
        /// Удалить чат для уведомлений при отсутствии отчета предприятия
        /// </summary>
        /// <returns></returns>
        [HttpPost(nameof(DeleteReportNotificationChatInfo))]
        public async Task<ActionResult> DeleteReportNotificationChatInfo(int chatId)
        {
            await _factoryNotifService.DeleteReportNotifInfo(chatId);
            return Ok();
        }

        /// <summary>
        /// Получить текущее значение счетчика уведомлений об отсутствии связи с предприятием
        /// </summary>
        /// <returns></returns>
        [HttpPost(nameof(GetConnectNotificationsValue))]
        public async Task<ActionResult<int>> GetConnectNotificationsValue(int factoryExternalId)
        {
            var factory = await _factoryNotifService.GetByExternalId(factoryExternalId);
            if(factory == null)
            {
                return NoContent();
            }

            return Ok(factory.ConnectNotificationsStatistic.NotificationCounter);
        }

        /// <summary>
        /// Задать значение счетчика уведомлений об отсутствии связи с предприятием
        /// </summary>
        /// <returns></returns>
        [HttpPost(nameof(PostConnectNotificationsValue))]
        public async Task<ActionResult> PostConnectNotificationsValue(int factoryExternalId, int connectNotifValue)
        {
            var connectNotifStat = new ConnectNotificationsStatisticRequestDto()
            {
                NotificationCounter = connectNotifValue
            };
            await _factoryNotifService.ChangeConnectNotificationsStatistic(factoryExternalId, connectNotifStat);
            return Ok();
        }

        /// <summary>
        /// Задать значение счетчика уведомлений об отсутствии отчета предприятия
        /// </summary>
        /// <returns></returns>
        [HttpPost(nameof(PostReportNotificationsValue))]
        public async Task<ActionResult> PostReportNotificationsValue(int factoryExternalId, int reportNotifValue)
        {
            var reportNotifStat = new ReportNotificationsStatisticRequestDto()
            {
                DayNotificationCounter = reportNotifValue
            };
            await _factoryNotifService.ChangeReportNotificationsStatistic(factoryExternalId, reportNotifStat);
            return Ok();
        }

        /// <summary>
        /// Получить текущее значение счетчика уведомлений об отсутствии отчета предприятия
        /// </summary>
        /// <returns></returns>
        [HttpPost(nameof(GetReportNotificationsValue))]
        public async Task<ActionResult<int>> GetReportNotificationsValue(int factoryExternalId)
        {
            var factory = await _factoryNotifService.GetByExternalId(factoryExternalId);
            if (factory == null)
            {
                return NoContent();
            }

            return Ok(factory.ReportNotificationsStatistic.DayNotificationCounter);
        }
    }
}
