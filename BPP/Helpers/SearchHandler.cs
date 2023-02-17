﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Beckn.Models;
using bpp.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using search.Models;


namespace bpp.Helpers
{
    public class SearchHandler
    {
        STDCodeHelper _STDCodeHelper;
        NetworkParticipantCache _networkParticipantCache;
        static ILogger _logger;
        public SearchHandler(STDCodeHelper sTDCodeHelper, NetworkParticipantCache networkParticipantCache, ILoggerFactory logfactory)
        {
            _STDCodeHelper = sTDCodeHelper;
            _networkParticipantCache = networkParticipantCache;
            _logger = logfactory.CreateLogger<SearchHandler>();
        }

        public async Task SearchAndReply(SearchBody query)
        {

            // await Task.Run(() => {

            _logger.LogInformation("performing search on search query ");

            OnSearchBody on_searchData = searchBppDB(query);

            await Reply(query, on_searchData);

            // });

        }

        private static async Task Reply(SearchBody query, OnSearchBody on_searchData)
        {

            try
            {
                string json = JsonConvert.SerializeObject
                    (
                    on_searchData,
                    Formatting.Indented,
                    new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }
                    );
                StringContent data = new StringContent(json, Encoding.UTF8, BPPConstants.RESPONSE_MEDIA_TYPE);
                string url = query.Context.BapUri + BPPConstants.ON_SEARCH_REPOSNE_URL_PATH; ;

                _logger.LogInformation("Rsponding to onsearch API at URL : " + url);
                using var client = new HttpClient();
                client.Timeout = new TimeSpan(0, 0, 10);
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue
                    (
                    BPPConstants.SIGNATURE_IDENTIFIER,
                    AuthUtil.createAuthorizationHeader(json)
                    );
                HttpResponseMessage response = await client.PostAsync(url, data);
                string result = await response.Content.ReadAsStringAsync();
                _logger.LogInformation("response from BAP API for on_search : " + result);

            }
            catch (HttpRequestException e)
            {
                _logger.LogInformation("\nException Caught!");
                _logger.LogInformation("Message :{0} ", e.Message);
            }

        }

