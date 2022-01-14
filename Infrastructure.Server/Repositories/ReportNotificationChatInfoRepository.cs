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
    public class ReportNotificationChatInfoRepository : IReportNotificationChatInfoRepository
    {

        private readonly SQLiteDataContext _dataContext;
        public ReportNotificationChatInfoRepository(SQLiteDataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task Delete(int id)
        {
            var reportInfos = await _dataContext.ReportNotificationChatInfos.Where(c => c.Id == id).FirstOrDefaultAsync();
            _dataContext.ReportNotificationChatInfos.Remove(reportInfos);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<ReportNotificationChatInfo?> Get(int id)
        {
            return await _dataContext.ReportNotificationChatInfos.Where(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task Save(ReportNotificationChatInfo entity)
        {
            await _dataContext.ReportNotificationChatInfos.AddRangeAsync(entity);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<ReportNotificationChatInfo?> Update(int id, ReportNotificationChatInfo entity)
        {
            var reportInfos = await _dataContext.ReportNotificationChatInfos.Where(c => c.Id == id).FirstOrDefaultAsync();
            if (reportInfos == null)
            {
                return null;
            }

            reportInfos.ChatId = entity.ChatId;

            return reportInfos;
        }
    }
}
