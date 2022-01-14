using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreadCommunityWeb.EventNotificator.Application.EndPoints
{
    public static class ApiEndPoints
    {
        public static string GetFactory => "http://92.63.192.191:5000/api/admin/factory/getfactorybyexternalId";
        public static string GetFactoryActionsInfo => "http://92.63.192.191:5000/api/factoryactionsinfo/getfactoryactionsinfo";
    }
}
