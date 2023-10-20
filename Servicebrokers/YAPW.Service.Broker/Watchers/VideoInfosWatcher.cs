using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base.Enums;
using TableDependency.SqlClient.Base.EventArgs;
using YAPW.MainDb.DbModels;
using YAPW.Service.Broker.Hubs;

namespace YAPW.Service.Broker.Watchers
{
    /// <summary>
    ///
    /// </summary>
    public class VideoInfosWatcher
    {
        #region Fields

        private static string _connectionString;
        private static string _connectionId;
        private static string _tableName = "VideoInfos";
        private SqlTableDependency<VideoInfo> sqlTableDependency;

        #endregion

        #region Properties

        private IHubContext<VideoInfoHub> Hub { get; set; }

        private SettingsData SettingsData { get; set; }

        #endregion

        #region Constructors

        public VideoInfosWatcher(IHubContext<VideoInfoHub> hub, IOptions<AppSettings> settings, IWebHostEnvironment hostingEnv)
        {
            if (settings is not null)
            {
                SettingsData = settings.Value.Environments.Where(e => e.Name.ToLower() == hostingEnv.EnvironmentName.ToLower()).Select(e => e.SettingsData).SingleOrDefault();
                _connectionString = SettingsData.ConnectionString;
            }

            Hub = hub;
            InitiateTableDependency();
        }

        #endregion

        #region Delegates

        private void OnError(object sender, ErrorEventArgs e) => Hub.Clients.Client(_connectionId).SendAsync("tableWatcherError", e.Database, e.Server, e.Message, e.Error.StackTrace);

        private void OnStatusChanged(object sender, StatusChangedEventArgs e) => Hub.Clients.Client(_connectionId).SendAsync("monitoringStarted", e.Database, e.Server, e.Status.ToString());

        private void OnNotificationReceived(object sender, RecordChangedEventArgs<VideoInfo> e)
        {
            if (e.EntityOldValues.Views != e.Entity.Views)
            {
                {
                    _ = (e.Entity.Views switch
                    {
                        var entity when entity.Equals("created", System.StringComparison.OrdinalIgnoreCase) => Hub.Clients.All.SendAsync("VideoInfoStatus",
                                                                                                                             e.Entity.Name,
                                                                                                                             e.Entity.Id,
                                                                                                                             e.Entity.BrandId),
                        _ => Hub.Clients.All.SendAsync("StatusChanged",
                                                                                                                             e.Entity.Name,
                                                                                                                             e.Entity.Id,
                                                                                                                             e.Entity.BrandId),
                    });
                }
            }
        }

        #endregion

        #region Methods

        public void RegisterOnChangeEvent() => sqlTableDependency.OnChanged += OnNotificationReceived;

        public void UnregisterOnChangeEvent() => sqlTableDependency.OnChanged -= OnNotificationReceived;

        private void InitiateTableDependency() => sqlTableDependency ??= new SqlTableDependency<VideoInfo>(_connectionString, _tableName, includeOldValues: true);

        public void RegisterSqlDependencyEvents(string connectionId)
        {
            _connectionId = connectionId;

            if (sqlTableDependency is not null)
            {
                if (sqlTableDependency.Status != TableDependencyStatus.WaitingForNotification)
                {
                    UnregisterOnChangeEvent();
                    RegisterOnChangeEvent();
                    sqlTableDependency.OnStatusChanged += OnStatusChanged;
                    sqlTableDependency.OnError += OnError;
                }
            }
        }

        public void StartWatching()
        {
            if (sqlTableDependency is not null)
            {
                if (sqlTableDependency.Status != TableDependencyStatus.WaitingForNotification)
                {
                    sqlTableDependency.Start();
                }
            }
        }

        public void StopWatching()
        {
            if (sqlTableDependency is not null)
            {
                sqlTableDependency.Stop();
            }
        }

        #endregion

        #region Helpers

        #endregion
    }
}
