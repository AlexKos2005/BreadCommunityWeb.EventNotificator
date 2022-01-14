using AutoMapper;
using BreadCommunityWeb.EventNotificator.Application.Dto;
using BreadCommunityWeb.EventNotificator.Application.Dto.RequestDto;
using BreadCommunityWeb.EventNotificator.Application.Dto.ResponseDto;
using BreadCommunityWeb.EventNotificator.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreadCommunityWeb.EventNotificator.Infrastructure.Server.Mapping
{
    public class SourceMappingProfiles : Profile
    {
        public SourceMappingProfiles()
        {
            #region ConnectNotifications
            var map1 = CreateMap<ConnectNotificationChatInfo, ConnectNotificationChatInfoRequestDto>().ReverseMap();
            map1.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
            map1.ForMember(dest => dest.ChatId, opt => opt.MapFrom(src => src.ChatId));

            var map2 = CreateMap<ConnectNotificationChatInfo, ConnectNotificationChatInfoResponseDto>().ReverseMap();
            map2.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
            map2.ForMember(dest => dest.ChatId, opt => opt.MapFrom(src => src.ChatId));

            var map3 = CreateMap<ConnectNotificatorServiceConfig, ConnectNotificatorServiceConfigDto>();
            map3.ForMember(dest => dest.IsEnable, opt => opt.MapFrom(src => src.IsEnable));
            map3.ForMember(dest => dest.ConnectionCheckPeriod, opt => opt.MapFrom(src => new TimeSpan(src.ConnectionCheckPeriod)));
            map3.ForMember(dest => dest.MaxNotificationsCount, opt => opt.MapFrom(src => src.MaxNotificationsCount));
            map3.ForMember(dest => dest.StartCheckReportDayTime, opt => opt.MapFrom(src => new TimeSpan(src.StartCheckReportDayTime)));
            map3.ForMember(dest => dest.MaxLackConnectionTime, opt => opt.MapFrom(src => new TimeSpan(src.MaxLackConnectionTimeTicks)));

            var map31 = CreateMap<ConnectNotificatorServiceConfigDto, ConnectNotificatorServiceConfig>();
            map31.ForMember(dest => dest.IsEnable, opt => opt.MapFrom(src => src.IsEnable));
            map31.ForMember(dest => dest.ConnectionCheckPeriod, opt => opt.MapFrom(src => src.ConnectionCheckPeriod.Ticks));
            map31.ForMember(dest => dest.MaxNotificationsCount, opt => opt.MapFrom(src => src.MaxNotificationsCount));
            map31.ForMember(dest => dest.StartCheckReportDayTime, opt => opt.MapFrom(src => src.StartCheckReportDayTime.Ticks));
            map31.ForMember(dest => dest.MaxLackConnectionTimeTicks, opt => opt.MapFrom(src => src.MaxLackConnectionTime.Ticks));


            var map4 = CreateMap<ConnectNotificatorServiceConfig, ConnectNotificatorServiceConfigResponseDto>();
            map4.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
            map4.ForMember(dest => dest.IsEnable, opt => opt.MapFrom(src => src.IsEnable));
            map4.ForMember(dest => dest.ConnectionCheckPeriod, opt => opt.MapFrom(src => new TimeSpan(src.ConnectionCheckPeriod)));
            map4.ForMember(dest => dest.MaxNotificationsCount, opt => opt.MapFrom(src => src.MaxNotificationsCount));
            map4.ForMember(dest => dest.MaxLackConnectionTime, opt => opt.MapFrom(src => new TimeSpan(src.MaxLackConnectionTimeTicks)));
            map4.ForMember(dest => dest.StartCheckReportDayTime, opt => opt.MapFrom(src => new TimeSpan(src.StartCheckReportDayTime)));

            var map41 = CreateMap<ConnectNotificatorServiceConfigResponseDto, ConnectNotificatorServiceConfig>();
            map41.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
            map41.ForMember(dest => dest.IsEnable, opt => opt.MapFrom(src => src.IsEnable));
            map41.ForMember(dest => dest.ConnectionCheckPeriod, opt => opt.MapFrom(src => src.ConnectionCheckPeriod.Ticks));
            map41.ForMember(dest => dest.MaxLackConnectionTimeTicks, opt => opt.MapFrom(src => src.MaxLackConnectionTime.Ticks));
            map41.ForMember(dest => dest.StartCheckReportDayTime, opt => opt.MapFrom(src => src.StartCheckReportDayTime.Ticks));
            map4.ForMember(dest => dest.MaxNotificationsCount, opt => opt.MapFrom(src => src.MaxNotificationsCount));
            #endregion

            #region ConnectNotificationsStatistic
            var map11 = CreateMap<ConnectNotificationsStatistic, ConnectNotificationsStatisticRequestDto>().ReverseMap();
            var map13 = CreateMap<ConnectNotificationsStatistic, ConnectNotificationsStatisticResponseDto>().ReverseMap();
            #endregion

            #region ReportNotifications
            var map5 = CreateMap<ReportNotificationChatInfo, ReportNotificationChatInfoRequestDto>().ReverseMap();
            map5.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
            map5.ForMember(dest => dest.ChatId, opt => opt.MapFrom(src => src.ChatId));

            var map6 = CreateMap<ReportNotificationChatInfo, ReportNotificationChatInfoResponseDto>().ReverseMap();
            map6.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
            map6.ForMember(dest => dest.ChatId, opt => opt.MapFrom(src => src.ChatId));

            var map7 = CreateMap<ReportNotificatorServiceConfig, ReportNotificatorServiceConfigDto>();
            map7.ForMember(dest => dest.IsEnable, opt => opt.MapFrom(src => src.IsEnable));
            map7.ForMember(dest => dest.StartCheckReportDayTime, opt => opt.MapFrom(src => new TimeSpan(src.StartCheckReportDayTime)));
            map7.ForMember(dest => dest.ReportCheckPeriod, opt => opt.MapFrom(src => new TimeSpan(src.ReportCheckPeriod)));
            map7.ForMember(dest => dest.MaxNotificationsCount, opt => opt.MapFrom(src => src.MaxNotificationsCount));

            var map71 = CreateMap<ReportNotificatorServiceConfigDto, ReportNotificatorServiceConfig>();
            map71.ForMember(dest => dest.IsEnable, opt => opt.MapFrom(src => src.IsEnable));
            map71.ForMember(dest => dest.StartCheckReportDayTime, opt => opt.MapFrom(src => src.StartCheckReportDayTime.Ticks));
            map71.ForMember(dest => dest.ReportCheckPeriod, opt => opt.MapFrom(src => src.ReportCheckPeriod.Ticks));
            map71.ForMember(dest => dest.MaxNotificationsCount, opt => opt.MapFrom(src => src.MaxNotificationsCount));

            var map8 = CreateMap<ReportNotificatorServiceConfig, ReportNotificatorServiceConfigResponseDto>();
            map8.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
            map8.ForMember(dest => dest.IsEnable, opt => opt.MapFrom(src => src.IsEnable));
            map8.ForMember(dest => dest.StartCheckReportDayTime, opt => opt.MapFrom(src => new TimeSpan(src.StartCheckReportDayTime)));
            map8.ForMember(dest => dest.ReportCheckPeriod, opt => opt.MapFrom(src => new TimeSpan(src.StartCheckReportDayTime)));
            map8.ForMember(dest => dest.MaxNotificationsCount, opt => opt.MapFrom(src => src.MaxNotificationsCount));

            var map81 = CreateMap<ReportNotificatorServiceConfigResponseDto, ReportNotificatorServiceConfig>();
            map81.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
            map81.ForMember(dest => dest.IsEnable, opt => opt.MapFrom(src => src.IsEnable));
            map81.ForMember(dest => dest.StartCheckReportDayTime, opt => opt.MapFrom(src => src.StartCheckReportDayTime.Ticks));
            map81.ForMember(dest => dest.ReportCheckPeriod, opt => opt.MapFrom(src => src.ReportCheckPeriod.Ticks));
            map81.ForMember(dest => dest.MaxNotificationsCount, opt => opt.MapFrom(src => src.MaxNotificationsCount));
            #endregion

            #region ReportNotificationsStatistic
            var map12 = CreateMap<ReportNotificationsStatistic, ReportNotificationsStatisticRequestDto>().ReverseMap();
            var map14 = CreateMap<ReportNotificationsStatistic, ReportNotificationsStatisticResponseDto>().ReverseMap();
            #endregion

            #region FactoryNotifications
            var map9 = CreateMap<FactoryNotificationsInfo, FactoryNotificationsInfoRequestDto>().ReverseMap();
            map9.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
            map9.ForMember(dest => dest.IsEnable, opt => opt.MapFrom(src => src.IsEnable));
            map9.ForMember(dest => dest.FactoryExternalId, opt => opt.MapFrom(src => src.FactoryExternalId));

            var map10 = CreateMap<FactoryNotificationsInfo, FactoryNotificationsInfoResponseDto>().ReverseMap();
            map10.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
            map10.ForMember(dest => dest.IsEnable, opt => opt.MapFrom(src => src.IsEnable));
            map10.ForMember(dest => dest.FactoryExternalId, opt => opt.MapFrom(src => src.FactoryExternalId));
            map10.ForMember(dest => dest.ConnectNotificationChats, opt => opt.MapFrom(src => src.ConnectNotificationChats));
            map10.ForMember(dest => dest.ReportNotificationChats, opt => opt.MapFrom(src => src.ReportNotificationChats));
            #endregion
        }

    }
}
