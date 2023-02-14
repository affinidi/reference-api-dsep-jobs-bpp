using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Beckn.Models;
using bpp.Models;
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
                application.id = aplicationID = DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString();//Guid.NewGuid().ToString("n");
                var fulfillment = body.Message?.Order?.Fulfillments.Where(x => x.Id == item.FulfillmentIds.FirstOrDefault()).FirstOrDefault();
                application.person = fulfillment?.Customer?.Person;
                application.contact = fulfillment?.Customer?.Contact;
                // application.docs = body.Message?.Order?;



                HttpResponseMessage response = null;
                try
                {

                    var jobDetails = SetJobTitle(application);

                    var json = JsonConvert.SerializeObject(application, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                    var data = new StringContent(json, Encoding.UTF8, "application/json");
                    Console.WriteLine(" application Details : " + json);

                    var url = Environment.GetEnvironmentVariable("searchbaseUrl")?.ToString();
                    url = url + "/applications";
                    using var client = new HttpClient();

                    response = client.PostAsync(url, data).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var result = response.Content.ReadAsStringAsync().Result;
                        Console.WriteLine("ApplicationId is : " + result);

                        await SendConfirmation(application, body, jobDetails);
                    }
                    else
                    {
                        await SendError(body);
                    }





                }
                catch (Exception e)
                {
                    Console.WriteLine("Error While saving application");
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

        private static async Task SendConfirmation(Application application, ConfirmBody body, Job jobDetails)
        {
            int locid = 0;
            int fulid = 0;

            var onConfirmBody = JsonConvert.DeserializeObject<OnConfirmBody>(File.ReadAllText("StaticFiles/OnConfirmBody.json"));
            onConfirmBody.Context.MessageId = body.Context.MessageId;
            onConfirmBody.Context.TransactionId = body.Context.TransactionId;
            onConfirmBody.Context.Timestamp = DateTime.Now;
            onConfirmBody.Context.BapId = body.Context.BapId;
            onConfirmBody.Context.BapUri = body.Context.BapUri;
            onConfirmBody.Context.BppId = Environment.GetEnvironmentVariable("bpp_subscriber_id");
            onConfirmBody.Context.BppUri = Environment.GetEnvironmentVariable("bpp_url");
            onConfirmBody.Message.Order.Id = application.id;
            onConfirmBody.Message.Order.Items.Add(MapJobtoItem(jobDetails));
            var selectedItem = onConfirmBody.Message.Order.Items.First();
            selectedItem.Time = new Time
            {
                Range = new TimeRange
                {
                    End = Convert.ToDateTime(jobDetails.validThrough),
                    Start = Convert.ToDateTime(jobDetails.datePosted)
                }
            };

            onConfirmBody.Message.Order.Provider.Descriptor = new Descriptor() { Name = jobDetails.hiringOrganization.name };

            onConfirmBody.Message.Order.Provider.Locations.Add(new Location() { Id = Convert.ToString(++locid), City = new City() { Name = jobDetails.jobLocation.address.addressRegion } });
            selectedItem.LocationIds = new List<string>() { Convert.ToString(locid) };
            if (jobDetails.jobLocationType?.Count > 0)
            {
                selectedItem.FulfillmentIds = new List<string>();
                foreach (var j in jobDetails.jobLocationType)
                {
                    onConfirmBody.Message.Order.Provider.Fulfillments.Add(new Fulfillment()
                    {
                        Id = Convert.ToString(++fulid),
                        Type = j
                    });
                    selectedItem.FulfillmentIds.Add(Convert.ToString(fulid));
                }
            }

            onConfirmBody.Message.Order.Fulfillments = new List<Fulfillment>()
            {
                new Fulfillment()
                {
                    Id="1",
                    Customer= new Customer(){Person= application.person },
                    State = new FulfillmentState()
                    {
                        Descriptor = new Descriptor()
                        {
                            Name = application.state.ToString(),
                            Code = application.state.ToString()
                        }
                    }
            }

            };

            onConfirmBody.Message.Order.Xinput = body.Message.Order.Xinput;

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

        private static Job SetJobTitle(Application application)
        {
            var url = Environment.GetEnvironmentVariable("searchbaseUrl")?.ToString();
            url = url + "/jobs/" + application.jobid;

            Console.WriteLine(" internal job search for job ID  : " + application.jobid);

            using var client = new HttpClient();
            var response = client.GetAsync(url).Result;
            response.EnsureSuccessStatusCode();
            var jobs = response.Content.ReadAsStringAsync().Result;
            var selectedjob = Newtonsoft.Json.JsonConvert.DeserializeObject<Job>(jobs);
            application.jobTitle = selectedjob.title;
            return selectedjob;
        }

        private static Item MapJobtoItem(Job selectedjob)
        {

            var selectedItem = JsonConvert.DeserializeObject<Item>(File.ReadAllText("StaticFiles/ItemAsJob.json"));
            selectedItem.Id = selectedjob.id;
            selectedItem.Descriptor = new Descriptor() { Name = selectedjob.title, LongDesc = selectedjob.description };
            selectedItem.LocationIds = selectedItem.FulfillmentIds = new List<string>();

            if (selectedjob.responsibilities?.Count > 0)
            {
                var tagGroup = selectedItem.Tags.Where(T => T.Descriptor.Name == "Responsibilities")?.First();
                tagGroup._List = new List<Tag>();
                foreach (var r in selectedjob.responsibilities)
                {

                    tagGroup._List.Add(new Tag() { Value = r });
                }

            }

            if (selectedjob.salary.pay?.Count > 0)
            {
                var tagGroup = selectedItem.Tags.Where(T => T.Descriptor.Code == "salary-info")?.First();
                tagGroup._List = new List<Tag>();

                foreach (var p in selectedjob.salary.pay)
                {

                    tagGroup._List.Add(new Tag()
                    {
                        Value = Convert.ToString(p.maxValue),
                        Descriptor = new Descriptor() { Name = p.type.ToString() }
                    });



                }

            }
            if (selectedjob.qualifications?.Count > 0)
            {
                foreach (var q in selectedjob.qualifications)
                {
                    var tagGroup = selectedItem.Tags.Where(T => T.Descriptor.Name == q.type)?.First();
                    tagGroup._List = new List<Tag>();
                    foreach (var v in q.values)
                    {
                        tagGroup._List.Add(new Tag()
                        {
                            Descriptor = new Descriptor() { Code = v.kind, Name = v.kind },
                            Value = v.value
                        }); ;
                    }
                }
            }

            if (selectedjob.employmentType.Count > 0)
            {
                var tagGroup = selectedItem.Tags.Where(t => t.Descriptor.Code == "employment-info")?.First();
                tagGroup._List = new List<Tag>();

                foreach (var et in selectedjob.employmentType)
                {
                    tagGroup._List.Add(new Tag()
                    {
                        Descriptor = new Descriptor() { Code = "emp-duration-type", Name = "Employment Duration Type" },
                        Value = et
                    });
                }
            }

            if (selectedjob.skills.Count > 0)
            {
                var tagGroup = selectedItem.Tags.Where(t => t.Descriptor.Code == "Skills")?.First();
                tagGroup._List = new List<Tag>();

                foreach (var s in selectedjob.skills)
                {
                    tagGroup._List.Add(new Tag { Descriptor = new Descriptor() { Code = s } });
                }

            }


            return selectedItem;
        }
    }
}

