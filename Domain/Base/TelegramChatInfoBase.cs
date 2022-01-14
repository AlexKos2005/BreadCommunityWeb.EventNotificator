using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreadCommunityWeb.EventNotificator.Domain.Base
{
    public class TelegramChatInfoBase : EntityBase
    {
        public long ChatId { get; set; }
    }
}
