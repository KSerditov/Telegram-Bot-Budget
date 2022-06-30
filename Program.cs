using Serilog;

namespace TelegramBotBudget;

class Program
{
    static async Task Main(string[] args)
    {
        try
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .Enrich.FromLogContext()
                .WriteTo.File("log.txt", rollOnFileSizeLimit: true, fileSizeLimitBytes: 1000000, retainedFileCountLimit: 10)
                .CreateLogger();

            IHost host = Host.CreateDefaultBuilder(args)
                .UseSystemd()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<TelegramService>();
                    services.AddSingleton<GoogleSheetsHelper>();
                    services.AddSingleton<SpendingWriter>();
                    services.AddSingleton<Category>();
                    services.AddSingleton<InitialHandlerService>();
                    services.AddSingleton<TelegramHandlerService>();
                })
                .UseSerilog()
                .Build();

            await host.RunAsync();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Host terminated unexpectedly");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}
