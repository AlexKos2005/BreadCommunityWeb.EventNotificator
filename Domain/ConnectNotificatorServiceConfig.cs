using BreadCommunityWeb.EventNotificator.Domain.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreadCommunityWeb.EventNotificator.Domain
{
    public class ConnectNotificatorServiceConfig : EntityBase
    {
        /// <summary>
        /// Разрешение на работу сервиса
        /// </summary>
        public bool IsEnable { get; set; }

        /// <summary>
        /// Время дня начала отправки уведомлений (до этого времени уведомления не рассылаются)
        /// </summary>
        public long StartCheckReportDayTime { get; set; }

        /// <summary>
        /// Период проверки времени последнего подключения.
        /// </summary>
        public long ConnectionCheckPeriod { get; set; }

        /// <summary>
        /// Максимально-допустимое время отсутствия связи (в тиках).
        /// </summary>
        public long MaxLackConnectionTimeTicks { get; set; }

        /// <summary>
        /// Максимальное число уведомлений при отсутствии связи.
        /// </summary>
        public int MaxNotificationsCount { get; set; }
    }
}
