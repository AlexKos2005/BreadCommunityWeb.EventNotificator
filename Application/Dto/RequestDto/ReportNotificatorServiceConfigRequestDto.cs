using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreadCommunityWeb.EventNotificator.Application.Dto.RequestDto
{
    public class ReportNotificatorServiceConfigRequestDto
    {
        public bool IsEnable { get; set; }
        public string StartCheckReportDayTime { get; set; }
        public string ReportCheckPeriod { get; set; }

        public int MaxNotificationsCount { get; set; }
    }
}
