using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreadCommunityWeb.EventNotificator.Application.Dto
{
    public class ReportNotificatorServiceConfigDto
    {
        public bool IsEnable { get; set; }
        public TimeSpan StartCheckReportDayTime { get; set; }
        public TimeSpan ReportCheckPeriod { get; set; }

        public int MaxNotificationsCount { get; set; }
    }
}
