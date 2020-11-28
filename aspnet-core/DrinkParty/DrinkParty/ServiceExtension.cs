using DrinkParty.EntityFramework;
using DrinkParty.Features.Rooms;
using DrinkParty.Hubs;
using DrinkParty.Hubs.Contracts;
using DrinkParty.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DrinkParty
{
    public static class ServiceExtension
    {
        public static void ConfigureAppServices(this IServiceCollection services)
        {
            //services.AddScoped<GameService>();
            //services.AddScoped<RoomRepository>();
            //services.AddScoped<PlayerRepository>();
            //services.AddScoped<PlayerSessionService>();
            //services.AddScoped<IGameHubFrontend, GameHub>();
            //services.AddTransient<TransactionService>();
        }
    }
}
