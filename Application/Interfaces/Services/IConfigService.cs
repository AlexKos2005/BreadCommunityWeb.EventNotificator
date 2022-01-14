using BreadCommunityWeb.EventNotificator.Application.Interfaces.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreadCommunityWeb.EventNotificator.Application.Interfaces.Services
{
    public interface IConfigService<Tin,Tout, Tid> where Tin : class where Tout : class
    {
        Task SaveOrUpdateByFirst(Tin entity);
        Task<Tout?> GetFirst();

        Task DeleteFirst();
    }
}
