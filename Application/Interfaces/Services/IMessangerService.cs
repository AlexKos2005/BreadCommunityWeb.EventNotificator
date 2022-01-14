using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreadCommunityWeb.EventNotificator.Application.Interfaces.Services
{
    public interface IMessangerService
    {
        Task SendMessage(long chatId, string message);
        Task SendMessage(List<long> chatsId, string message);
    }
}
