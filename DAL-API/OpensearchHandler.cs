using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using bpp.Helpers;
using Microsoft.Extensions.Logging;
using OpenSearch.Client;
using search.Models;


namespace search
{
    public class OpensearchHandler
    {
        OpenSearchClient _client;
        string _opensearchBaseUrl;
        ILogger _logger;
        public OpensearchHandler(ILoggerFactory logfactory)
        {
            _opensearchBaseUrl = Environment.GetEnvironmentVariable(EnvironmentVariables.OPENSEARCHBASEURL)?.ToString();
            var uri = new UriBuilder(_opensearchBaseUrl).Uri;

            var user = Environment.GetEnvironmentVariable(EnvironmentVariables.USER)?.ToString();
            SecureString secureString = new NetworkCredential("", Environment.GetEnvironmentVariable(EnvironmentVariables.SECURE)?.ToString()).SecurePassword;
            var config = new ConnectionSettings(uri)
                .BasicAuthentication(user, secureString)
                .ServerCertificateValidationCallback(delegate (object o, X509Certificate certificate, X509Chain arg3, SslPolicyErrors arg4) { return true; });
            _client = new OpenSearchClient(config);
            _logger = logfactory.CreateLogger<OpensearchHandler>();



        }

        internal string DeletApplbyId(string id)
        {
            _logger.LogInformation("Delete request for Appplication ID : " + id);
            var deleteResponse = _client.Delete<Application>(id, idx => idx.Index(Constant.INDEX_APPLICATION));

            return (deleteResponse.IsValid ? (deleteResponse.Result.ToString()) : null);
        }

        internal string DeleteJobbyId(string id)
        {
            _logger.LogInformation("Delete request for job ID : " + id);
            var deleteResponse = _client.Delete<Job>(id, idx => idx.Index(Constant.INDEX_JOB));

            return (deleteResponse.IsValid ? (deleteResponse.Result.ToString()) : null);
        }

        internal Job Find(string id)
        {
            _logger.LogInformation("select job request for ID : " + id);
            var searchResponse = _client.Get<Job>(id, idx => idx.Index(Constant.INDEX_JOB));

            return (Job)(searchResponse.IsValid ? ((Job)searchResponse.Source) : null);


        }

        internal Application FindApplication(string id)
        {
            _logger.LogInformation("select application for ID : " + id);
            var searchResponse = _client.Get<Application>(id, idx => idx.Index(Constant.INDEX_APPLICATION));

            return (Application)(searchResponse.IsValid ? ((Application)searchResponse.Source) : null);
        }

        internal List<Application> FindApplications()
        {
            List<Application> applications = new List<Application>();

            var request = new SearchRequest
            {
                From = 0,
                Size = 100,
                Query = new ExistsQuery() { Field = "person.creds" }
            };
            var response = _client.Search<Application>(request);
            if (response.IsValid && response.Documents.Count > 0)
            {
                applications.AddRange(response.Documents);
                _logger.LogInformation("Total Applications in system :" + applications.Count);
            }

            //applications = applications.GroupBy(x => x.jobid).Select(a => a.First()).ToList();

            return applications;
        }

        internal List<Application> FindApplications(string jobid)
        {
            _logger.LogInformation("searching applications for jobID : " + jobid);
            List<Application> applications = new List<Application>();

            var request = new SearchRequest
            {
                From = 0,
                Size = 100,
                Query = new MatchQuery() { Field = "jobid", Query = jobid }
            };
            var response = _client.Search<Application>(request);

            if (response.IsValid && response.Documents.Count > 0)
            {
                applications.AddRange((List<Application>)response.Documents);
                _logger.LogInformation("Total Applications in system for JOb ID  {0} :  {1} :", jobid, applications.Count);
            }

            //applications = applications.GroupBy(x => x.jobid).Select(a => a.First()).ToList();

            return applications;
        }

