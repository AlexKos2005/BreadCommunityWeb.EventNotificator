using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreadCommunityWeb.EventNotificator.Infrastructure.Server.Configurations
{
    public class TelegramConnectionConfig
    {
        public string IpProxy { get; set; } = string.Empty;
        public int PortProxy { get; set; }
        public string Login { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public string Token { get; set; } = string.Empty;
    }
}
