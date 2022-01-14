using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace BreadCommunityWeb.Blz.Server.Extentions
{
    public static class SwaggerGenOptionsExt
    {
        public static SwaggerGenOptions IncludeXmlFile(this SwaggerGenOptions self, string folderPath, string fileName, bool includeControllerXmlComments = true)
        {
            var xmlPath = Path.Combine(folderPath, fileName);
            self.IncludeXmlComments(xmlPath, includeControllerXmlComments);
            return self;
        }
    }
}
