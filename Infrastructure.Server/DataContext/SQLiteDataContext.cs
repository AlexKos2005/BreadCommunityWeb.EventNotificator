using BreadCommunityWeb.EventNotificator.Domain;
using BreadCommunityWeb.EventNotificator.Infrastructure.Server.Configurations;
using BreadCommunityWeb.EventNotificator.Infrastructure.Server.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreadCommunityWeb.EventNotificator.Infrastructure.Server.DataContext
{
    public class SQLiteDataContext : DbContext
    {
        private readonly DbConnectionConfig _dbConnection;
        public DbSet<FactoryNotificationsInfo> FactoryNotificationsInfos { get; set; }
        public DbSet<ConnectNotificationChatInfo> ConnectNotificationChatInfos { get; set; }
        public DbSet<ReportNotificationChatInfo> ReportNotificationChatInfos { get; set; }

        public DbSet<ConnectNotificationsStatistic> ConnectNotificationsStatistic { get; set; }

        public DbSet<ReportNotificationsStatistic> ReportNotificationsStatistic { get; set; }

        public DbSet<ConnectNotificatorServiceConfig> ConnectNotificatorServiceConfig { get; set; }

        public DbSet<ReportNotificatorServiceConfig> ReportNotificatorServiceConfig { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_dbConnection.ConnectionString);
        }

        public SQLiteDataContext(DbConnectionConfig dbConnection)
        {
            _dbConnection = dbConnection;
            Database.EnsureCreated();
        }
    }
}
