using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreadCommunityWeb.EventNotificator.Application.Messages
{
    public static class ConnectNotifMessages
    {
        const int offset = 3;
        public static string BuildConnectTimeOutMessage(string factoryName, string factoryExternalId, DateTime lastDateTimeConnection)
        {
            string strResult = $"Отсутствие связи\n " +
                $"Код предприятия: {factoryExternalId}\n " +
                $"Наименование предприятия: {factoryName}\n " +
                $"Дата последнего подключения: {lastDateTimeConnection.AddHours(3).ToString()} (МСК)";
            return strResult;
        }
    }
}