        internal IEnumerable<Job> FindManyWProvider(Query query)
        {
            ISearchResponse<Job> searchResponse = null;
            List<Job> jobs = new List<Job>();
            foreach (var item in query.provider ?? Enumerable.Empty<ProviderQuery>())
            {
                var request = new SearchRequest
                {
                    From = 0,
                    Size = 100,
                    Query = new MatchQuery { Field = "hiringOrganization.name", Query = item.name } &&
                       new MatchQuery { Field = "jobLocation.address.addressRegion", Query = string.Join(", ", item.locations.Select(x => x)) }
                };
                searchResponse = _client.Search<Job>(request);
                if (searchResponse.IsValid && searchResponse.Documents.Count > 0)
                {
                    jobs.AddRange((List<Job>)searchResponse.Documents);
                }

            }
            return jobs;
        }

        internal IEnumerable<Job> FindWithItem(Query query)
        {
            ISearchResponse<Job> searchResponse = null;
            List<Job> jobs = new List<Job>();
            var jobTitle = query?.title;

            if (query.provider?.Count > 0)
            {
                foreach (var item in query.provider ?? Enumerable.Empty<ProviderQuery>())
                {
                    if (string.IsNullOrEmpty(item.name))
                    {
                        var request = new SearchRequest
                        {
                            From = 0,
                            Size = 100,
                            Query = new MatchQuery { Field = "jobLocation.address.addressRegion", Query = string.Join(", ", item.locations.Select(x => x)) } &&
                                                              new MatchQuery { Field = "title", Query = jobTitle }
                                                              //new MatchQuery { Field = "description", Query = string.Join(",", query.skills.Select(x => x))}

                        };
                        searchResponse = _client.Search<Job>(request);
                        if (searchResponse.IsValid && searchResponse.Documents.Count > 0)
                        {
                            jobs.AddRange((List<Job>)searchResponse.Documents);
                        }
                    }
                    else
                    {
                        var request = new SearchRequest
                        {
                            From = 0,
                            Size = 100,
                            Query = new MatchQuery { Field = "hiringOrganization.name", Query = string.IsNullOrEmpty(item.name) ? "" : item.name } ||
                               new MatchQuery { Field = "jobLocation.address.addressRegion", Query = string.Join(", ", item.locations.Select(x => x)) } &&
                               new MatchQuery { Field = "title", Query = jobTitle }
                               //new MatchQuery { Field = "description", Query = string.Join(",", query.skills.Select(x => x))}

                        };
                        searchResponse = _client.Search<Job>(request);
                        if (searchResponse.IsValid && searchResponse.Documents.Count > 0)
                        {
                            jobs.AddRange((List<Job>)searchResponse.Documents);
                        }
                    }

                }
            }
            else
            {
                var request = new SearchRequest
                {
                    From = 0,
                    Size = 10,

                    Query = new MatchQuery { Field = "title", Query = jobTitle } &&
                                                       new TermsQuery { Field = "skills", Terms = query.skills }

                                                       //new MatchQuery { Field = "description", Query = string.Join(",", query.skills.Select(x => x)) }
                };

                searchResponse = _client.Search<Job>(request);
                if (searchResponse.IsValid && searchResponse.Documents.Count > 0)
                {
                    jobs.AddRange((List<Job>)searchResponse.Documents);
                }
                //jobs = jobs.Where(x => x.skills.Where(s=>s.));
            }

            return jobs;
        }

        internal string SaveDoc(Job job)
        {
            _logger.LogInformation("Saving posted job");
            var response = _client.Index(job, i => i.Index(Constant.INDEX_JOB).Id(job.id));
            return response.IsValid ? response.Id : response.ApiCall?.OriginalException?.ToString();
        }

        internal object SaveDoc(Application application)
        {
            _logger.LogInformation("Saving Application : " + application.id);
            var response = _client.Index(application, i => i.Index(Constant.INDEX_APPLICATION).Id(application.id));
            return response.IsValid ? response.Id : response.ApiCall?.OriginalException?.ToString();
            //return new Object();
        }

        internal string SaveXinput(XinputData xinputData)
        {
            _logger.LogInformation("Saving posted Xinput Data");
            var response = _client.Index(xinputData, i => i.Index(Constant.INDEX_XINPUT).Id(xinputData.xinputFormID));
            return response.IsValid ? response.Id : response.ApiCall?.OriginalException?.ToString();
        }

    }
}

