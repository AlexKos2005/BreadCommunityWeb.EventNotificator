using BreadCommunityWeb.EventNotificator.Application.Dto.ResponseDto;
using BreadCommunityWeb.EventNotificator.Application.Interfaces.Services;
using BreadCommunityWeb.EventNotificator.Application.EndPoints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using NLog;


namespace BreadCommunityWeb.EventNotificator.Infrastructure.Server.Services
{
    public class HttpService : IHttpService
    {
        public HttpService()
        {
            
        }

        public async Task<FactoryResponseDto?> GetFactory(int factoryExternalId)
        {
            using var httpClient = new HttpClient();
            var requestUri = ApiEndPoints.GetFactory + $"?factoryExternalId={factoryExternalId}";
            var contentResult = await httpClient.GetAsync(requestUri);
            var result = JsonConvert.DeserializeObject<FactoryResponseDto>(await contentResult.Content.ReadAsStringAsync());
            return result;
        }
        public async Task<FactoryActionsInfoResponseDto?> GetFactoryActionInfo(int factoryExternalId)
        {
            using var httpClient = new HttpClient();
            var requestUri = ApiEndPoints.GetFactoryActionsInfo + $"?factoryExternalId={factoryExternalId}";
            var contentResult = await httpClient.GetAsync(requestUri);
            var result = JsonConvert.DeserializeObject<FactoryActionsInfoResponseDto>(await contentResult.Content.ReadAsStringAsync());
            return result;
        }
    }
}
