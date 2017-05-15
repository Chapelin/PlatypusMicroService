using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microphone.Consul;
using Microsoft.Extensions.Options;
using Microphone;

namespace MicroServicesArticles
{
    public class Program
    {
        private const string ConsulIP = "consul";
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .UseApplicationInsights()
                .Build();

            var options = new ConsulOptions();
            options.Host = ConsulIP;
            options.HealthCheckPath = "/api/values";
            var loggerFactory = new LoggerFactory();
            var logger = loggerFactory.CreateLogger("logger");
            var provider = new ConsulProvider(loggerFactory, Options.Create(options));
            Cluster.RegisterService(new Uri(string.Format("http://{0}", Environment.MachineName)), provider, "articles", "v1", logger);
            host.Run();
        }
    }
}
