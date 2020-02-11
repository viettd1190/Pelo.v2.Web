﻿using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Serilog;
using Serilog.Events;

namespace Pelo.v2.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args)
                    .Build()
                    .Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            var outputTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u4}] | {Message:l}{NewLine}{Exception}";

            return WebHost.CreateDefaultBuilder(args)
                          .UseStartup<Startup>()
                          .UseSerilog((context,
                                       configuration) =>
                                      {
                                          configuration.MinimumLevel.Debug()
                                                       .MinimumLevel.Override("Microsoft",
                                                                              LogEventLevel.Warning)
                                                       .MinimumLevel.Override("System",
                                                                              LogEventLevel.Warning)
                                                       .MinimumLevel.Override("Microsoft.AspNetCore.Authentication",
                                                                              LogEventLevel.Information)
                                                       .Enrich.FromLogContext()
                                                       .WriteTo.File(@"logs/log-.txt",
                                                                     outputTemplate: outputTemplate,
                                                                     rollingInterval: RollingInterval.Day);
                                      });
        }
    }
}
