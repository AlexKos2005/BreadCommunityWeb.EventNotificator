using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreadCommunityWeb.EventNotificator.Application.Dto.ResponseDto
{
    public class FactoryNotificationsInfoResponseDto
    {
        public int Id { get; private set; }
        public int FactoryExternalId { get; set; }

        public bool IsEnable { get; set; }

        public List<ConnectNotificationChatInfoResponseDto> ConnectNotificationChats { get; set; }

        public List<ReportNotificationChatInfoResponseDto> ReportNotificationChats { get; set; }

        public ConnectNotificationsStatisticResponseDto ConnectNotificationsStatistic { get; set; }

        public ReportNotificationsStatisticResponseDto ReportNotificationsStatistic { get; set; }
    }
}
