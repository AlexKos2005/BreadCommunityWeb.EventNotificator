using BreadCommunityWeb.EventNotificator.Domain.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreadCommunityWeb.EventNotificator.Application.Dto.RequestDto
{
    public class ReportNotificationChatInfoRequestDto
    {
        public int Id { get; private set; }
        public long ChatId { get; set; }
    }
}
