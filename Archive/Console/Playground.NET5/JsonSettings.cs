using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace Playground.NET
{

    /********
     * {
     *      "Services": {
     *          "Synnex": {
     *              "Id": 1,
     *              "ApiEndpoint": "https://synnex.com/api"
     *              "ApiKey": "Key",
     *              "ClientName": "Client"
     *          },
     *          "Ingram": {
     *              "Id": 2,
     *              "ApiEndpoint": "https://ingram.com/api",
     *              "userName": "ingram",
     *              "password": "password"
     *          }
     *      }
     * }
     ********/
    public class JsonSettings
    {
        public Dictionary<ProviderType, ServiceOptions> Services { get; set; }

        public static JsonSettings Create(IConfigurationRoot root)
        {
            var services = new Dictionary<ProviderType, ServiceOptions>();

            var servicesSections = root.GetSection("Services").GetChildren();

            foreach (var section in servicesSections)
            {
                if (Enum.TryParse<ProviderType>(section.Key, true, out var providerType))
                {
                    switch (providerType)
                    {
                        case ProviderType.Synnex:
                            services.Add(providerType, section.Get<SynnexOptions>());
                            break;
                        case ProviderType.Ingram:
                            break;
                    }
                }
            }

            return new JsonSettings
            {
                Services = services
            };
        }
    }
    
    public enum ProviderType { Synnex, Ingram }

    /// <summary>
    ///  Abstraction Point. You can add common options like Id or api endpoint here.
    /// </summary>
    public abstract class ServiceOptions
    {
        public int Id { get; set; }

        public Uri ApiEndpoint { get; set; }
    }

    /// <summary>
    /// You can add specific options to the service here.
    /// </summary>
    public sealed class SynnexOptions : ServiceOptions
    {

        public string ApiKey { get; set; }

        public string ClientName { get; set; }

        // More Options
    }

}