        private OnSearchBody searchBppDB(SearchBody query)
        {
            OnSearchBody result = JsonConvert.DeserializeObject<OnSearchBody>(File.ReadAllText(BPPConstants.STATIC_FILE_ON_SEARCH));
            try
            {
                List<Query> allQueries = new List<Query>();

                if (query.Message.Intent?.Item != null)
                {
                    FindItemRelatedSearchQuery(query, allQueries);
                }
                else
                {
                    var searchQuery = new Query();
                    GetProviders(query, searchQuery);
                    allQueries.Add(searchQuery);

                }

                //actual search
                List<Job> jobs = new List<Job>();

                foreach (var q in allQueries)
                {
                    jobs.AddRange(ActualSearch(q));
                }
                if (jobs.Count > 0)
                {

                    jobs = jobs.GroupBy(x => x.id).Select(j => j.First()).ToList();
                    _logger.LogInformation("Responding to BAP with jobs. Total Jobs found for current query {0} is {1}", query.Context.TransactionId, jobs.Count);

                    SetContext(query, result);
                    CreateSearchResult(result, jobs);
                }
                //search end
                else
                {
                    SetContext(query, result);
                    result.Error = new Error() { Message = "No Job found for current query: TID : " + query.Context.TransactionId };
                }



            }
            catch (Exception e)
            {
                _logger.LogInformation("\nException Caught in internal job search!");
                _logger.LogInformation("exception message " + e.Message);
                _logger.LogInformation(e.StackTrace);
            }

            return result;

            static List<Job> ActualSearch(Query searchQuery)
            {
                List<Job> joblist = new List<Job>();
                HttpClient client;
                string jobs;
                HttpResponseMessage response = null;
                try
                {
                    var json = JsonConvert.SerializeObject(searchQuery);
                    _logger.LogInformation("***query : " + json);
                    var data = new StringContent(json, Encoding.UTF8, BPPConstants.RESPONSE_MEDIA_TYPE);

                    var url = Environment.GetEnvironmentVariable(EnvironmentVariables.SEARCHURL)?.ToString();
                    _logger.LogInformation("search URL is : " + url);
                    client = new HttpClient();
                    _logger.LogInformation(" internal job search at : " + url);
                    _logger.LogInformation("search with query data : " + JsonConvert.SerializeObject(data));
                    response = client.PostAsync(url, data).Result;
                    response.EnsureSuccessStatusCode();
                    jobs = response.Content.ReadAsStringAsync().Result;
                    joblist.AddRange(JsonConvert.DeserializeObject<List<Job>>(jobs));
                    _logger.LogInformation("Total Jobs for current search query is {0}: ", joblist.Count);
                }
                catch (Exception e)
                {
                    _logger.LogInformation("Search Error");
                    _logger.LogInformation(e.Message);
                    _logger.LogInformation(response?.Content.ReadAsStringAsync().Result);

                }

                return joblist;
            }

            static void SetContext(SearchBody query, OnSearchBody result)
            {
                //var tags = new Tags() { { "", "" } };
                result.Context.BapId = query?.Context.BapId;
                result.Context.BapUri = query?.Context.BapUri;
                result.Context.MessageId = query.Context.MessageId;
                result.Context.TransactionId = query.Context.TransactionId;
                result.Context.Timestamp = DateTime.Now;
                result.Context.BppId = Environment.GetEnvironmentVariable(EnvironmentVariables.BPP_SUBSCRIBER_ID);
                result.Context.BppUri = Environment.GetEnvironmentVariable(EnvironmentVariables.BPP_URL);

                result.Message.Catalog.Providers = new List<Provider>();
            }

            //void FindProviderRelatedSearchItems(SearchBody query, Query searchQuery)
            //{

            //}

            void FindItemRelatedSearchQuery(SearchBody query, List<Query> allQueries)
            {
                //foreach (var item in query.Message.Intent.Item ?? Enumerable.Empty<Item>())
                //{
                var searchQuery = new Query();
                searchQuery.title = query.Message.Intent.Item?.Descriptor?.Name;
                foreach (var t in query.Message.Intent.Item.Tags ?? Enumerable.Empty<TagGroup>())
                {
                    if (t.Descriptor.Code == "skills")
                    {
                        searchQuery.skills.AddRange(t._List.Select(x => x.Descriptor.Code));
                    }
                }

                //searchQuery.joblocation.Add(query.Message.Intent.Fulfillment.Where(x => x.Id == item.FulfillmentId).Select(x => x.Start.Location.City.Name)?.ToList()[0].ToString());
                //searchQuery.joblocation.Add(query.Message.Intent.Fulfillment.Select(x => x.Start).Where(x => x.Location.Id == item.LocationId).Select(x => x.Location.City.Name)?.ToList()[0].ToString());
                //searchQuery.interviewLocation.Add(query.Message.Intent.Fulfillment.Where(x => x.Id == item.FulfillmentId).Select(x => x.Type)?.ToList()[0].ToString());
                //searchQuery.jobtype.Add(query.Message.Intent.Fulfillment.Where(x => x.Id == item.FulfillmentId).Select(x => x.Type)?.ToList()[0].ToString());

                GetProviders(query, searchQuery);
                allQueries.Add(searchQuery);
                //}
            }

            static void CreateSearchResult(OnSearchBody result, List<Job> jobs)
            {
                int locid = 0;
                int providerId = 0;


                result.Message.Catalog.Payments = new List<Payment>();

                try
                {
                    foreach (var job in jobs)
                    {
                        var item = new Item()
                        {
                            Id = job.id,
                            Descriptor = new Descriptor() { Name = job.title, LongDesc = job.description },
                            LocationIds = new List<string> { Convert.ToString(++locid) },
                            //Time = new Time { Range = new TimeRange { End = Convert.ToDateTime(job.validThrough), Start = Convert.ToDateTime(job.datePosted) } }


                        };

                        var provider = result.Message.Catalog.Providers.Count == 0 ?
                            result.Message.Catalog.Providers :
                            result.Message.Catalog.Providers.Where(x => x.Descriptor.Name.Contains(job.hiringOrganization.name));

                        if (provider?.Count() > 0)
                        {
                            var pr = provider.First();
                            pr.Items.Add(item);
                            pr.Locations.Add(new Location()
                            {
                                Id = Convert.ToString(locid),
                                City = new City() { Name = job.jobLocation?.address?.addressRegion },
                                State = new State() { Name = "" },
                                Country = new Country() { Name = "" }
                            });
                            result.Message.Catalog.Providers.Add(pr);
                        }
                        else
                        {
                            var pr = new Provider()
                            {
                                Id = Convert.ToString(++providerId),
                                Descriptor = new Descriptor() { Name = job.hiringOrganization.name },
                                Items = new List<Item>() { item },
                                Locations = new List<Location>(){new Location()
                                {
                                    Id = Convert.ToString(locid),
                                    City = new City() { Name = job.jobLocation?.address?.addressRegion },
                                    State = new State() { Name = "" },
                                    Country = new Country() { Name = "" }
                                }
                                }
                            };
                            result.Message.Catalog.Providers.Add(pr);
                        }


                    }
                }
                catch (Exception e)
                {
                    _logger.LogInformation(e.Message);
                    _logger.LogInformation(e.StackTrace);
                }
            }
        }

        private void GetProviders(SearchBody query, Query searchQuery)
        {
            if (query.Message.Intent.Provider != null)
            {
                var pr = new ProviderQuery() { name = query.Message.Intent.Provider.Descriptor?.Name };


                //var locationFromCOntext = _STDCodeHelper.GetCity(query.Context.City.Split(':')[1]);
                //if (!string.IsNullOrEmpty(locationFromCOntext))
                //{
                //    pr.locations.Add(locationFromCOntext);
                //}

                foreach (var loc in query.Message.Intent.Provider.Locations ?? Enumerable.Empty<Location>())
                {
                    pr.locations.Add(loc.City?.Name);
                }

                searchQuery.provider.Add(pr);



            }
        }
    }
}

