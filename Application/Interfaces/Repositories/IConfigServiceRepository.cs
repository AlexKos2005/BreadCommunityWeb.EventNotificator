using BreadCommunityWeb.EventNotificator.Application.Interfaces.Repositories.Base;
using BreadCommunityWeb.EventNotificator.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreadCommunityWeb.EventNotificator.Application.Interfaces.Repositories
{
    public interface IConfigServiceRepository<T> : ICrud<T, int> where T : EntityBase
    {
        Task<List<T>> GetAll();
    }
}
