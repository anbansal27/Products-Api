using System.Net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Extensions.Logging;
using Serilog.Formatting.Json;
using Serilog.Sinks.RollingFileAlternate;

namespace Products.Api
{
    public class Program
    {
        public static void Main(string[] args) => CreateHostBuilder(args).Build().Run();

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                    .ConfigureLogging(
                      (hostingContext, logging) => logging.AddProvider(CreateLoggerProvdier(hostingContext.Configuration)));
                });

        private static SerilogLoggerProvider CreateLoggerProvdier(IConfiguration configuration)
        {
            var loggerConfiguration = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .WriteTo.RollingFileAlternate(new JsonFormatter(), "./logs", fileSizeLimitBytes: 100000000, retainedFileCountLimit: 30)
                .ReadFrom.Configuration(configuration)
                .Enrich.WithProperty("Hostname", Dns.GetHostName());

            return new SerilogLoggerProvider(loggerConfiguration.CreateLogger());
        }
    }
}
