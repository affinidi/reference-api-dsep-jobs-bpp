//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Logging;
//using Newtonsoft.Json;
//using search.Models;

//namespace search.Controllers
//{
//    [ApiController]
//    [Route("[controller]")]
//    public class SearchController: ControllerBase
//    {
//        private Repository _repository;
//        private FileHandler _fileHandler;

//        public SearchController(Repository repository, FileHandler fileHandler)

//        {
//            _repository = repository;
//            _fileHandler = fileHandler;
//        }


//        [HttpPost]
//        [Route("jobs")]
//        public IEnumerable<Job> Get(Query query)
//        {
//            Console.WriteLine("New Request for search");
//            Console.WriteLine(JsonConvert.SerializeObject( query));

//            var jobs = new List<Job>();
//            if(string.IsNullOrEmpty(query.title))
//            {
//                jobs.AddRange(ItemBasedSearch(query));
//            }
//            else
//            {
//                jobs.AddRange(ProviderBasedSearch(query));
//            }

//            return jobs;
          
            
//        }

        

//        private IEnumerable<Job> ProviderBasedSearch(Query query)
//        {
//            List<Job> jobs = new List<Job>();
//            try
//            {
//                jobs.AddRange( _repository.jobs.Where(x => (x.hiringOrganization.name.Contains(query.provider.name)) &&
//                (query.joblocation.Count == 0 || (query.joblocation.Exists(j => x.jobLocation.address.addressRegion.Contains(j)))))
//                    .Where(x => ((query.title.Count == 0) || query.title.Exists(t => x.title.Contains(t)))
//                    || ((query.joblocation.Count == 0) || (query.joblocation.Exists(j => x.jobLocation.address.addressRegion.Contains(j))))
//                    ));
//            }
//            catch(Exception e)
//            {
//                Console.WriteLine(e.Message);
//                Console.WriteLine("query jobtype :" + query.joblocation[0]);
//                Console.WriteLine(e.StackTrace);

//            }
//            return jobs;
//        }

//        private IEnumerable<Job> ItemBasedSearch(Query query)
//        {
//            List<Job> jobs = new List<Job>();
//            try
//            {
//                Console.WriteLine("No of jobs before search " + _repository.jobs.Count);
//                //return _repository.jobs.Select(x => x);

//                return _repository.jobs//Where(x => (query.jobtype?.Count() == 0 || (query.jobtype.Exists(j => x.jobLocationType.Contains(j)))))
//                    ?.Where(x => ((query.title.Count == 0) || query.title.Exists(t => x.title.Contains(t)))
//                                || ((query.joblocation.Count == 0) || (query.joblocation.Exists(j => x.jobLocation.address.addressRegion.Contains(j))))
//                           );
                
//            }
//            catch(Exception e)
//            {
//                Console.WriteLine(e.Message);
//                Console.WriteLine("query jobtype :" + query.joblocation[0]);
//                Console.WriteLine(e.StackTrace);
//            }
//            return jobs;
//        }

//        [HttpGet]
//        [Route("getbyid/{id}")]
//        public Job Getbyid(string id)
//        {

//            return _repository.jobs.FirstOrDefault(x=>x.id==id);

//        }

//        [HttpPost]
//        public IActionResult Post(Job job)
//        {
//            var jobId = System.Guid.NewGuid().ToString();
//            job.id = jobId;
//            _fileHandler.SaveFile(job);
//            _repository.jobs.Add(job);
//            Console.WriteLine($" total jobs : " + _repository.jobs.Count);
//            return new OkResult();
//        }
//    }
//}

