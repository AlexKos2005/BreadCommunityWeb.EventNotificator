using AutoMapper;
using BreadCommunityWeb.EventNotificator.Application.Dto.RequestDto;
using BreadCommunityWeb.EventNotificator.Application.Dto.ResponseDto;
using BreadCommunityWeb.EventNotificator.Application.Interfaces.Services.Base;
using BreadCommunityWeb.EventNotificator.Domain;
using BreadCommunityWeb.EventNotificator.Infrastructure.Server.Configurations;
using BreadCommunityWeb.EventNotificator.Infrastructure.Server.DataContext;
using BreadCommunityWeb.EventNotificator.Infrastructure.Server.Interfaces;
using BreadCommunityWeb.EventNotificator.Infrastructure.Server.Repositories.Base;
using BreadCommunityWeb.EventNotificator.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BreadCommunityWeb.EventNotificator.Application.Dto;

namespace BreadCommunityWeb.EventNotificator.Infrastructure.Server.Services
{
    public class ConnectNotificatorConfigService : IConfigService<ConnectNotificatorServiceConfigDto, ConnectNotificatorServiceConfigResponseDto, int>
    {
        private readonly DbConnectionConfig _dbConnectionConfig;
        private readonly IMapper _mapper;
        public ConnectNotificatorConfigService(DbConnectionConfig dbConnectionConfig,IMapper mapper)
        {
            _dbConnectionConfig = dbConnectionConfig;
            _mapper = mapper;
        }
        public async Task DeleteFirst()
        {
            using var db = new SQLiteDataContext(_dbConnectionConfig);
            var repository = new ServiceConfigRepositoryBase<ConnectNotificatorServiceConfig>(db);
            var config = repository.GetAll().Result.FirstOrDefault();
            if(config != null)
            {
                await repository.Delete(config.Id);
            }
            
        }

        public async Task<ConnectNotificatorServiceConfigResponseDto?> GetFirst()
        {
            using var db = new SQLiteDataContext(_dbConnectionConfig);
            var repository = new ServiceConfigRepositoryBase<ConnectNotificatorServiceConfig>(db);
            var config = repository.GetAll().Result.FirstOrDefault();
            if (config == null)
            {
                return null;
            }
            return _mapper.Map<ConnectNotificatorServiceConfigResponseDto>(config);
        }

        public async Task SaveOrUpdateByFirst(ConnectNotificatorServiceConfigDto entity)
        {
            using var db = new SQLiteDataContext(_dbConnectionConfig);
            var repository = new ServiceConfigRepositoryBase<ConnectNotificatorServiceConfig>(db);
            var config = repository.GetAll().Result.FirstOrDefault();

            if (config == null)
            {
                await repository.Save(_mapper.Map<ConnectNotificatorServiceConfig>(entity));
                return;
            }
            var updateEntity = _mapper.Map<ConnectNotificatorServiceConfig>(entity);
            config.ConnectionCheckPeriod = updateEntity.ConnectionCheckPeriod;
            config.IsEnable = updateEntity.IsEnable;
            config.MaxLackConnectionTimeTicks = updateEntity.MaxLackConnectionTimeTicks;
            config.MaxNotificationsCount = updateEntity.MaxNotificationsCount;
            config.StartCheckReportDayTime = updateEntity.StartCheckReportDayTime;
            await repository.Update(config.Id, updateEntity);
        }

    }
}
