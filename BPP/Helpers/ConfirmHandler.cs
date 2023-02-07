using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Beckn.Models;
using Newtonsoft.Json;
using search.Models;

namespace bpp.Helpers
{
    public class ConfirmHandler
    {
        string aplicationID = string.Empty;
        string jobId = string.Empty;
        public ConfirmHandler()
        {
        }

        internal async Task SaveApplication(ConfirmBody body)
        {


            foreach (Item item in body.Message.Order?.Items)
            {
                var application = new Application(item.Id);
                application.transactionid = body.Context.TransactionId;
                application.orderId = body.Message.Order?.Id;
                application.id = aplicationID = DateTimeOffset.Now.ToUnixTimeSeconds().ToString();//Guid.NewGuid().ToString("n");
                var fulfillment = body.Message?.Order?.Fulfillments.Where(x => x.Id == item.FulfillmentIds.FirstOrDefault()).FirstOrDefault();
                application.person = fulfillment?.Customer?.Person;
                application.contact = fulfillment?.Customer?.Contact;
                // application.docs = body.Message?.Order?;



                HttpResponseMessage response = null;
                try
                {

                    SetJobTitle(application);

                    var json = JsonConvert.SerializeObject(application, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                    var data = new StringContent(json, Encoding.UTF8, "application/json");
                    Console.WriteLine(" application Details : " + json);

                    var url = Environment.GetEnvironmentVariable("searchbaseUrl")?.ToString();
                    url = url + "/saveapplication";
                    using var client = new HttpClient();

                    response = client.PostAsync(url, data).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var result = response.Content.ReadAsStringAsync().Result;
                        Console.WriteLine("ApplicationId is : " + result);

                        await SendConfirmation(application, body);
                    }
                    else
                    {
                        await SendError(body);
                    }





                }
                catch (Exception e)
                {
                    Console.WriteLine("Search Error");
                    Console.WriteLine(e.Message);
                    Console.WriteLine(response?.Content.ReadAsStringAsync().Result);

                }

            }

        }

        private static async Task SendError(ConfirmBody body)
        {
            var onConfirmBody = JsonConvert.DeserializeObject<OnConfirmBody>(File.ReadAllText("StaticFiles/OnConfirmBody.json"));
            onConfirmBody.Context.MessageId = body.Context.MessageId;
            onConfirmBody.Context.TransactionId = body.Context.TransactionId;
            onConfirmBody.Context.Timestamp = DateTime.Now;
            onConfirmBody.Context.BapId = body.Context.BapId;
            onConfirmBody.Context.BapUri = body.Context.BapUri;
            onConfirmBody.Error = new Error() { Code = "500", Message = "Internal server while saving application. Please try again or contact BPP" };

            try
            {
                var json = JsonConvert.SerializeObject(onConfirmBody, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                Console.WriteLine("on_confirm data : " + json);
                var url = body.Context.BapUri + "on_confirm";
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Signature", AuthUtil.createAuthorizationHeader(json));
                var response = await client.PostAsync(url, data);

                var result = await response.Content.ReadAsStringAsync();
                Console.WriteLine("result for on_confirm  : " + result);

            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("Error while sending On_confirm message! : " + e.Message);
                Console.WriteLine("Message :{0} ", e.StackTrace);
            }
        }

        private static async Task SendConfirmation(Application application, ConfirmBody body)
        {

            var onConfirmBody = JsonConvert.DeserializeObject<OnConfirmBody>(File.ReadAllText("StaticFiles/OnConfirmBody.json"));
            onConfirmBody.Context.MessageId = body.Context.MessageId;
            onConfirmBody.Context.TransactionId = body.Context.TransactionId;
            onConfirmBody.Context.Timestamp = DateTime.Now;
            onConfirmBody.Context.BapId = body.Context.BapId;
            onConfirmBody.Context.BapUri = body.Context.BapUri;
            onConfirmBody.Message.Order.Id = application.id;
            onConfirmBody.Message.Order.Items.Add(new Item() { Id = application.jobid });

            try
            {
                var json = JsonConvert.SerializeObject(onConfirmBody, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                Console.WriteLine("on_confirm data : " + json);
                var url = body.Context.BapUri + "on_confirm";
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Signature", AuthUtil.createAuthorizationHeader(json));
                var response = await client.PostAsync(url, data);

                var result = await response.Content.ReadAsStringAsync();
                Console.WriteLine("result for on_confirm : " + result);

            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("Error while sending On_confirm message! : " + e.Message);
                Console.WriteLine("Message :{0} ", e.StackTrace);
            }

        }

        private static void SetJobTitle(Application application)
        {
            var url = Environment.GetEnvironmentVariable("searchbaseUrl")?.ToString();
            url = url + "/getbyid/" + application.jobid;

            Console.WriteLine(" internal job search for job ID  : " + application.jobid);

            using var client = new HttpClient();
            var response = client.GetAsync(url).Result;
            response.EnsureSuccessStatusCode();
            var jobs = response.Content.ReadAsStringAsync().Result;
            var selectedjob = Newtonsoft.Json.JsonConvert.DeserializeObject<Job>(jobs);
            application.jobTitle = selectedjob.title;
        }


    }
}

