using Chatbot.App.Domain.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using System.Globalization;

namespace Chatbot.App.Infrastructure.Hubs
{
    public class ChatRoomHubConnectionService : IChatRoomHubConnectionService
    {
        private const string DEFAULT_CHATROOM_URL = "/chat";
        private const string BROADCAST_METHOD = "Broadcast";
        private Action<string, string> _broadCastMessage;
        private Func<string, string> _goodbyeMessage;
        private Func<string, string> _welcomeMessage;
        private string _hubUrl;
        private HubConnection _hub;
        private bool _disposed = false;
        private string _userName = "";

        public ChatRoomHubConnectionService()
        {
            _goodbyeMessage = (userName) => GoodByeMessage(userName);
            _welcomeMessage = (userName) => WelcomeMessage(userName);
        }

        /// <inheritdoc/>
        public IChatRoomHubConnectionService SetUserName(string userName)
        {
            _userName = userName;

            return this;
        }

        /// <inheritdoc/>
        public IChatRoomHubConnectionService ConfigureOnBroadcastMessage(Action<string, string> broadcastMessage)
        {
            _broadCastMessage = broadcastMessage;

            return this;
        }

        /// <inheritdoc/>
        public IChatRoomHubConnectionService ConfigureWelcomeMessage(Func<string, string> welcomeMessage)
        {
            _welcomeMessage = welcomeMessage;
            return this;
        }

        /// <inheritdoc/>
        public IChatRoomHubConnectionService ConfigureGoodbyeMessage(Func<string, string> goodbyeMessage)
        {
            _goodbyeMessage = goodbyeMessage;
            return this;
        }

        /// <inheritdoc/>
        public async Task BroadCastMessage(string message)
        {
            //string sentAt = DateTime.Now.ToString(CultureInfo.InvariantCulture.DateTimeFormat.ShortTimePattern);
            await GetHubSafety().SendAsync(BROADCAST_METHOD, _userName, message);
        }

        /// <inheritdoc/>
        public async Task SendNotice(string message) => await BroadCastMessage($"[Notice] {message}");

        /// <inheritdoc/>
        public IChatRoomHubConnectionService SetHubUrl(string hubUrl)
        {
            _hubUrl = hubUrl.TrimEnd('/') + DEFAULT_CHATROOM_URL;

            return this;
        }

        /// <inheritdoc/>
        public async Task StartAsync()
        {
            _hub = new HubConnectionBuilder()
                .WithUrl(GetHubUrlSafety())
                .Build();

            _hub.On<string, string>(BROADCAST_METHOD, _broadCastMessage);

            await _hub.StartAsync();

            await SendNotice(_welcomeMessage.Invoke(_userName));
        }

        private string GetHubUrlSafety() => string.IsNullOrWhiteSpace(_hubUrl) ? throw new Exception($"{nameof(_hubUrl)} cannot be blank") : _hubUrl;
        private HubConnection GetHubSafety() => _hub ?? throw new Exception($"{nameof(_hub)} needs to be an valid reference, starts the hub first");
        private static string WelcomeMessage(string userName) => $"{userName} joined chat room.";
        private static string GoodByeMessage(string userName) => $"{userName} left chat room.";

        /// <inheritdoc/>
        public async Task DisconnectAsync()
        {
            try
            {
                if (_goodbyeMessage != null) await SendNotice(_goodbyeMessage.Invoke(_userName));
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                await FinalizeHub();
            }
        }

        private async Task FinalizeHub()
        {
            await GetHubSafety().StopAsync();
            await GetHubSafety().DisposeAsync();
            _hub = null;
        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                Task.WaitAny(FinalizeHub());
                _disposed = true;
            }
        }
    }
}
