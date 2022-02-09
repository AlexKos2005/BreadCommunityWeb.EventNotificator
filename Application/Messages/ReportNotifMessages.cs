using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreadCommunityWeb.EventNotificator.Application.Messages
{
    public static class ReportNotifMessages
    {
        const int offset = 3;
       public static string BuildReportOutMessage(string factoryName, string factoryExternalId, DateTime lastDateTimeReporting)
        {
            string strResult = $"Отсутствие отчета\n " +
                 $"Код предприятия: {factoryExternalId}\n " +
                 $"Наименование предприятия: {factoryName}\n " +
                 $"Дата последней отправки отчета: {lastDateTimeReporting.ToString()} (МСК)";
            return strResult;
        }
    }
}
