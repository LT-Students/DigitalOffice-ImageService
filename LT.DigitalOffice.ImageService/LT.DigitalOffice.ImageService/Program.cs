using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LT.DigitalOffice.ImageService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            string seqServerUrl = Environment.GetEnvironmentVariable("seqServerUrl");
            if (string.IsNullOrEmpty(seqServerUrl))
            {
                seqServerUrl = configuration["Serilog:WriteTo:1:Args:serverUrl"];
            }

            string seqApiKey = Environment.GetEnvironmentVariable("seqApiKey");
            if (string.IsNullOrEmpty(seqApiKey))
            {
                seqApiKey = configuration["Serilog:WriteTo:1:Args:apiKey"];
            }

            Log.Logger = new LoggerConfiguration().ReadFrom
                .Configuration(configuration)
                .Enrich.WithProperty("Service", "ImageService")
                .WriteTo.Seq(
                    serverUrl: seqServerUrl,
                    apiKey: seqApiKey)
                .CreateLogger();

            try
            {
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception exc)
            {
                Log.Fatal(exc, "Can not properly start ImageService.");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
