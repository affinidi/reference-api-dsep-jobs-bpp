using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using BAP.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using HiringOrganization = BAP.Models.HiringOrganization;
using Job = BAP.Models.Job;
using JobLocation = BAP.Models.JobLocation;

namespace BAP.Services
{
	public class DataRetrievalService
	{
		public DataRetrievalService()
		{

		}

        internal List<Job> GetDataForTransactionID(string operation, string transationid, string messageid)
        {
            List<Job> joblist = new List<Job>();
            HttpClient client;
            string jobs;
            HttpResponseMessage response = null;
            try
            {
             
                var url = Environment.GetEnvironmentVariable("RetrieveUrl")?.ToString();
                var uri = new Uri(url).AddParameter(
                    ("transactionid",transationid),
                    ("messageid",messageid),
                    ("action",operation)
                    );

                Console.WriteLine("The retrieve URL is " + uri.AbsoluteUri);
                client = new HttpClient();
               
                response = client.GetAsync(uri).Result;
                response.EnsureSuccessStatusCode();
                jobs = response.Content.ReadAsStringAsync().Result;

              if(operation=="on_search")
                {
                    joblist.AddRange(GetJobsFromBPPMessage(jobs));
                }
              else if (operation=="on_select")
                {
                    joblist.Add(GetJobDetails(jobs));
                }

                
            }
            catch (Exception e)
            {
                Console.WriteLine("Search Error");
                Console.WriteLine(e.Message);
                Console.WriteLine(response?.Content.ReadAsStringAsync().Result);

            }

            return joblist;
        }

   
        internal string SearchOnBPP(EUAPayload payload)
        {
            string messageId = Guid.NewGuid().ToString("n");
            List<Location> pLocations = new List<Location>();
            foreach(var l in payload?.locations)
            {
                pLocations.Add(new Location { City  = new City { Name = l } });
            }
            TagGroup tagGroup = new TagGroup() { Code = "func_skilss", _List = new List<Tag>() };

            if (payload.skills?.Count>0)
            {
               
                foreach(var s in payload.skills)
                {
                    tagGroup._List.Add(new Tag { Code = s });
                }
            }
          
            var searchBody = JsonConvert.DeserializeObject<SearchBody>(File.ReadAllText("StaticFiles/SearchContext.json"));
            searchBody.Context.MessageId = messageId;
            searchBody.Context.TransactionId = payload.transactionId;
            searchBody.Context.Timestamp = new DateTime();
            searchBody.Message.Intent.Provider = string.IsNullOrEmpty(payload.provider)? searchBody.Message.Intent.Provider: new Provider() { Descriptor = new Descriptor { Name = payload.provider }
            , Locations= new List<Location>()};
            searchBody.Message.Intent.Item = string.IsNullOrEmpty(payload.title)? searchBody.Message.Intent.Item: new Item { Descriptor = new Descriptor { Name = payload.title }, Tags= new List<TagGroup> { tagGroup} };
           
            if (searchBody.Message.Intent.Provider !=null)
            {
                
                searchBody.Message.Intent.Provider.Locations.AddRange(pLocations);
               
            }
           
            HttpResponseMessage response = null;
            var json = JsonConvert.SerializeObject(searchBody, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
         
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var url = Environment.GetEnvironmentVariable("BGUrl")?.ToString();
            var client = new HttpClient();


            response = client.PostAsync(url+"/search",data).Result;
            response.EnsureSuccessStatusCode();
            var result  = response.Content.ReadAsStringAsync().Result;


            var ack = JsonConvert.DeserializeObject<Ack>(result);


            return messageId;
    }

        internal string SelectOnBPP(EUAPayload payload)
        {
            string messageId = Guid.NewGuid().ToString("n");
          
            var selectBody = JsonConvert.DeserializeObject<SelectBody>(File.ReadAllText("StaticFiles/selectBody.json"));
            selectBody.Context.MessageId = messageId;
            selectBody.Context.TransactionId = payload.transactionId;
            selectBody.Context.Timestamp = new DateTime();
            selectBody.Message.Order.item = new Item { Id = payload.jobid };
            
            HttpResponseMessage response = null;
            var json = JsonConvert.SerializeObject(selectBody, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var url = Environment.GetEnvironmentVariable("BPPURL")?.ToString();
            var client = new HttpClient();

            response = client.PostAsync(url + "/select", data).Result;
            response.EnsureSuccessStatusCode();
            var result = response.Content.ReadAsStringAsync().Result;


            var ack = JsonConvert.DeserializeObject<Ack>(result);


            return messageId;
        }

        private IEnumerable<Job> GetJobsFromBPPMessage(string jobs)
        {
            var BAPJobs = new List<Job>();
            try
            {


                var JsonObject = JObject.Parse(jobs);
                var searchResult = JsonObject["Item"]["result"]["message"]["catalog"];
                var catalogs = JsonConvert.DeserializeObject<Catalog>(searchResult.ToString());

             
                foreach (var p in catalogs.Providers)
                {
                    foreach (var i in p.Items)
                    {
                        BAPJobs.Add(
                          new Job
                          {
                              id = i.Id,
                              title = i.Descriptor.Name,
                              hiringOrganization = new HiringOrganization { name = p.Descriptor.Name },
                              description = i.Descriptor.ShortDesc,
                              jobLocation = new JobLocation { name = string.Join(",", p.Locations.Where(x => i.LocationIds.Contains(x.Id)).Select(x => x.City.Name).ToList()) },
                              baseSalary = new BaseSalary { currency = i.Price?.Currency, value = new Value { maxValue = Convert.ToInt32(i.Price.MaximumValue), minValue = Convert.ToInt32(i.Price.MinimumValue) } },
                              employmentType= p.Categories.Where(x=>i.CategoryIds.Contains(x.Id)).Select(y=>string.Join("",y.Descriptor.Code.Split("_")).ToLowerInvariant()).ToList(),
                              datePosted= i.Time.Range.Start.ToString(),
                              validThrough= Convert.ToDateTime(i.Time.Range.End)
                          }
                      );

                    }

                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }

            return BAPJobs;
        }

        private Job GetJobDetails(string jobs)
        {
            var JsonObject = JObject.Parse(jobs);
            var searchResult = JsonObject["Item"]["result"]["message"]["order"];
            var selectOrder = JsonConvert.DeserializeObject<Order>(searchResult.ToString());

            var BAPJobs = new Job();
            var resultJOb = selectOrder.Provider.Items.First();
            

            BAPJobs.id = resultJOb.Id;
            BAPJobs.title = resultJOb.Descriptor.Name;
            BAPJobs.description = resultJOb.Descriptor.LongDesc;
            BAPJobs.skills = new List<string>();
            BAPJobs.jobLocation = new JobLocation { name = selectOrder.Provider.Locations?.First().City?.Name };
            BAPJobs.hiringOrganization = new HiringOrganization { name = selectOrder.Provider.Descriptor.Name };
            BAPJobs.baseSalary = new BaseSalary { currency = resultJOb.Price?.Currency, value = new Value { maxValue = Convert.ToInt32(resultJOb.Price.MaximumValue), minValue = Convert.ToInt32(resultJOb.Price.MinimumValue) } };
            BAPJobs.employmentType = selectOrder.Provider.Categories.Where(x => resultJOb.CategoryIds.Contains(x.Id)).Select(y => string.Join("", y.Descriptor.Code.Split("_")).ToLowerInvariant()).ToList();
            BAPJobs.datePosted = resultJOb.Time?.Range?.Start?.ToString();
            BAPJobs.validThrough = Convert.ToDateTime(resultJOb.Time.Range.End);
            BAPJobs.responsibilities = resultJOb.AddOns.Where(x => x.Descriptor.Code == "Responsibilities").Select(x => x.Descriptor.LongDesc).ToList()[0].Split("|").ToList();

            foreach (var t in selectOrder.Provider.Items?.First()?.Tags?.Where(x => x.Code == "skill" || x.Code == "func_skills")?.First()?._List)
            {
                BAPJobs.skills.Add(t.Code);
            }

            return BAPJobs;
        }

        internal string ConfirmOnDsep(EUAPayload payload)
        {
            string messageId = Guid.NewGuid().ToString("n");

            var confirmBody = JsonConvert.DeserializeObject<ConfirmBody>(File.ReadAllText("StaticFiles/ConfirmBody.json"));
            confirmBody.Context.MessageId = messageId;
            confirmBody.Context.TransactionId = payload.transactionId;
            confirmBody.Context.Timestamp = new DateTime();
            confirmBody.Message.Order.item = new Item { Id = payload.jobid };
            foreach (var c in payload.credentials)
            {
                confirmBody.Message.Order.Fulfillment.Customer.Person.Creds.Add(c);

            }

            confirmBody.Message.Order.Fulfillment.Customer.Person.Creds = confirmBody.Message.Order.Fulfillment.Customer.Person.Creds.Where(x=> !string.IsNullOrEmpty(x.Id)).ToList();

            HttpResponseMessage response = null;
            var json = JsonConvert.SerializeObject(confirmBody, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var url = Environment.GetEnvironmentVariable("BPPURL")?.ToString();
            var client = new HttpClient();

            response = client.PostAsync(url + "/confirm", data).Result;
            response.EnsureSuccessStatusCode();
            var result = response.Content.ReadAsStringAsync().Result;


            var ack = JsonConvert.DeserializeObject<Ack>(result);


            return messageId;
        }
    }
}

