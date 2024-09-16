using Microsoft.EntityFrameworkCore;
using super_exchange.Server.Client;
using super_exchange.Server.Database;
using super_exchange.Server.Facade;
using super_exchange.Server.Mapper;
using System.Reflection;

namespace super_exchange.Server.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<ExchangeDatabaseContext>(options => options.UseSqlServer(config.GetConnectionString("SuperExchange")));
        return services;
    }

    public static IServiceCollection AddClients(this IServiceCollection services)
    {
        services.AddHttpClient();
        services.AddTransient<ITableClient, NbpTableClient>();

        return services;
    }

    public static IServiceCollection AddMappers(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddTransient<IRateMapper, RateMapper>();
        return services;
    }
    public static IServiceCollection AddFacades(this IServiceCollection services)
    {
        services.AddScoped<ICurrencyFacade, CurrencyFacade>();

        return services;
    }
}
