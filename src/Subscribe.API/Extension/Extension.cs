using Microsoft.EntityFrameworkCore;
using Npgsql;
using Subscribe.API.Application.Query.Subscribe;
using Subscribe.Domain.Model.SubscribeAggregate;
using Subscribe.Infrastructure.Context;
using Subscribe.Infrastructure.Repositories;

public static class Extension
{
    public static void StartupDI(this WebApplicationBuilder builder)
    {
        var configure = builder.Configuration;
        var connectionString = configure.GetConnectionString("subscribedb");

        builder.Services.AddDbContextPool<SubscribeContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });

        builder.Services.AddSingleton(NpgsqlDataSource.Create(connectionString));

        var services = builder.Services;
        services.AddMediatR((cfg) =>
        {
            cfg.RegisterServicesFromAssemblyContaining(typeof(Program));
        });

        builder.Services.AddTransient<ISubscribeRepository, SubscribeRepository>();

        builder.Services.AddScoped<ISubscribeQueries, SubscribeQueries>();
    }
}