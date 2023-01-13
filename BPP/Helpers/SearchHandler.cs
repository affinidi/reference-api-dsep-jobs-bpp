using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using bpp.Models;
using Newtonsoft.Json;
using search.Models;
using static bpp.Models.Context;

namespace bpp.Helpers
{
    public class SearchHandler
    {
        STDCodeHelper _STDCodeHelper;
        public SearchHandler(STDCodeHelper sTDCodeHelper)
        {
            _STDCodeHelper = sTDCodeHelper;
        }

        public async Task SearchAndReply(SearchBody query)
        {

            // await Task.Run(() => {

            Console.WriteLine("performing search on search query ");

            OnSearchBody on_searchData = searchBppDB(query);

            await Reply(query, on_searchData);

            // });

        }

        private static async Task Reply(SearchBody query, OnSearchBody on_searchData)
        {
            if (on_searchData.Message.Catalog.Offers.Count > 0 || on_searchData.Message.Catalog.Providers.Count>0||on_searchData.Message.Catalog.Fulfillments.Count>0)
            {
                try
                {
                    var json = JsonConvert.SerializeObject(on_searchData, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                    var data = new StringContent(json, Encoding.UTF8, "application/json");

                    var url = query.Context.BapUri + "on_search";
                    using var client = new HttpClient();

                    var response = await client.PostAsync(url, data);

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
                Console.WriteLine("No job found for given query TID : " + query.Context.TransactionId);
            }
        }

        private OnSearchBody searchBppDB(SearchBody query)
        {
            var result = new OnSearchBody();
            SetContext(query, result);
            try
            {
                List<Query> allQueries = new List<Query>();

                if (query.Message.Intent?.Item !=null)
                {
                    FindItemRelatedSearchQuery(query, allQueries);
                }else
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

                jobs = jobs.GroupBy(x => x.id).Select(j => j.First()).ToList();
                //search end

                CreateSearchResult(result, jobs);
            }
            catch (Exception e)
            {
                Console.WriteLine("\nException Caught in internal job search!");
                Console.WriteLine("exception message " + e.Message);
                Console.WriteLine(e.StackTrace);
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
                    Console.WriteLine("***query : " + json);
                    var data = new StringContent(json, Encoding.UTF8, "application/json");

                    var url = Environment.GetEnvironmentVariable("searchUrl")?.ToString();
                    Console.WriteLine("search URL is : " + url);
                    client = new HttpClient();
                    Console.WriteLine(" internal job search at : " + url);
                    Console.WriteLine("search with query data : " + JsonConvert.SerializeObject(data));
                    response = client.PostAsync(url, data).Result;
                    response.EnsureSuccessStatusCode();
                    jobs = response.Content.ReadAsStringAsync().Result;
                    joblist.AddRange(Newtonsoft.Json.JsonConvert.DeserializeObject<List<Job>>(jobs));
                }
                catch (Exception e)
                {
                    Console.WriteLine("Search Error");
                    Console.WriteLine(e.Message);
                    Console.WriteLine(response?.Content.ReadAsStringAsync().Result);

                }

                return joblist;
            }

            static void SetContext(SearchBody query, OnSearchBody result)
            {
                //var tags = new Tags() { { "", "" } };
                result.Context = query?.Context;
                result.Message = new OnSearchMessage();
                result.Message.Catalog = new Catalog();
                result.Message.Catalog.Offers = new List<Offer>();
                result.Message.Catalog.Providers = new List<Provider>();
                result.Message.Catalog.Fulfillments = new List<Fulfillment>();
                result.Context.Action = ActionEnum.OnSearchEnum;
                result.Context.BppId = "Aff-Dsep-";
                result.Context.BppUri = "http://DSEP-nlb-d3ed9a3f85596080.elb.ap-south-1.amazonaws.com";
                //result.Context.MessageId = Guid.NewGuid().ToString("n");
                result.Message.Catalog.Descriptor = new Descriptor() { Name = "Affindi BPP" };
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
                        if (t.Code == "skills")
                        {
                            searchQuery.skills.AddRange(t._List.Select(x => x.Code));
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
                int fulid = 0;
                int catid = 0;
               

                foreach (var job in jobs)
                {
                    // string locationid = Guid.NewGuid().ToString();
                    var categories = new List<Category>();

                    foreach(var jt in job.employmentType)
                    {
                        categories.Add(new Category
                        {
                            Id = Convert.ToString(++catid),
                            Descriptor = new Descriptor { Code = jt }
                        } );
                    }

                    var item = new Item()
                    {
                        Id = job.id,
                        Descriptor = new Descriptor() { Name = job.title },
                        Price= new Price() { MinimumValue= Convert.ToString(job.baseSalary?.value.minValue)
                        , MaximumValue = Convert.ToString(job.baseSalary?.value.maxValue)
                        ,ListedValue = Convert.ToString(job.baseSalary?.value.maxValue)
                        , Currency= job.baseSalary?.currency
                        , OfferedValue= Convert.ToString(job.baseSalary?.value.maxValue)
                        
                        },
                        LocationIds = new List<string> { Convert.ToString(++locid) },
                        FulfillmentIds= new List<string> { Convert.ToString(++fulid) },
                       
                        CategoryIds= categories.Select(x=>x.Id).ToList(),
                        Time= new Time { Range= new TimeRange { End= Convert.ToDateTime(job.validThrough), Start= Convert.ToDateTime(job.datePosted) } }


                    };
                  //  offer.ItemIds.Add(item.Id);
                    var provider = result.Message.Catalog.Providers.Count ==0 ?
                        result.Message.Catalog.Providers :
                        result.Message.Catalog.Providers.Where(x => x.Descriptor.Name.Contains(job.hiringOrganization.name));

                    if (provider?.Count() > 0)
                    {
                        var pr = provider.First();
                        pr.Items.Add(item);
                        pr.Categories.AddRange(categories);
                        pr.Locations.Add(new Location() { Id= Convert.ToString(locid), City = new City() { Name = job.jobLocation?.address?.addressRegion } });
                        result.Message.Catalog.Providers.Add(pr);
                    }
                    else
                    {
                        var pr = new Provider()
                        {
                           // Id = Guid.NewGuid().ToString("n"),
                            Descriptor = new Descriptor() { Name = job.hiringOrganization.name },
                            Items = new List<Item>() { item },
                            Categories= categories,
                            Locations = new List<Location>() { new Location() { Id = Convert.ToString(locid), City = new City() { Name = job.jobLocation?.address?.addressRegion } } }
                        };
                        result.Message.Catalog.Providers.Add(pr);
                    }

                    var fullfillment = new Fulfillment() { Id = Convert.ToString(fulid) };
                    //fullfillment.Type = job.jobLocationType;
                    fullfillment.Start = new FulfillmentStart() { Time = new Time() { Timestamp = Convert.ToDateTime(job.datePosted) } };
                    ///fullfillment.Start.Location = new Location() { Id = item.LocationIds?.First, City = new City() { Name = job.jobLocation.address.addressRegion } };
                    result.Message.Catalog.Fulfillments.Add(fullfillment);

                   // result.Message.Catalog.Offers.Add(offer);


                }
            }
        }

        private void GetProviders(SearchBody query, Query searchQuery)
        {
            if ( query.Message.Intent.Provider !=null)
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

