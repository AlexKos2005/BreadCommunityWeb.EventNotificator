using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreadCommunityWeb.EventNotificator.Application.Interfaces.Services.Base
{
    public interface ICrudService<Tin,Tout, Tid> where Tin : class where Tout : class
    {
        Task Save(Tin entity);
        Task<Tout?> Get(Tid id);

        Task<Tout?> Update(Tid id, Tin entity);

        Task Delete(Tid id);
    }
}
