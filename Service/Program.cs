using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Sepidar.Service;
using Sepidar.Framework;

namespace Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, builder) =>
                    {
                        // The following line set the BasePath to bin folder because Configuration in Startup could not find appsettings.json file
                        builder.SetBasePath(AppDomain.CurrentDomain.BaseDirectory);
                    })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
