using BreadCommunityWeb.EventNotificator.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BreadCommunityWeb.EventNotificator.Application.Interfaces.Services.Base
{
    public interface IWorkerService
    {
        public ServiceState ServiceState { get; set; }
        void Start(CancellationTokenSource cts);

        void Stop();

        Task Restart(CancellationTokenSource cts);
    }
}
