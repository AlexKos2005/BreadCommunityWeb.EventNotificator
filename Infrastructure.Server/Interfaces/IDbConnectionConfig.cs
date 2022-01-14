using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreadCommunityWeb.EventNotificator.Infrastructure.Server.Interfaces
{
    public interface IDbConnectionConfig
    {
        public string ConnectionString { get; set; }
    }
}
