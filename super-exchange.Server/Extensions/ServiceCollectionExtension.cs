using Microsoft.EntityFrameworkCore;
using super_exchange.Server.Database;

namespace super_exchange.Server.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<ExchangeDatabaseContext>(options => options.UseSqlServer(config.GetConnectionString("SuperExchange")));
        return services;
    }
}
