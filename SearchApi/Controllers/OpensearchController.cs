using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using bpp.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using search.Models;

namespace search.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OpensearchController : ControllerBase
    {
        ILogger _logger;
        OpensearchHandler _opensearchHandler;
        public OpensearchController(OpensearchHandler opensearchHandler, ILoggerFactory loggerfactory)
        {
            _logger = loggerfactory.CreateLogger<OpensearchController>();
            _opensearchHandler = opensearchHandler;
        }

        [HttpGet]
        [Route("jobs/{id}")]
        public Job Getbyid(string id)
        {

            return _opensearchHandler.Find(id);

        }

        [HttpGet]
        [Route("applications/{id}")]
        public Application Applicationbyid(string id)
        {

            return _opensearchHandler.FindApplication(id);

        }

        [HttpGet]
        [Route("allapplications")]
        public List<Application> GetApplications() => _opensearchHandler.FindApplications();

        [HttpGet]
        [Route("applications")]
        public List<Application> GetApplicationForJob(string jobid) => _opensearchHandler.FindApplications(jobid);

        [HttpPost]
        [Route("addjob")]
        [ValidateAntiForgeryToken]
        public IActionResult Post(Job job)
        {

            job.id = ComputeSha256Hash(JsonConvert.SerializeObject(job));
            var result = _opensearchHandler.SaveDoc(job);
            return Ok(result);
        }
        static string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }


        [HttpPost]
        [Route("applications")]
        [ValidateAntiForgeryToken]
        public IActionResult Post([FromBody] Application application)
        {

            var result = _opensearchHandler.SaveDoc(application);
            return Created("", result); // If Created exists in cs, it shhould be 204
        }

        [HttpPost]
        [Route("jobs")]
        public IEnumerable<Job> Find(Query query)
        {
            _logger.LogInformation("New Request for search");
            _logger.LogInformation(JsonConvert.SerializeObject(query));

            var jobs = new List<Job>();
            if (!string.IsNullOrEmpty(query.title))
            {
                jobs.AddRange(ItemBasedSearch(query));
            }
            else
            {
                jobs.AddRange(ProviderBasedSearch(query));
            }

            return jobs;


        }

        private IEnumerable<Job> ProviderBasedSearch(Query query)
        {
            List<Job> jobs = new List<Job>();
            try
            {
                var searchResult = _opensearchHandler.FindManyWProvider(query).ToList();
                if (searchResult.Count > 1)
                {
                    jobs.AddRange(_opensearchHandler.FindManyWProvider(query));
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);

                _logger.LogError(e.StackTrace);

            }
            return jobs;
        }

        private IEnumerable<Job> ItemBasedSearch(Query query)
        {
            List<Job> jobs = new List<Job>();
            try
            {
                jobs.AddRange(_opensearchHandler.FindWithItem(query));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                _logger.LogError("query jobtype :" + query);
                _logger.LogError(e.StackTrace);

            }
            return jobs;
        }

    }
}

