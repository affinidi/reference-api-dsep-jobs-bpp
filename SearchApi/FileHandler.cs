using System;
using System.IO;
using Newtonsoft.Json;
using search.Models;
using static System.Net.Mime.MediaTypeNames;

namespace search
{
    public class FileHandler
    {
        private string jobDirectory;
        public FileHandler()
        {
            jobDirectory = Environment.GetEnvironmentVariable("jobsDirectory");
        }

        internal void SaveFile(Job job)
        {
            try
            {
                File.WriteAllText(Path.Combine( jobDirectory,job.id+".json"), JsonConvert.SerializeObject(job));

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}

