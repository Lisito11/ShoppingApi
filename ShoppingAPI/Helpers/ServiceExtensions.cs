using System;
using Microsoft.EntityFrameworkCore;
using ShoppingAPI.Contracts;
using ShoppingAPI.Repositories;
using ShoppingAPI.Services;
using ShoppingAPI.Services.Contracts;

namespace ShoppingAPI.Helpers
{
	public static class ServiceExtensions
	{
        public static void ConfigureSqlServerContext(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config["SqlServerConnection:ShoppingDB"];
            services.AddDbContext<ShoppingDbContext>(options => options.UseSqlServer(connectionString));
        }

        public static void ConfigureRepositoryWrapper(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
        }
    }
}

