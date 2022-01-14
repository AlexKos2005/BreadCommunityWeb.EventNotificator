using BreadCommunityWeb.EventNotificator.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreadCommunityWeb.EventNotificator.Domain
{
    public class ReportNotificatorServiceConfig : EntityBase
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
        public long ReportCheckPeriod { get; set; }

        /// <summary>
        /// Максимальное число уведомлений при отсутствии отчета.
        /// </summary>
        public int MaxNotificationsCount { get; set; }
    }
}
