using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Cache.CacheManager;
using Ocelot.Values;

var builder = WebApplication.CreateBuilder(args);


builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
    {
        config.AddJsonFile($"ocelot.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true);
    })
    .ConfigureLogging((hostingContext, loggingbuilder) =>
    {
        loggingbuilder.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
        loggingbuilder.AddConsole();
        loggingbuilder.AddDebug();
    });

builder.Services.AddOcelot()
                .AddCacheManager(settings => settings.WithDictionaryHandle());





var app = builder.Build();

app.UseRouting();

app.MapGet("/", () => "Hello World!");

await app.UseOcelot();

app.Run();
