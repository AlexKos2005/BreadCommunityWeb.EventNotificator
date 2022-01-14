using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreadCommunityWeb.EventNotificator.Infrastructure.Server.Extentions
{
    public static class DateTimeCheckExtention
    {
        public static bool CheckTimeOut(this DateTime lastDtConnection,long timeStepTicks)
        {
            if (DateTimeOffset.UtcNow.Subtract(lastDtConnection.ToUniversalTime()).Ticks >= timeStepTicks)
            {
                return true;
            }

            return false;
        }
    }
}
