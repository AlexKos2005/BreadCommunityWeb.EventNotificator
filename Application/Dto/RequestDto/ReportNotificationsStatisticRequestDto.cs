using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreadCommunityWeb.EventNotificator.Application.Dto.RequestDto
{
   public class ReportNotificationsStatisticRequestDto
    {
        public int Id { get; private set; }
        public DateTimeOffset LastNotificationDate { get; set; }

        public int DayNotificationCounter { get; set; }
    }
}
