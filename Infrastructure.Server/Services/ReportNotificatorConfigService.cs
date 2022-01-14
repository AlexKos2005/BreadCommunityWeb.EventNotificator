using AutoMapper;
using BreadCommunityWeb.EventNotificator.Application.Dto;
using BreadCommunityWeb.EventNotificator.Application.Dto.RequestDto;
using BreadCommunityWeb.EventNotificator.Application.Dto.ResponseDto;
using BreadCommunityWeb.EventNotificator.Application.Interfaces.Services;
using BreadCommunityWeb.EventNotificator.Application.Interfaces.Services.Base;
using BreadCommunityWeb.EventNotificator.Domain;
using BreadCommunityWeb.EventNotificator.Infrastructure.Server.Configurations;
using BreadCommunityWeb.EventNotificator.Infrastructure.Server.DataContext;
using BreadCommunityWeb.EventNotificator.Infrastructure.Server.Interfaces;
using BreadCommunityWeb.EventNotificator.Infrastructure.Server.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreadCommunityWeb.EventNotificator.Infrastructure.Server.Services
{
    public class ReportNotificatorConfigService : IConfigService<ReportNotificatorServiceConfigDto, ReportNotificatorServiceConfigResponseDto, int>
    {
        private readonly DbConnectionConfig _dbConnectionConfig;
        private readonly IMapper _mapper;
        public ReportNotificatorConfigService(DbConnectionConfig dbConnectionConfig,IMapper mapper)
        {
            _dbConnectionConfig = dbConnectionConfig;
            _mapper = mapper;
        }
  
        public async Task DeleteFirst()
        {
            using var db = new SQLiteDataContext(_dbConnectionConfig);
            var repository = new ServiceConfigRepositoryBase<ReportNotificatorServiceConfig>(db);
            var config = repository.GetAll().Result.FirstOrDefault();
            if (config != null)
            {
                await repository.Delete(config.Id);
            }

        }

        public async Task<ReportNotificatorServiceConfigResponseDto?> GetFirst()
        {
            using var db = new SQLiteDataContext(_dbConnectionConfig);
            var repository = new ServiceConfigRepositoryBase<ReportNotificatorServiceConfig>(db);
            var config = repository.GetAll().Result.FirstOrDefault();
            if (config == null)
            {
                return null;
            }
            return _mapper.Map<ReportNotificatorServiceConfigResponseDto>(config);
        }

        public async Task SaveOrUpdateByFirst(ReportNotificatorServiceConfigDto entity)
        {
            using var db = new SQLiteDataContext(_dbConnectionConfig);
            var repository = new ServiceConfigRepositoryBase<ReportNotificatorServiceConfig>(db);
            var config = repository.GetAll().Result.FirstOrDefault();
            if (config == null)
            {
                await repository.Save(_mapper.Map<ReportNotificatorServiceConfig>(entity));
                return;
            }

            var updateEntity = _mapper.Map<ReportNotificatorServiceConfig>(entity);
            config.IsEnable= updateEntity.IsEnable;
            config.MaxNotificationsCount = updateEntity.MaxNotificationsCount;
            config.ReportCheckPeriod = updateEntity.ReportCheckPeriod;
            config.StartCheckReportDayTime = updateEntity.StartCheckReportDayTime;
            await repository.Update(config.Id, updateEntity);
        }
    }
}
