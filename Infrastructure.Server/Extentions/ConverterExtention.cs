using BreadCommunityWeb.EventNotificator.Application.Dto;
using BreadCommunityWeb.EventNotificator.Application.Dto.RequestDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreadCommunityWeb.EventNotificator.Infrastructure.Server.Extentions
{
    public static class ConverterExtention
    {
        public static ConnectNotificatorServiceConfigDto ConvertServiceConfig(this ConnectNotificatorServiceConfigRequestDto configRequest)
        {
            var convertedConfig = new ConnectNotificatorServiceConfigDto();
            convertedConfig.IsEnable = configRequest.IsEnable;
            convertedConfig.MaxNotificationsCount = configRequest.MaxNotificationsCount;

            TimeSpan.TryParse(configRequest.MaxLackConnectionTime, out TimeSpan maxLackConnectionTimeResult);
            if(maxLackConnectionTimeResult == default)
            {
                throw new ArgumentException("Can't parse MaxLackConnectionTime");
            }

            TimeSpan.TryParse(configRequest.StartCheckReportDayTime, out TimeSpan startCheckReportDayTime);
            if (startCheckReportDayTime == default)
            {
                throw new ArgumentException("Can't parse StartCheckReportDayTime");
            }

            TimeSpan.TryParse(configRequest.ConnectionCheckPeriod, out TimeSpan connectionCheckPeriod);
            if (startCheckReportDayTime == default)
            {
                throw new ArgumentException("Can't parse ConnectionCheckPeriod");
            }

            convertedConfig.MaxLackConnectionTime = maxLackConnectionTimeResult;
            convertedConfig.StartCheckReportDayTime = startCheckReportDayTime;
            convertedConfig.ConnectionCheckPeriod = connectionCheckPeriod;

            return convertedConfig;

        }

        public static ReportNotificatorServiceConfigDto ConvertServiceConfig(this ReportNotificatorServiceConfigRequestDto configRequest)
        {
            var convertedConfig = new ReportNotificatorServiceConfigDto();
            convertedConfig.IsEnable = configRequest.IsEnable;
            convertedConfig.MaxNotificationsCount = configRequest.MaxNotificationsCount;

            TimeSpan.TryParse(configRequest.StartCheckReportDayTime, out TimeSpan startCheckReportDayTime);
            if (startCheckReportDayTime == default)
            {
                throw new ArgumentException("Can't parse StartCheckReportDayTime");
            }

            TimeSpan.TryParse(configRequest.ReportCheckPeriod, out TimeSpan reportCheckPeriod);
            if (reportCheckPeriod == default)
            {
                throw new ArgumentException("Can't parse ReportCheckPeriod");
            }

            convertedConfig.StartCheckReportDayTime = startCheckReportDayTime;
            convertedConfig.ReportCheckPeriod = reportCheckPeriod;

            return convertedConfig;

        }
    }
}
