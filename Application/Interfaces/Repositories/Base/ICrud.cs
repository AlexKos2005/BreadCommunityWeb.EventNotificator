using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreadCommunityWeb.EventNotificator.Application.Interfaces.Repositories.Base
{
    public interface ICrud<Tentity,Tid> where Tentity: class
    {
        Task Save(Tentity entity);
        Task<Tentity?> Get(Tid id);

        Task<Tentity?> Update(Tid id, Tentity entity);

        Task Delete(Tid id);
    }
}
