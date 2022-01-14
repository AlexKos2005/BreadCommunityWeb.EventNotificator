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
    public class ConnectNotificationsChatInfoRepository : IConnectNotificationsChatInfoRepository
    {
        private readonly SQLiteDataContext _dataContext;
        public ConnectNotificationsChatInfoRepository(SQLiteDataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task Delete(int id)
        {
            var connectInfos = await _dataContext.ConnectNotificationChatInfos.Where(c => c.Id == id).FirstOrDefaultAsync();
            _dataContext.ConnectNotificationChatInfos.Remove(connectInfos);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<ConnectNotificationChatInfo?> Get(int id)
        {
            return await _dataContext.ConnectNotificationChatInfos.Where(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task Save(ConnectNotificationChatInfo entity)
        {
            await _dataContext.ConnectNotificationChatInfos.AddRangeAsync(entity);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<ConnectNotificationChatInfo?> Update(int id, ConnectNotificationChatInfo entity)
        {
            var connectInfos = await _dataContext.ConnectNotificationChatInfos.Where(c => c.Id == id).FirstOrDefaultAsync();
            if (connectInfos == null)
            {
                return null;
            }

            connectInfos.ChatId = entity.ChatId;

            return connectInfos;
        }
    }
}
