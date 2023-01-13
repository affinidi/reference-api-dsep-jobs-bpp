using System;
using System.Net.Http;
using System.Text;
using bpp.Models;
using Newtonsoft.Json;
using search.Models;

namespace bpp.Helpers
{
    public class ConfirmHandler
    {
        public ConfirmHandler()
        {
        }

        internal void SaveApplication(ConfirmBody body)
        {
            var application = new Application(body.Message.Order.item.Id);
            application.transactionid = body.Context.TransactionId;
            application.orderId = body.Message.Order?.Id;
            application.id = DateTimeOffset.Now.ToUnixTimeSeconds().ToString();//Guid.NewGuid().ToString("n");
            application.person = body.Message?.Order?.Fulfillment?.Customer?.Person;
            application.contact= body.Message?.Order?.Fulfillment?.Customer?.Contact;
            application.docs= body.Message?.Order?.Documents;


          
            HttpResponseMessage response = null;
            try
            {
             
                SetJobTitle(application);

                var json = JsonConvert.SerializeObject(application, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                Console.WriteLine(" application Details");
                Console.WriteLine(json);

                var url = Environment.GetEnvironmentVariable("searchbaseUrl")?.ToString();
                url = url + "/saveapplication";
                using var client = new HttpClient();

                response = client.PostAsync(url, data).Result;

                var result = response.Content.ReadAsStringAsync();
                Console.WriteLine(result);


            }
            catch (Exception e)
            {
                Console.WriteLine("Search Error");
                Console.WriteLine(e.Message);
                Console.WriteLine(response?.Content.ReadAsStringAsync().Result);

            }
        }

        private static void SetJobTitle(Application application)
        {
            var url = Environment.GetEnvironmentVariable("searchbaseUrl")?.ToString();
            url = url + "/getbyid/" + application.jobid;
         
            Console.WriteLine(" internal job search at : " + url);

            using var client = new HttpClient();
            var response = client.GetAsync(url).Result;
            response.EnsureSuccessStatusCode();
            var jobs = response.Content.ReadAsStringAsync().Result;
            var selectedjob = Newtonsoft.Json.JsonConvert.DeserializeObject<Job>(jobs);
            application.jobTitle = selectedjob.title;
        }
    }
}

