using Microsoft.AspNetCore.SignalR.Client;

namespace Chatbot.App.Domain.Hubs
{
    public interface IChatRoomHubConnectionService : IDisposable
    {
        /// <summary>
        /// Configures the user name
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public IChatRoomHubConnectionService SetUserName(string userName);

        /// <summary>
        /// Configures the hub source URL
        /// </summary>
        /// <param name="hubUrl">Hub Url</param>
        public IChatRoomHubConnectionService SetHubUrl(string hubUrl);

        /// <summary>
        /// Configures the action to be performed when sending a message
        /// </summary>
        /// <param name="broadcastMessage"></param>
        public IChatRoomHubConnectionService ConfigureOnBroadcastMessage(Action<string, string> broadcastMessage);

        /// <summary>
        /// Configures the Welcome message (On Start)
        /// </summary>
        /// <param name="welcomeMessage"></param>
        /// <returns></returns>
        public IChatRoomHubConnectionService ConfigureWelcomeMessage(Func<string, string> welcomeMessage);

        /// <summary>
        /// Configures Goodbye Message (On finalize)
        /// </summary>
        /// <param name="welcomeMessage"></param>
        /// <returns></returns>
        public IChatRoomHubConnectionService ConfigureGoodbyeMessage(Func<string, string> welcomeMessage);

        /// <summary>
        /// Sends a message to the configured hub
        /// </summary>
        /// <param name="userName">chat user name</param>
        /// <param name="message">message to be send</param>
        /// <returns></returns>
        public Task BroadCastMessage(string message);

        /// <summary>
        /// Sends a notice to the chat
        /// </summary>
        /// <param name="message">display message</param>
        /// <returns></returns>
        public Task SendNotice(string message);

        /// <summary>
        /// Creates and starts a new instance of <see cref="HubConnection"/> 
        /// </summary>
        /// <returns></returns>
        public Task StartAsync();

        /// <summary>
        /// Disconects the hub
        /// </summary>
        /// <returns></returns>
        Task DisconnectAsync();
    }
}
