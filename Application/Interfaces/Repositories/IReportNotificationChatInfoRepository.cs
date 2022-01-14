using BreadCommunityWeb.EventNotificator.Application.Interfaces.Repositories.Base;
using BreadCommunityWeb.EventNotificator.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreadCommunityWeb.EventNotificator.Application.Interfaces.Repositories
{
    public interface IReportNotificationChatInfoRepository : ICrud<ReportNotificationChatInfo, int>
    {
    }
}
