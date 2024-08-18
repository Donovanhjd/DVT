﻿using DVT.Domain.Entities;
using DVT.Infrastructure.Data;
using DVT.Infrastructure.Interfaces;
using DVT.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DVT.ConsoleApp
{
    public class Startup
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<InMemoryDatabase>(options =>
                options.UseInMemoryDatabase("InMemoryDb"));

            services.AddScoped<IRepository<Elevator>, ElevatorRepository>();
        }
    }
}
