using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Beckn.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using search.Models;


namespace bpp.Helpers
{
    public class SelectHandler
    {

        public async void SelectAndReply(SelectBody selectBody)
        {
            try
            {
                Job selectedjob;
                HttpResponseMessage response;
                SelectJob(selectBody, out selectedjob, out response);
                OnSelectBody selectResult = BuildRespons(selectBody, selectedjob);
                await SendResponse(selectBody, response, selectResult);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }

        }

        private static void SelectJob(SelectBody selectBody, out Job selectedjob, out HttpResponseMessage response)
        {
            string selectedJObId = selectBody.Message.Order?.Items?.First()?.Id;
            selectedjob = null;
            HttpClient client;
            string jobs;
            response = null;
            try
            {

                var url = Environment.GetEnvironmentVariable("searchbaseUrl")?.ToString();
                url = url + "/getbyid/" + selectedJObId;
                client = new HttpClient();
                Console.WriteLine(" internal job search at : " + url);

                response = client.GetAsync(url).Result;
                response.EnsureSuccessStatusCode();
                jobs = response.Content.ReadAsStringAsync().Result;
                selectedjob = Newtonsoft.Json.JsonConvert.DeserializeObject<Job>(jobs);
            }
            catch (Exception e)
            {
                Console.WriteLine("Search Error");
                Console.WriteLine(e.Message);
                Console.WriteLine(response?.Content.ReadAsStringAsync().Result);

            }
        }

        private static OnSelectBody BuildRespons(SelectBody selectBody, Job selectedjob)
        {
            var selectResult = JsonConvert.DeserializeObject<OnSelectBody>(File.ReadAllText("StaticFiles/onSelect.json"));
            SetContext(selectBody, selectResult);

            if (selectedjob != null)
            {
                int locid = 0;
                int fulid = 0;
                int catid = 0;
                int payid = 0;

                var categories = new List<Category>();
                foreach (var jt in selectedjob.employmentType)
                {
                    categories.Add(new Category
                    {
                        Id = Convert.ToString(++catid),
                        Descriptor = new Descriptor { Code = jt }
                    });
                }


                selectResult.Message.Order.Items = new List<Item>() { MapJobtoItem(selectedjob) };
                var selectedItem = selectResult.Message.Order.Items.First();
                selectedItem.CategoryIds = new List<string>();
                selectedItem.CategoryIds.AddRange(categories.Select(x => x.Id).ToList());
                selectedItem.Time = new Time { Range = new TimeRange { End = Convert.ToDateTime(selectedjob.validThrough), Start = Convert.ToDateTime(selectedjob.datePosted) } };

                //selectResult.Message.Order.Provider = new Provider()
                //{
                //    Descriptor = new Descriptor() { Name = selectedjob.hiringOrganization.name },
                //    Items = new List<Item>() { selectedItem },
                //    Locations = new List<Location>(),
                //    Categories = categories
                //};

                selectResult.Message.Order.Provider.Descriptor = new Descriptor() { Name = selectedjob.hiringOrganization.name };

                selectResult.Message.Order.Provider.Locations.Add(new Location() { Id = Convert.ToString(++locid), City = new City() { Name = selectedjob.jobLocation.address.addressRegion } });
                selectedItem.LocationIds = new List<string>() { Convert.ToString(locid) };

                if (selectedjob.skills.Count > 0)
                {
                    var tagGroup = new TagGroup { Descriptor = new Descriptor() { Code = "skills" }, _List = new List<Tag>() };

                    foreach (var s in selectedjob.skills)
                    {
                        tagGroup._List.Add(new Tag { Descriptor = new Descriptor() { Code = s } });
                    }
                    selectResult.Message.Order.Provider.Items?.First().Tags.Add(tagGroup);
                }


            }
            else
            {
                Console.WriteLine("no job founds from open search. returning error in on select body");
                selectResult.Error = new Error() { Message = "Could not find the selected job in catalog. Please check with BPP" };

            }

            return selectResult;
        }

        private static Item MapJobtoItem(Job selectedjob)
        {
            int locId = 0;
            var selectedItem = JsonConvert.DeserializeObject<Item>(File.ReadAllText("StaticFiles/ItemAsJob.json"));
            selectedItem.Id = selectedjob.id;
            selectedItem.Descriptor = new Descriptor() { Name = selectedjob.title, LongDesc = selectedjob.description };

            foreach (var r in selectedjob.responsibilities)
            {
                var tagGroup = selectedItem.Tags.Where(T => T.Descriptor.Name == "Responsibilities")?.First();
                tagGroup._List = new List<Tag>() { new Tag() { Value = r } };
            }
            foreach (var p in selectedjob.salary.pay)
            {
                var tagGroup = selectedItem.Tags.Where(T => T.Descriptor.Code == "salary-info")?.First();
                tagGroup._List = new List<Tag>() {
                    new Tag() { Value = Convert.ToString(p.maxValue), Descriptor = new Descriptor() { Name = p.type.ToString() } }
                    };
            }


            return selectedItem;
        }

        private static async Task SendResponse(SelectBody selectBody, HttpResponseMessage response, OnSelectBody selectResult)
        {
            if (selectResult.Message.Order.Items.Count > 0)
            {
                try
                {
                    var json = JsonConvert.SerializeObject(selectResult, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

                    var data = new StringContent(json, Encoding.UTF8, "application/json");

                    var url = selectResult.Context.BapUri + "on_select";
                    using var postclient = new HttpClient();
                    postclient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Signature", AuthUtil.createAuthorizationHeader(json));

                    var postResponse = await postclient.PostAsync(url, data);

                    var result = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(result);

                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine("\nException Caught!");
                    Console.WriteLine("Message :{0} ", e.Message);
                }
            }
            else
            {
                Console.WriteLine("No job found for given query TID : " + selectBody.Context.TransactionId);
            }
        }
        static void SetContext(SelectBody query, OnSelectBody result)
        {
            //var tags = new Tags() { { "", "" } };
            result.Context.BapId = query?.Context.BapId;
            result.Context.BapUri = query?.Context.BapUri;
            result.Context.MessageId = query.Context.MessageId;
            result.Context.TransactionId = query.Context.TransactionId;
            result.Context.Timestamp = DateTime.Now;


        }
    }
}

