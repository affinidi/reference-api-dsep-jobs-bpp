using Beckn.Models;
using bpp.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using bpp.Models;
using System.Net.Http;
using static System.Net.Mime.MediaTypeNames;

namespace bpp
{
    internal class DSEPSigVerification
    {

        NetworkParticipantCache _networkParticipantCache;
        private readonly RequestDelegate _next;
        ILogger _logger;


        public DSEPSigVerification(NetworkParticipantCache networkParticipantCache, RequestDelegate next, ILoggerFactory logfactory)
        {
            _networkParticipantCache = networkParticipantCache;
            _next = next;
            _logger = logfactory.CreateLogger<DSEPSigVerification>();
        }
        public async Task InvokeAsync(HttpContext context)
        {
            bool verifySignature = Convert.ToBoolean(Environment.GetEnvironmentVariable(EnvironmentVariables.VERIFY_SIGNATURE));
            bool verifyProxySignature = Convert.ToBoolean(Environment.GetEnvironmentVariable(EnvironmentVariables.VERIFY_PROXY_SIGNATURE));
            bool ValidSignature = false;
            bool validProxySignature = false;
            Context dsepContext;


            if (verifySignature && context.Request.Method == "POST")
            {

                string requestBody = await ReadAndStoreRequestBody(context.Request);
                _logger.LogInformation("New request  : " + requestBody);
                // Check if header exists
                if (!context.Request.Headers.ContainsKey("Authorization"))
                {
                    try
                    {
                        ValidSignature = false;
                        //var dsepContext = JsonConvert.DeserializeObject<Context>(requestBody);
                        dsepContext = JsonConvert.DeserializeObject<Context>(JObject.Parse(requestBody)["context"].ToString());

                        _logger.LogInformation("Authorization header is missing for request : " + JsonConvert.SerializeObject(dsepContext));
                        await UnAuthorizedResponse(context);
                        return;

                    }
                    catch (Exception e)
                    {
                        _logger.LogError(e.Message);
                    }

                }
                dsepContext = JsonConvert.DeserializeObject<Context>(JObject.Parse(requestBody)["context"].ToString());

                if (dsepContext.Action.ToUpper() == "search".ToUpper() && verifyProxySignature)
                {
                    if (context.Request.Headers.ContainsKey("x-gateway-authorization"))
                    {
                        validProxySignature = VerifySignature(context.Request.Headers["x-gateway-authorization"], requestBody);
                    }
                    else
                    {
                        await UnAuthorizedResponse(context);
                        return;
                    }
                }
                else
                {
                    validProxySignature = true;
                }


                if (!string.IsNullOrEmpty(context.Request.Headers["Authorization"]))
                {
                    try
                    {
                        // Validate header value
                        string headerValue = context.Request.Headers["Authorization"];
                        _logger.LogInformation($"Auth Header : {headerValue}");
                        ValidSignature = VerifySignature(headerValue, requestBody);
                    }
                    catch (Exception e)
                    {
                        _logger.LogError(e.Message);
                    }
                }

                if (validProxySignature && ValidSignature)
                {
                    _logger.LogInformation($"request from  {dsepContext.BapId} has been Authenticated");
                    // Call the next middleware/handler in the pipeline
                    await _next(context);
                }
                else
                {
                    _logger.LogInformation($"Authentication failed for request from  {dsepContext.BapId} d");
                    await UnAuthorizedResponse(context);
                    return;
                }
            }
            else
            {
                _logger.LogInformation("Authentication is disbaled");
                await _next(context);
            }

            // Call the next middleware/handler in the pipeline

        }

        private bool VerifySignature(string HedaerValue, string requestBody)
        {
            try
            {
                var parts = SplitHeader(HedaerValue);

                if (parts.Keys.Count == 0)
                {
                    return false;
                }

                var subscriberId = Regex.Unescape(parts["keyId"].Split('|')[0]);

                var uniqueKeyId = parts["keyId"].Split('|')[1];

                var subscriberDetails = GetSubscriberDetails(subscriberId, uniqueKeyId);
                if (subscriberDetails?.pub_key_id != null)
                {
                    //var publicKey = subscriberDetails.signing_public_key;
                    //SingingString sg = AuthUtil.createSigningString(requestBody, parts["created"], parts["expires"]);
                    //var signature = parts["signature"];
                    //// var s = AuthUtil.VerifySignature(publicKey, sg.signing_string, signature);
                    //var r = AuthUtil.Verify(signature, sg.signing_string, publicKey);
                }
                else
                {
                    _logger.LogError($"network participant details not found for subscriber ID {subscriberId} and unique key ID {uniqueKeyId}");
                    return false;
                }



            }
            catch (Exception e)
            {

            }


            return true;



            //if (headerValue.StartsWith("Signature"))
            //{
            //    headerValue = context.Request.Headers["Authorization"].ToString().Split("Signature")[1];
            //    _logger.LogInformation("Verifying Signature of network request:");

            //    //get the key value pair
            //    var signaturePair = headerValue.Split(',')
            //        .Select(value => value.Split('='))
            //        .ToDictionary(pair => pair[0], pair => pair[1]);


            //    //ToDO
            //    //Verify the signature as defined here
            //    //https://developers.becknprotocol.io/docs/infrastructure-layer-specification/authentication/signature-verification/


            //    ValidSignature = true;

            //}
        }

        private NetworkParticipant GetSubscriberDetails(string subscriberId, string uniqueKeyId)
        {

            NetworkParticipant requestDetails = new NetworkParticipant() { subscriber_id = subscriberId, unique_key_id = uniqueKeyId };
            var json = JsonConvert.SerializeObject(requestDetails, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            var data = new StringContent(json, Encoding.UTF8, BPPConstants.RESPONSE_MEDIA_TYPE);

            var url = Environment.GetEnvironmentVariable(EnvironmentVariables.DSEP_REGISTRY_URL);
            //"https://registry.becknprotocol.io/subscribers/lookup";
            using var client = new HttpClient();
            //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("authorization", AuthUtil.createAuthorizationHeader(json));
            var response = client.PostAsync(url, data).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            requestDetails = JsonConvert.DeserializeObject<List<NetworkParticipant>>(result)?.FirstOrDefault();
            return requestDetails;
        }

        private Task getSubscriberDetails(string subscriberId, string uniqueKeyId)
        {
            throw new NotImplementedException();
        }

        private Dictionary<string, string> SplitHeader(string hedaerValue)
        {
            hedaerValue = hedaerValue.Replace("Signature ", "");
            var signaturePair = hedaerValue.Split(',')
                    .Select(value => value.Split('='))
                    .ToDictionary(pair => pair[0]?.Trim(), pair => pair[1].Replace("\"", string.Empty)?.Trim());


            return signaturePair;
        }

        private static async Task UnAuthorizedResponse(HttpContext context)
        {
            context.Response.StatusCode = 401; // Unauthorized
            await context.Response.WriteAsync("{\n  \"message\": {\n     \"ack\": {\n    \"status\": \"NACK\"\n    }\n  }\n}");
            return;
        }

        private async Task<string> ReadAndStoreRequestBody(HttpRequest request)
        {
            string body = string.Empty;
            try
            {
                request.EnableBuffering();
                var buffer = new MemoryStream();
                await request.Body.CopyToAsync(buffer);
                buffer.Seek(0, SeekOrigin.Begin);
                body = string.Empty;

                using (var reader = new StreamReader(buffer, Encoding.UTF8, true, 1024, true))
                {
                    body = await reader.ReadToEndAsync();
                }
                buffer.Seek(0, SeekOrigin.Begin);
                request.Body = buffer;


            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            return body;
        }
    }
}