﻿using DrinkParty.EntityFramework;
using DrinkParty.Features;
using DrinkParty.Features.Players;
using DrinkParty.Features.Rooms;
using Microsoft.Extensions.DependencyInjection;

namespace DrinkParty
{
    public static class ServiceExtension
    {
        public static void ConfigureAppServices(this IServiceCollection services)
        {
            services.AddScoped<RoomService>();
            services.AddScoped<PlayerService>();
            services.AddScoped<GameService>();
            services.AddTransient<TransactionService>();
        }
    }
}
