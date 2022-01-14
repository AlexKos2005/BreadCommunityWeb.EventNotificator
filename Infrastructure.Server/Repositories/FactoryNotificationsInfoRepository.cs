using BreadCommunityWeb.EventNotificator.Application.Interfaces.Repositories;
using BreadCommunityWeb.EventNotificator.Domain;
using BreadCommunityWeb.EventNotificator.Infrastructure.Server.DataContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreadCommunityWeb.EventNotificator.Infrastructure.Server.Repositories
{
    public class FactoryNotificationsInfoRepository : IFactoryNotificationsInfoRepository
    {
        private readonly SQLiteDataContext _dataContext;
        public FactoryNotificationsInfoRepository(SQLiteDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task Delete(int id)
        {
            var factoryInfos = await _dataContext.FactoryNotificationsInfos.Where(c => c.Id == id)
                .Include(c => c.ConnectNotificationChats)
                .Include(c => c.ReportNotificationChats)
                .FirstOrDefaultAsync();
            _dataContext.FactoryNotificationsInfos.Remove(factoryInfos);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<FactoryNotificationsInfo?> Get(int id)
        {
            return await _dataContext.FactoryNotificationsInfos.Where(c => c.Id == id)
                .Include(c=>c.ConnectNotificationChats)
                .Include(c => c.ConnectNotificationsStatistic)
                .Include(c => c.ReportNotificationChats)
                .Include(c => c.ReportNotificationsStatistic)
                .FirstOrDefaultAsync();
        }

        public async Task<List<FactoryNotificationsInfo>> GetAll()
        {
            return await _dataContext.FactoryNotificationsInfos
                .Include(c=>c.ConnectNotificationChats)
                .Include(c=>c.ConnectNotificationsStatistic)
                .Include(c=>c.ReportNotificationChats)
                .Include(c=>c.ReportNotificationsStatistic)
                .ToListAsync();
        }

        public async Task<FactoryNotificationsInfo?> GetByExternalId(int factoryExternalId)
        {
            var tt = await _dataContext.FactoryNotificationsInfos.Where(c => c.FactoryExternalId == factoryExternalId)
                .Include(c => c.ConnectNotificationChats)
                .Include(c=>c.ConnectNotificationsStatistic)
                .Include(c => c.ReportNotificationChats)
                .Include(c => c.ReportNotificationsStatistic)
                .FirstOrDefaultAsync();

            return tt;
        }

        public async Task Save(FactoryNotificationsInfo entity)
        {
            await _dataContext.FactoryNotificationsInfos.AddRangeAsync(entity);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<FactoryNotificationsInfo?> Update(int id, FactoryNotificationsInfo entity)
        {
            var factoryInfos = await _dataContext.FactoryNotificationsInfos.Where(c => c.Id == id)
                .Include(c => c.ConnectNotificationChats)
                .Include(c => c.ConnectNotificationsStatistic)
                .Include(c => c.ReportNotificationChats)
                .Include(c => c.ReportNotificationsStatistic)
                .FirstOrDefaultAsync();
            if (factoryInfos == null)
            {
                return null;
            }

            factoryInfos.ConnectNotificationChats = entity.ConnectNotificationChats;
            factoryInfos.ConnectNotificationsStatistic = entity.ConnectNotificationsStatistic;
            factoryInfos.FactoryExternalId = entity.FactoryExternalId;
            factoryInfos.ReportNotificationChats = entity.ReportNotificationChats;
            factoryInfos.ReportNotificationsStatistic = entity.ReportNotificationsStatistic;

            await _dataContext.SaveChangesAsync();

            return factoryInfos;
        }
    }
}
