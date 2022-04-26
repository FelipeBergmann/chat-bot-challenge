using Chatbot.App.Domain.Hubs;
using Chatbot.App.Infrastructure.Hubs;
using Microsoft.Extensions.DependencyInjection;

namespace Chatbot.App.Infrastructure.IoC
{
    public static class Container
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            services.AddTransient<IChatRoomHubConnectionService, ChatRoomHubConnectionService>();

            return services;
        }
    }
}