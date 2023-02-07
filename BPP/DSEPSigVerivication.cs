using Beckn.Models;
using bpp.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

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
            string requestBody = string.Empty;
            try
            {
                requestBody = await ReadAndStoreRequestBody(context.Request);
                _logger.LogInformation("New request  : " + requestBody);
            }

            catch (Exception e)
            {
                Console.WriteLine("Error while serializing request Body :" + e.Message);
            }

            // Check if header exists
            if (!context.Request.Headers.ContainsKey("Authorization"))
            {
                //context.Response.StatusCode = 400; // Bad Request
                //await context.Response.WriteAsync("Authorization header is missing");
                //return;

                try
                {

                    //var dsepContext = JsonConvert.DeserializeObject<Context>(requestBody);
                    var dsepContext = JsonConvert.DeserializeObject<Context>(JObject.Parse(requestBody)["context"].ToString());

                    _logger.LogInformation("Authorization header is missing for request : " + JsonConvert.SerializeObject(dsepContext));

                }
                catch (Exception e)
                {
                    _logger.LogError(e.Message);
                }

            }
            else if (!string.IsNullOrEmpty(context.Request.Headers["Authorization"]))
            {
                try
                {
                    // Validate header value
                    string headerValue = context.Request.Headers["Authorization"];
                    if (!headerValue.StartsWith("Signature"))
                    {
                        _logger.LogInformation("Verifying Signature of network request:");

                        //ToDO
                        //Verify the signature as defined here
                        //https://developers.becknprotocol.io/docs/infrastructure-layer-specification/authentication/signature-verification/

                        //if not valid 
                        //context.Response.StatusCode = 401; // Unauthorized
                        //await context.Response.WriteAsync("Authorization header is invalid");
                        //return;

                    }
                }
                catch (Exception e)
                {
                    _logger.LogError(e.Message);
                }
            }

            // Call the next middleware/handler in the pipeline
            await _next(context);
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