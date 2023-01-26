using System;
using System.Collections;
using System.Collections.Generic;
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
            string selectedJObId = selectBody.Message.Order.Items.First().Id;
            Job selectedjob = null;
            HttpClient client;
            string jobs;
            HttpResponseMessage response = null;
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

            var selectResult = new OnSelectBody();

            if (selectedjob != null)
            {
                int locid = 0;
                int fulid = 0;
                int catid = 0;
                int payid = 0;

                selectResult.Message = new OnSelectMessage();
                selectResult.Message.Order = new Order();
                selectResult.Message.Order.Offers = new List<Offer>();
                selectResult.Message.Order.Payments = new List<Payment>() { new Payment() { Id = Convert.ToString(++payid) } };

                selectResult.Message.Order.Items = new List<Item>();

                var categories = new List<Category>();
                foreach (var jt in selectedjob.employmentType)
                {
                    categories.Add(new Category
                    {
                        Id = Convert.ToString(++catid),
                        Descriptor = new Descriptor { Code = jt }
                    });
                }

                selectResult.Context = selectBody.Context;
                selectResult.Context.Action = ActionEnum.OnSelectEnum.ToString();
                selectResult.Context.BppId = "affinidi.bpp";
                selectResult.Context.BppUri = "http://DSEP-nlb-d3ed9a3f85596080.elb.ap-south-1.amazonaws.com";

                selectResult.Message.Order.Offers.Add(new Offer() { ItemIds = new List<string>() { selectedjob.id } });

                var selectedItem = new Item() { Tags = new List<TagGroup>() };
                selectedItem.Id = selectedjob.id;
                selectedItem.Descriptor = new Descriptor() { Name = selectedjob.title, LongDesc = selectedjob.description };
                selectedItem.AddOns = new List<AddOn> { new AddOn { Descriptor = new Descriptor { Code = "Responsibilities", LongDesc = string.Join("|", selectedjob.responsibilities) } } };
                //selectedItem.AddOns = new List<AddOn> { new AddOn { Descriptor = new Descriptor { Code = "Experince", LongDesc = string.Join("|", selectedjob.responsibilities) } } };
                selectedItem.Price = new Price()
                {
                    MinimumValue = Convert.ToString(selectedjob.baseSalary?.value.minValue)
                        ,
                    MaximumValue = Convert.ToString(selectedjob.baseSalary?.value.maxValue)
                        ,
                    ListedValue = Convert.ToString(selectedjob.baseSalary?.value.maxValue)
                        ,
                    Currency = selectedjob.baseSalary?.currency
                        ,
                    OfferedValue = Convert.ToString(selectedjob.baseSalary?.value.maxValue)

                };
                selectedItem.CategoryIds = new List<string>();
                selectedItem.CategoryIds.AddRange(categories.Select(x => x.Id).ToList());
                selectedItem.Time = new Time { Range = new TimeRange { End = Convert.ToDateTime(selectedjob.validThrough), Start = Convert.ToDateTime(selectedjob.datePosted) } };

                selectResult.Message.Order.Provider = new Provider()
                {
                    Descriptor = new Descriptor() { Name = selectedjob.hiringOrganization.name },
                    Items = new List<Item>() { selectedItem },
                    Locations = new List<Location>(),
                    Categories = categories
                };
                selectResult.Message.Order.Items.Add(selectedItem);
                selectResult.Message.Order.Provider.Locations.Add(new Location() { City = new City() { Name = selectedjob.jobLocation.address.addressRegion } });

                if (selectedjob.skills.Count > 0)
                {
                    selectResult.Message.Order.Provider.Items?.First().Tags.Add(new TagGroup { Code = "skill", _List = new List<Tag>() });
                }
                selectResult.Message.Order.Provider.Items?.First().Tags.Add(new TagGroup { Code = "skill" });
                var tagGroup = selectResult.Message.Order.Provider.Items?.First().Tags.Where(x => x.Code == "skill").ToList().First();

                foreach (var s in selectedjob.skills)
                {
                    tagGroup._List.Add(new Tag { Code = s });

                }



            }
            else
            {
                Console.WriteLine("no job founds from open search");
            }


            if (selectResult.Message.Order.Offers.Count > 0)
            {
                try
                {
                    var json = JsonConvert.SerializeObject(selectResult, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

                    var data = new StringContent(json, Encoding.UTF8, "application/json");

                    var url = selectResult.Context.BapUri + "on_select";
                    using var postclient = new HttpClient();
                    postclient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("authorization", AuthUtil.createAuthorizationHeader(json));

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
    }
}

