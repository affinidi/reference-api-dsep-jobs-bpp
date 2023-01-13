using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using bpp.Helpers;
using Newtonsoft.Json;
using search.Models;

namespace search
{
    public class Repository
    {
        public List<Job> jobs;
        public List<Application> applications;
        public List<City> cities;
        public Repository()
        {
            //jobs = new List<Job>();
            //applications = new List<Application>();
            //var jobDirectory = Environment.GetEnvironmentVariable("jobsDirectory");
            //var applicationDirectory = Environment.GetEnvironmentVariable("apDirectory");
            //IntializePersistedJobs(jobDirectory);
            //IntializePersistedApplication(applicationDirectory);
            //Console.WriteLine($"total jobs : " +  jobs.Count);
            //Console.WriteLine($"total application : " + applications?.Count);

            cities = new List<City>();
             cities.AddRange(Newtonsoft.Json.JsonConvert.DeserializeObject<List<City>>(File.ReadAllText("cities.json")));

        }

        private void IntializePersistedApplication(string applicationDirectory)
        {
            if (!string.IsNullOrEmpty(applicationDirectory))
            {
                var appFiles = Directory.GetFiles(applicationDirectory, "*.json");
                if (appFiles.Count() > 0)
                {
                    foreach (var file in appFiles)
                    {
                        var application = File.ReadAllText(file);
                        applications.Add(JsonConvert.DeserializeObject<Application>(application));
                    }

                }
                else
                {
                    Console.WriteLine("files not found");
                }
            }
            else
            {
                Console.WriteLine("application directory not set");
            }
        }

        private void IntializePersistedJobs(string jobDirectory)
        {
            if (!string.IsNullOrEmpty(jobDirectory))
            {
                var jobFiles = Directory.GetFiles(jobDirectory, "*.json");
                if (jobFiles.Count() > 0)
                {
                    foreach (var file in jobFiles)
                    {
                        var job = File.ReadAllText(file);
                        jobs.Add(Newtonsoft.Json.JsonConvert.DeserializeObject<Job>(job));
                    }

                }
                else
                {
                    Console.WriteLine("files not found");
                }
            }
            else
            {
                Console.WriteLine("job directory not set");
            }
        }
    }
}

