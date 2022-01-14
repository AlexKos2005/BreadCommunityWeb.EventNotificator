using AutoMapper;
using BreadCommunityWeb.EventNotificator.Application.Dto.RequestDto;
using BreadCommunityWeb.EventNotificator.Application.Dto.ResponseDto;
using BreadCommunityWeb.EventNotificator.Application.Interfaces.Services;
using BreadCommunityWeb.EventNotificator.Domain;
using BreadCommunityWeb.EventNotificator.Infrastructure.Server.Configurations;
using BreadCommunityWeb.EventNotificator.Infrastructure.Server.DataContext;
using BreadCommunityWeb.EventNotificator.Infrastructure.Server.Interfaces;
using BreadCommunityWeb.EventNotificator.Infrastructure.Server.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreadCommunityWeb.EventNotificator.Infrastructure.Server.Services
{
    public class FactoryNotificationsInfoService : IFactoryNotificationsInfoService
    {
        private readonly DbConnectionConfig _dbConnectionConfig;
        private readonly IMapper _mapper;
        public FactoryNotificationsInfoService(DbConnectionConfig dbConnectionConfig, IMapper mapper)
        {
            _dbConnectionConfig = dbConnectionConfig;
            _mapper = mapper;
        }
        public async Task<bool> AddConnectNotifInfo(int factoryExternalId, ConnectNotificationChatInfoRequestDto entity)
        {
            using var db = new SQLiteDataContext(_dbConnectionConfig);
            var factoryInfoRepository = new FactoryNotificationsInfoRepository(db);
            var factoryInfo = await factoryInfoRepository.GetByExternalId(factoryExternalId);
            if(factoryInfo == null)
            {
                return false;
            }

            factoryInfo.ConnectNotificationChats.Add(_mapper.Map<ConnectNotificationChatInfo>(entity));

            var result = await factoryInfoRepository.Update(factoryInfo.Id, factoryInfo);
            if(result == null)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> AddReportNotifInfo(int factoryExternalId, ReportNotificationChatInfoRequestDto entity)
        {
            using var db = new SQLiteDataContext(_dbConnectionConfig);
            var factoryInfoRepository = new FactoryNotificationsInfoRepository(db);
            var factoryInfo = await factoryInfoRepository.GetByExternalId(factoryExternalId);
            if (factoryInfo == null)
            {
                return false;
            }

            factoryInfo.ReportNotificationChats.Add(_mapper.Map<ReportNotificationChatInfo>(entity));

            var result = await factoryInfoRepository.Update(factoryInfo.Id, factoryInfo);
            if (result == null)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> ChangeConnectNotificationsStatistic(int factoryExternalId, ConnectNotificationsStatisticRequestDto entity)
        {
            using var db = new SQLiteDataContext(_dbConnectionConfig);
            var factoryInfoRepository = new FactoryNotificationsInfoRepository(db);
            var factoryInfo = await factoryInfoRepository.GetByExternalId(factoryExternalId);
            if (factoryInfo == null)
            {
                return false;
            }

            factoryInfo.ConnectNotificationsStatistic = _mapper.Map<ConnectNotificationsStatistic>(entity);
            var result = await factoryInfoRepository.Update(factoryExternalId, factoryInfo);

            if(result ==null)
            {
                return false;
            }

            return true;

        }

        public async Task<bool> ChangeReportNotificationsStatistic(int factoryExternalId, ReportNotificationsStatisticRequestDto entity)
        {
            using var db = new SQLiteDataContext(_dbConnectionConfig);
            var factoryInfoRepository = new FactoryNotificationsInfoRepository(db);
            var factoryInfo = await factoryInfoRepository.GetByExternalId(factoryExternalId);
            if (factoryInfo == null)
            {
                return false;
            }

            factoryInfo.ReportNotificationsStatistic = _mapper.Map<ReportNotificationsStatistic>(entity);
            var result = await factoryInfoRepository.Update(factoryExternalId, factoryInfo);

            if (result == null)
            {
                return false;
            }

            return true;
        }

        public async Task Delete(int id)
        {
            using var db = new SQLiteDataContext(_dbConnectionConfig);
            var factoryInfoRepository = new FactoryNotificationsInfoRepository(db);
            await factoryInfoRepository.Delete(id);
        }

        public async Task DeleteByExternalId(int externalId)
        {
            using var db = new SQLiteDataContext(_dbConnectionConfig);
            var factoryInfoRepository = new FactoryNotificationsInfoRepository(db);
            var factory = await factoryInfoRepository.GetByExternalId(externalId);
            if(factory == null)
            {
                return;
            }

            await factoryInfoRepository.Delete(factory.Id);

        }

        public async Task DeleteConnectNotifInfo(int id)
        {
            using var db = new SQLiteDataContext(_dbConnectionConfig);
            var connectNotifRepository = new ConnectNotificationsChatInfoRepository(db);
            var entity = await connectNotifRepository.Get(id);
            if(entity == null)
            {
                return;
            }

            await connectNotifRepository.Delete(id);
        }

        public async Task DeleteReportNotifInfo(int id)
        {
            using var db = new SQLiteDataContext(_dbConnectionConfig);
            var reportNotifRepository = new ReportNotificationChatInfoRepository(db);
            var entity = await reportNotifRepository.Get(id);
            if (entity == null)
            {
                return;
            }
            await reportNotifRepository.Delete(id);
        }

        public async Task<FactoryNotificationsInfoResponseDto?> Get(int id)
        {
            using var db = new SQLiteDataContext(_dbConnectionConfig);
            var factoryInfoRepository = new FactoryNotificationsInfoRepository(db);
            return _mapper.Map<FactoryNotificationsInfoResponseDto>(await factoryInfoRepository.Get(id));
        }

        public async Task<List<FactoryNotificationsInfoResponseDto>> GetAll()
        {
            using var db = new SQLiteDataContext(_dbConnectionConfig);
            var factoryInfoRepository = new FactoryNotificationsInfoRepository(db);
            return _mapper.Map<List<FactoryNotificationsInfoResponseDto>>(await factoryInfoRepository.GetAll());
        }

        public async Task<FactoryNotificationsInfoResponseDto?> GetByExternalId(int externalId)
        {
            using var db = new SQLiteDataContext(_dbConnectionConfig);
            var factoryInfoRepository = new FactoryNotificationsInfoRepository(db);
            var tt = await factoryInfoRepository.GetByExternalId(externalId);
            return _mapper.Map<FactoryNotificationsInfoResponseDto>(await factoryInfoRepository.GetByExternalId(externalId));
        }

        public async Task<bool> IncrementConnectNotificationsStatistic(int factoryExternalId)
        {
            using var db = new SQLiteDataContext(_dbConnectionConfig);
            var factoryInfoRepository = new FactoryNotificationsInfoRepository(db);
            var factoryInfo = await factoryInfoRepository.GetByExternalId(factoryExternalId);
            if (factoryInfo == null)
            {
                return false;
            }

            var currentStat = factoryInfo.ConnectNotificationsStatistic;
            factoryInfo.ConnectNotificationsStatistic.NotificationCounter = currentStat.NotificationCounter + 1;
            var result = await factoryInfoRepository.Update(factoryInfo.Id, factoryInfo);

            if (result == null)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> IncrementReportNotificationsStatistic(int factoryExternalId)
        {
            using var db = new SQLiteDataContext(_dbConnectionConfig);
            var factoryInfoRepository = new FactoryNotificationsInfoRepository(db);
            var factoryInfo = await factoryInfoRepository.GetByExternalId(factoryExternalId);
            if (factoryInfo == null)
            {
                return false;
            }

            var currentStat = factoryInfo.ReportNotificationsStatistic;
            factoryInfo.ReportNotificationsStatistic.DayNotificationCounter = currentStat.DayNotificationCounter + 1;
            var result = await factoryInfoRepository.Update(factoryExternalId, factoryInfo);

            if (result == null)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> ResetConnectNotificationsStatistic(int factoryExternalId)
        {
            using var db = new SQLiteDataContext(_dbConnectionConfig);
            var factoryInfoRepository = new FactoryNotificationsInfoRepository(db);
            var factoryInfo = await factoryInfoRepository.GetByExternalId(factoryExternalId);
            if (factoryInfo == null)
            {
                return false;
            }

            factoryInfo.ConnectNotificationsStatistic.NotificationCounter = 0;
            var result = await factoryInfoRepository.Update(factoryExternalId, factoryInfo);

            if (result == null)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> ResetReportNotificationsStatistic(int factoryExternalId)
        {
            using var db = new SQLiteDataContext(_dbConnectionConfig);
            var factoryInfoRepository = new FactoryNotificationsInfoRepository(db);
            var factoryInfo = await factoryInfoRepository.GetByExternalId(factoryExternalId);
            if (factoryInfo == null)
            {
                return false;
            }

            var currentStat = factoryInfo.ReportNotificationsStatistic;
            factoryInfo.ReportNotificationsStatistic.DayNotificationCounter = 0;
            var result = await factoryInfoRepository.Update(factoryExternalId, factoryInfo);

            if (result == null)
            {
                return false;
            }

            return true;
        }

        public async Task Save(FactoryNotificationsInfoRequestDto entity)
        {
            using var db = new SQLiteDataContext(_dbConnectionConfig);
            var factoryInfoRepository = new FactoryNotificationsInfoRepository(db);
            await factoryInfoRepository.Save(_mapper.Map<FactoryNotificationsInfo>(entity));
        }

        public async Task<FactoryNotificationsInfoResponseDto> Update(int id, FactoryNotificationsInfoRequestDto entity)
        {
            using var db = new SQLiteDataContext(_dbConnectionConfig);
            var factoryInfoRepository = new FactoryNotificationsInfoRepository(db);
            return _mapper.Map<FactoryNotificationsInfoResponseDto>(await factoryInfoRepository.Update(id,_mapper.Map<FactoryNotificationsInfo>(entity)));
        }
    }
}
