using BreadCommunityWeb.EventNotificator.Domain.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreadCommunityWeb.EventNotificator.Domain
{
    [Index(nameof(FactoryExternalId), IsUnique = true)]
    public class FactoryNotificationsInfo : EntityBase
    {
        public FactoryNotificationsInfo()
        {
            ConnectNotificationChats = new List<ConnectNotificationChatInfo>();
            ReportNotificationChats = new List<ReportNotificationChatInfo>();
            ConnectNotificationsStatistic = new ConnectNotificationsStatistic();
            ReportNotificationsStatistic = new ReportNotificationsStatistic();

        }

        public int FactoryExternalId { get; set; }

        public bool IsEnable { get; set; }
        public List<ConnectNotificationChatInfo> ConnectNotificationChats { get; set; }

        public List<ReportNotificationChatInfo> ReportNotificationChats { get; set; }

        public ConnectNotificationsStatistic ConnectNotificationsStatistic { get; set; }

        public ReportNotificationsStatistic ReportNotificationsStatistic { get; set; }
    }
}
