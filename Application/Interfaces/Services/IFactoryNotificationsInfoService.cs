using BreadCommunityWeb.EventNotificator.Application.Dto.RequestDto;
using BreadCommunityWeb.EventNotificator.Application.Dto.ResponseDto;
using BreadCommunityWeb.EventNotificator.Application.Interfaces.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreadCommunityWeb.EventNotificator.Application.Interfaces.Services
{
    public interface IFactoryNotificationsInfoService : ICrudService<FactoryNotificationsInfoRequestDto, FactoryNotificationsInfoResponseDto,int>
    {
        Task<List<FactoryNotificationsInfoResponseDto>> GetAll();
        Task DeleteByExternalId(int externalId);
        Task<FactoryNotificationsInfoResponseDto?> GetByExternalId(int externalId);

        Task<bool> AddConnectNotifInfo(int factoryExternalId, ConnectNotificationChatInfoRequestDto entity);
        Task DeleteConnectNotifInfo(int id);

        Task<bool> AddReportNotifInfo(int factoryExternalId, ReportNotificationChatInfoRequestDto entity);
        Task DeleteReportNotifInfo(int id);

        Task<bool> ChangeConnectNotificationsStatistic(int factoryExternalId, ConnectNotificationsStatisticRequestDto entity);
        Task<bool> ChangeReportNotificationsStatistic(int factoryExternalId, ReportNotificationsStatisticRequestDto entity);

        Task<bool> IncrementConnectNotificationsStatistic(int factoryExternalId);
        Task<bool> IncrementReportNotificationsStatistic(int factoryExternalId);

        Task<bool> ResetConnectNotificationsStatistic(int factoryExternalId);
        Task<bool> ResetReportNotificationsStatistic(int factoryExternalId);
    }
}
