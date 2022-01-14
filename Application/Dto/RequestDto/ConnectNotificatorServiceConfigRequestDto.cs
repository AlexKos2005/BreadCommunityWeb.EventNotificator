using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreadCommunityWeb.EventNotificator.Application.Dto.RequestDto
{
    public class ConnectNotificatorServiceConfigRequestDto
    {
        public bool IsEnable { get; set; }

        public string StartCheckReportDayTime { get; set; }
        public string ConnectionCheckPeriod { get; set; }
        public string MaxLackConnectionTime { get; set; }

        public int MaxNotificationsCount { get; set; }
    }
}
