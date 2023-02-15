using System;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Collections.Generic;
using bpp.Models;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace bpp.Helpers
{
    public class NetworkParticipantCache
    {
        List<NetworkParticipant> networkCache;
        ILogger _logger;
        public NetworkParticipantCache(ILoggerFactory logfactory)
        {
            _logger = logfactory.CreateLogger<NetworkParticipant>();

            var json = "{}";
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = "https://registry.becknprotocol.io/subscribers/lookup";
            using var client = new HttpClient();
            //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("authorization", AuthUtil.createAuthorizationHeader(json));
            var response = client.PostAsync(url, data).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            networkCache = JsonConvert.DeserializeObject<List<NetworkParticipant>>(result);
            if (networkCache != null)
            {
                _logger.LogInformation("Total participants : " + networkCache.Count);
                _logger.LogInformation("BPPs : " + networkCache.Where(x => x.type == "BPP").Count());
                _logger.LogInformation("BAPs : " + networkCache.Where(x => x.type == "BAP").Count());
            }
        }
    }
}

