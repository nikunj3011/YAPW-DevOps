using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;
using YAPW.Service.Broker.Interfaces;
using YAPW.Service.Broker.Watchers;

namespace YAPW.Service.Broker.Hubs
{
    public class VideoHub : Hub<IVIdeoHub>
    {
        private readonly VideosWatcher _tripEventsWatcher;

        public VideoHub(VideosWatcher tripEventsWatcher)
        {
            _tripEventsWatcher = tripEventsWatcher;
        }
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.ReceiveMessage(user, message);
        }

        public async Task StartWatching()
        {
            _tripEventsWatcher.RgisterSqlDependencyEvents(Context.ConnectionId);
            _tripEventsWatcher.StartWatching();

            //await Clients.Caller.Notify("Sql dependency events registered for trip table");
            //await Clients.Caller.Notify("Sql dependency started on trips table");
        }

        public override async Task OnConnectedAsync()
        {
            var connectionId = Context.ConnectionId;
            await Groups.AddToGroupAsync(connectionId, "SignalR Users");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            //_alarmEventsWatcher.StopWatching();

            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "SignalR Users");
            await base.OnDisconnectedAsync(exception);
        }
    }
}
