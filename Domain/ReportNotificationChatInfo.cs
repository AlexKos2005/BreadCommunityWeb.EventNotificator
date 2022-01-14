using BreadCommunityWeb.EventNotificator.Domain.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreadCommunityWeb.EventNotificator.Domain
{
    [Index(nameof(Id), IsUnique = true)]
    public class ReportNotificationChatInfo : TelegramChatInfoBase
    {
        public int FactoryNotificationsInfoId { get; set; }

        [ForeignKey(nameof(FactoryNotificationsInfoId))]
        public FactoryNotificationsInfo FactoryNotificationsInfo { get; set; }
    }
}
