using AutoMapper;
using BreadCommunityWeb.Blz.Server.Extentions;
using BreadCommunityWeb.EventNotificator.Application.Interfaces.Services;
using BreadCommunityWeb.EventNotificator.Infrastructure.Server.Configurations;
using BreadCommunityWeb.EventNotificator.Infrastructure.Server.Extentions;
using BreadCommunityWeb.EventNotificator.Infrastructure.Server.Interfaces;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace BreadCommunityWeb.EventNotificator
{
    public class Startup
    {
        private MapperConfiguration _mapperConfig;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region MapperConfiguration
            _mapperConfig = new MapperConfiguration(cfg => cfg.AddMaps("BreadCommunityWeb.EventNotificator.Infrastructure.Server"));
            services.AddSingleton(s => _mapperConfig.CreateMapper());
            #endregion

            services.AddSingleton(Configuration.GetSection("DbConnectionConfig").Get<DbConnectionConfig>());
            services.AddSingleton(Configuration.GetSection("TelegramConnectionConfig").Get<TelegramConnectionConfig>());
            services.RegistrateServices(Configuration);

            services.AddControllers();
            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new OpenApiInfo { Title = "BreadCommunityWeb.EventNotificator", Version = "v1" });
            //});

            services.AddAutoMapper(typeof(Startup).Assembly);
            services
               .AddProblemDetails(ConfigureProblemDetails)
               .AddSwaggerGen(ConfigureSwaggerGenOptions)
               .ConfigureSwaggerGen(options =>
               {
                   options
                       .IncludeXmlFile(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");
               });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app, 
            IWebHostEnvironment env,
            IConnectNotificatorService connectNotificatorService,
            IReportNotificatorService reportNotificatorService)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BreadCommunityWeb.EventNotificator v1"));
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            connectNotificatorService.Start(new CancellationTokenSource());
            reportNotificatorService.Start(new CancellationTokenSource());

            app.UseRouting();

            //app.UseAuthorization();
            app.UseSwagger()
              .UseSwaggerUI(c =>
              {
                  c.SwaggerEndpoint("/swagger/v1/swagger.json",
                      $"{Assembly.GetExecutingAssembly().GetName().Name}");
              })
              .UseStaticFiles()
              .UseRouting()
              .UseAuthentication()
              .UseAuthorization()
               .UseEndpoints(endpoints =>
               {
                   endpoints.MapDefaultControllerRoute();
               }).UseProblemDetails();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllers();
            //});
        }

        private void ConfigureProblemDetails(ProblemDetailsOptions options)
        {

            options.OnBeforeWriteDetails = (ctx, problem) =>
            {
                problem.Extensions["traceId"] = ctx.TraceIdentifier;
                problem.Extensions["remoteIp"] = ctx.Connection.RemoteIpAddress;
                problem.Extensions["problem"] = problem.Detail;
            };
            options.MapToStatusCode<Exception>(StatusCodes.Status500InternalServerError);

        }
        private void ConfigureSwaggerGenOptions(SwaggerGenOptions options)
        {
            var ver = GetType()!.Assembly!.GetName()!.Version!.ToString();
            options.SwaggerDoc("v1", new OpenApiInfo { Title = $"{Assembly.GetExecutingAssembly().GetName().Name}", Version = ver });
            options.DocumentFilter<SwaggerAddEnumDescriptions>();
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);

        }
    }
}
