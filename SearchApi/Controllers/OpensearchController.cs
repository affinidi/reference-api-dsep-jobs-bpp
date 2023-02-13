﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using bpp.Helpers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using search.Models;

namespace search.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OpensearchController : ControllerBase
    {
        OpensearchHandler _opensearchHandler;
        public OpensearchController(OpensearchHandler opensearchHandler)
        {
            Console.WriteLine("Current controller object hascode: " + this.GetHashCode().ToString());
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
        [Route("applications")]
        public List<Application> GetApplications() => _opensearchHandler.FindApplications();

        [HttpPost]
        [Route("jobs")]
        public IActionResult Post(Job job)
        {
            // var jobId = System.Guid.NewGuid().ToString();
            //var json = JsonConvert.SerializeObject(job);


            job.id = CreateMD5(JsonConvert.SerializeObject(job));
            var result = _opensearchHandler.SaveDoc(job);
            return Ok(result);
        }

        private static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                //return Convert.ToHexString(hashBytes); // .NET 5 +

                //Convert the byte array to hexadecimal string prior to.NET 5
                StringBuilder sb = new System.Text.StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
        [HttpPost]
        [Route("applications")]
        public IActionResult Post([FromBody] Application application)
        {

            var result = _opensearchHandler.SaveDoc(application);
            return Created(result); // If Created exists in cs, it shhould be 204
        }

        [HttpPost]
        [Route("jobs")]
        public IEnumerable<Job> Find(Query query)
        {
            Console.WriteLine("New Request for search");
            Console.WriteLine(JsonConvert.SerializeObject(query));

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
                Console.WriteLine(e.Message);
                //Console.WriteLine("query jobtype :" + query?.joblocation[0]);
                Console.WriteLine(e.StackTrace);

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
                Console.WriteLine(e.Message);
                Console.WriteLine("query jobtype :" + query);
                Console.WriteLine(e.StackTrace);

            }
            return jobs;
        }

    }
}

