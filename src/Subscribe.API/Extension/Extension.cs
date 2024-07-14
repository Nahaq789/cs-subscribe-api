using Microsoft.EntityFrameworkCore;

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
    }
}