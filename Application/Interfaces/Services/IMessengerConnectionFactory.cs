using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreadCommunityWeb.EventNotificator.Application.Interfaces.Services
{
    public interface IMessengerConnectionFactory<T> : IDisposable where T : class
    {
        T GetConnection();
    }
}
