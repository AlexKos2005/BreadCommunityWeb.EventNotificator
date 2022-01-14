using BreadCommunityWeb.EventNotificator.Application.Dto.ResponseDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreadCommunityWeb.EventNotificator.Application.Interfaces.Services
{
    public interface IHttpService
    {
        Task<FactoryResponseDto?> GetFactory(int factoryExternalId);
        Task<FactoryActionsInfoResponseDto?> GetFactoryActionInfo(int factoryExternalId);
    }
}
