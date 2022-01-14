using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreadCommunityWeb.EventNotificator.Application.Dto.RequestDto
{
    public class ConnectNotificationsStatisticRequestDto
    {
        public int Id { get; private set; }
        public int NotificationCounter { get; set; }
    }
}
