﻿using System;
using Microsoft.Extensions.Logging;

namespace Microphone
{
    public static class Cluster
    {
        public static IClusterClient Client { get; private set; }

        public static void RegisterService(Uri uri, IClusterProvider clusterProvider, string serviceName, string version, ILogger log, params string[] tags)
        {
            var port = uri.Port;
            var host = uri.Host;
            var publicUri = new Uri($"{uri.Scheme}://{host}:{port}",UriKind.Absolute);
                 
            log.LogInformation("Bootstrapping Microphone");
            var serviceId = $"{serviceName}_{EscapeHost(host)}_{port}";
            try
            {
                clusterProvider.RegisterServiceAsync(serviceName, serviceId, version, publicUri,tags).Wait();
            }
            catch
            {
                log.LogError($"Could not register service '{serviceId}'");
                throw;
            }
            Client = clusterProvider;
        }

        internal static string EscapeHost(string host)
        {
            return host.Replace(".", "_");
        }
    }
}
