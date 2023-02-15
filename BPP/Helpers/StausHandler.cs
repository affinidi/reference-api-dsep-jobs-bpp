using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Beckn.Models;
using bpp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using search.Models;
using static System.Net.Mime.MediaTypeNames;

namespace bpp.Helpers
{
    public class StausHandler
    {
        static ILogger _logger;
        StatusBody _statusBody;
        Job _job;
        public StausHandler(ILoggerFactory loggerfactory)
        {
            _logger = loggerfactory.CreateLogger<StausHandler>();
        }

        internal void SendStatus(StatusBody body)
        {
            try
            {


                _statusBody = body;
                string applicationId = body.Message.OrderId;
                var applicationDetails = GetApplication(applicationId);
                RespondOnStatus(applicationDetails);
            }
            catch (Exception e)
            {
                _logger.LogError(" Error in  Status API  " + e.Message);
                _logger.LogError(e.StackTrace);
            }
        }



        private Application GetApplication(string applicationId)
        {
            //var application = new Application("dummmy");
            var url = Environment.GetEnvironmentVariable("searchbaseUrl")?.ToString();
            url = url + "/applications/" + applicationId;

            _logger.LogInformation(" internal search for Application ID  : " + applicationId);

            using var client = new HttpClient();
            var response = client.GetAsync(url).Result;
            response.EnsureSuccessStatusCode();
            var Application = response.Content.ReadAsStringAsync().Result;
            var selectedApplication = Newtonsoft.Json.JsonConvert.DeserializeObject<Application>(Application);
            return selectedApplication;
        }

        private void RespondOnStatus(Application applicationDetails)
        {
            var onStatusBody = JsonConvert.DeserializeObject<OnConfirmBody>(File.ReadAllText("StaticFiles/OnStatus.json"));
            onStatusBody.Context.MessageId = _statusBody.Context.MessageId;
            onStatusBody.Context.TransactionId = _statusBody.Context.TransactionId;
            onStatusBody.Context.Timestamp = DateTime.Now;
            onStatusBody.Context.BapId = _statusBody.Context.BapId;
            onStatusBody.Context.BapUri = _statusBody.Context.BapUri;
            onStatusBody.Context.BppId = Environment.GetEnvironmentVariable("bpp_subscriber_id");
            onStatusBody.Context.BppUri = Environment.GetEnvironmentVariable("bpp_url");
            onStatusBody.Message.Order.Id = applicationDetails.id;
            onStatusBody.Message.Order.Items.Add(GetJobDetails(applicationDetails.jobid));
            onStatusBody.Message.Order.Fulfillments.Add(GetFulfillment(applicationDetails));
            onStatusBody.Message.Order.Provider = GetProvider(applicationDetails);

            if (onStatusBody.Message.Order.Items.Count > 0)
            {
                try
                {
                    var json = JsonConvert.SerializeObject(onStatusBody, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

                    var data = new StringContent(json, Encoding.UTF8, "application/json");

                    var url = _statusBody.Context.BapUri + "on_status";
                    using var postclient = new HttpClient();
                    postclient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Signature", AuthUtil.createAuthorizationHeader(json));

                    var postResponse = postclient.PostAsync(url, data).Result;

                    var result = postResponse.Content.ReadAsStringAsync().Result;
                    _logger.LogInformation("On_status call result: " + result);

                }
                catch (HttpRequestException e)
                {
                    _logger.LogError("\nException Caught in On_status API call to BAP !");
                    _logger.LogError("Message :{0} ", e.Message);
                }
            }
            else
            {
                _logger.LogInformation("No job found for given application TID : " + applicationDetails.id);
            }
        }

        private Provider GetProvider(Application applicationDetails)
        {
            return new Provider()
            {
                Id = "1",
                Descriptor = new Descriptor() { Name = _job?.hiringOrganization?.name }
            ,
                Locations = new List<Location>() { new Location (){
                    Id="1", City= new City() { Name= _job?.hiringOrganization?.address?.addressRegion} }
                }
            };


        }

        private Fulfillment GetFulfillment(Application applicationDetails)
        {
            var fulfillment = new Fulfillment() { Id = "1", Customer = new Customer() { Person = applicationDetails.person } };
            fulfillment.State = new FulfillmentState()
            {
                Descriptor = new Descriptor()
                {
                    Name = applicationDetails.state.ToString(),
                    Code = applicationDetails.state.ToString()
                }
            };

            return fulfillment;
        }

        private Item GetJobDetails(string jobid)
        {
            Item item = new Item();

            _job = FetchJob(jobid);
            item = MapJobToItem(_job);

            return item;
        }

        private Item MapJobToItem(Job job)
        {
            int locId = 0;
            var selectedItem = JsonConvert.DeserializeObject<Item>(File.ReadAllText("StaticFiles/ItemAsJob.json"));
            selectedItem.Id = job.id;
            selectedItem.Descriptor = new Descriptor() { Name = job.title, LongDesc = job.description };

            foreach (var r in job.responsibilities)
            {
                var tagGroup = selectedItem.Tags.Where(T => T.Descriptor.Name == "Responsibilities")?.First();
                tagGroup._List = new List<Tag>() { new Tag() { Value = r } };
            }
            foreach (var p in job.salary.pay)
            {
                var tagGroup = selectedItem.Tags.Where(T => T.Descriptor.Code == "salary-info")?.First();
                tagGroup._List = new List<Tag>() {
                    new Tag() { Value = Convert.ToString(p.maxValue), Descriptor = new Descriptor() { Name = p.type.ToString() } }
                    };
            }


            return selectedItem;
        }

        private Job FetchJob(string jobid)
        {
            var url = Environment.GetEnvironmentVariable("searchbaseUrl")?.ToString();
            url = url + "/jobs/" + jobid;

            _logger.LogInformation(" internal job search for job ID  : " + jobid);

            using var client = new HttpClient();
            var response = client.GetAsync(url).Result;
            response.EnsureSuccessStatusCode();
            var jobs = response.Content.ReadAsStringAsync().Result;
            var selectedjob = Newtonsoft.Json.JsonConvert.DeserializeObject<Job>(jobs);
            return selectedjob;
        }
    }
}


