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
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace bpp.Helpers
{
    public class InitHandler
    {
        static ILogger _logger;
        public InitHandler(ILoggerFactory logfactory)
        {
            _logger = logfactory.CreateLogger<InitHandler>();
        }

        public async Task InitJobApplication(InitBody body)
        {
            try
            {
                Job selectedjob;
                HttpResponseMessage response;
                var xinput = body.Message.Order.Items.First().Xinput;
                if (Validate(xinput))
                {
                    SelectJob(body, out selectedjob, out response);
                    OnInitBody InitResult = BuildRespons(body, selectedjob);
                    InitResult.Message.Order.Fulfillments = body.Message.Order.Fulfillments;
                    InitResult.Message.Order.Items.First().Xinput = xinput;
                    await SendResponse(body, response, InitResult);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                _logger.LogError(e.StackTrace);
            }
        }

        private bool Validate(XInput xinput)
        {
            return true; //TODO add logic
        }

        private static void SelectJob(InitBody selectBody, out Job selectedjob, out HttpResponseMessage response)
        {
            string selectedJObId = selectBody.Message.Order?.Items?.First()?.Id;
            selectedjob = null;
            HttpClient client;
            string jobs;
            response = null;
            try
            {

                var url = Environment.GetEnvironmentVariable(EnvironmentVariables.SEARCHBASEURL)?.ToString();
                url = url + BPPConstants.URL_PATH_GET_JOB_DETAIL + selectedJObId;
                client = new HttpClient();
                _logger.LogInformation(" internal job search at : " + url);

                response = client.GetAsync(url).Result;
                response.EnsureSuccessStatusCode();
                jobs = response.Content.ReadAsStringAsync().Result;
                selectedjob = Newtonsoft.Json.JsonConvert.DeserializeObject<Job>(jobs);
            }
            catch (Exception e)
            {
                _logger.LogError("Search Error");
                _logger.LogError(e.Message);
                _logger.LogError(response?.Content.ReadAsStringAsync().Result);

            }
        }

        private static OnInitBody BuildRespons(InitBody selectBody, Job selectedjob)
        {
            var selectResult = JsonConvert.DeserializeObject<OnInitBody>(File.ReadAllText(BPPConstants.STATIC_FILE_ON_INIT));
            SetContext(selectBody, selectResult);

            if (selectedjob != null)
            {
                int locid = 0;
                int fulid = 0;
                int catid = 0;


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

                if (selectedjob.jobLocationType?.Count > 0)
                {
                    selectedItem.FulfillmentIds = new List<string>();
                    foreach (var j in selectedjob.jobLocationType)
                    {
                        selectResult.Message.Order.Provider.Fulfillments.Add(new Fulfillment()
                        {
                            Id = Convert.ToString(++fulid),
                            Type = j
                        });
                        selectedItem.FulfillmentIds.Add(Convert.ToString(fulid));
                    }
                }


            }
            else
            {
                _logger.LogInformation("no job founds from open search. returning error in on select body");
                selectResult.Error = new Error() { Message = "Could not find the selected job in catalog. Please check with BPP" };

            }

            return selectResult;
        }
        private static Item MapJobtoItem(Job selectedjob)
        {

            var selectedItem = JsonConvert.DeserializeObject<Item>(File.ReadAllText(BPPConstants.STATIC_FILE_ITEM_AS_JOB));
            selectedItem.Id = selectedjob.id;
            selectedItem.Descriptor = new Descriptor() { Name = selectedjob.title, LongDesc = selectedjob.description };
            selectedItem.LocationIds = selectedItem.FulfillmentIds = new List<string>();
            selectedItem.Xinput.Form.Url = Environment.GetEnvironmentVariable(EnvironmentVariables.BPP_XINPUT_URL)
                                            + BPPConstants.URL_PATH_XINPUT_FORM_PATH + selectedjob.id;

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
        static void SetContext(InitBody query, OnInitBody result)
        {
            //var tags = new Tags() { { "", "" } };
            result.Context.BapId = query?.Context.BapId;
            result.Context.BapUri = query?.Context.BapUri;
            result.Context.MessageId = query.Context.MessageId;
            result.Context.TransactionId = query.Context.TransactionId;
            result.Context.Timestamp = DateTime.Now;
            result.Context.BppId = Environment.GetEnvironmentVariable(EnvironmentVariables.BPP_SUBSCRIBER_ID);
            result.Context.BppUri = Environment.GetEnvironmentVariable(EnvironmentVariables.BPP_URL);


        }

        private static async Task SendResponse(InitBody selectBody, HttpResponseMessage response, OnInitBody selectResult)
        {
            if (selectResult.Message.Order.Items.Count > 0)
            {
                try
                {
                    var json = JsonConvert.SerializeObject(selectResult, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

                    var data = new StringContent(json, Encoding.UTF8, BPPConstants.RESPONSE_MEDIA_TYPE);

                    var url = selectResult.Context.BapUri + BPPConstants.ON_INIT_REPOSNE_URL_PATH;
                    using var postclient = new HttpClient();
                    postclient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(
                        BPPConstants.SIGNATURE_IDENTIFIER
                        , AuthUtil.createAuthorizationHeader(json));

                    var postResponse = await postclient.PostAsync(url, data);

                    var result = await postResponse.Content.ReadAsStringAsync();
                    _logger.LogInformation(result);

                }
                catch (HttpRequestException e)
                {
                    _logger.LogError("\nException Caught!");
                    _logger.LogError("Message :{0} ", e.Message);
                }
            }
            else
            {
                _logger.LogInformation("No job found for given query TID : " + selectBody.Context.TransactionId);
            }
        }
    }
}

