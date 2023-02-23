using System;
using System.Collections.Generic;

namespace search.Models
{
    public class Query
    {
        public List<ProviderQuery> provider { get; set; }
        public string title { get; set; }
        public List<string> joblocation { get; set; }
        public List<string> interviewLocation { get; set; }
        public List<string> providerLocation { get; set; }
        public List<string> category { get; set; }
        public List<string> skills { get; set; }
        public List<string> salary { get; set; }
        public List<DateTime?> jobposted { get; set; }
        public List<string> duration { get; set; }
        public List<string> jobtype { get; set; }

        public Query()
        {
            provider = new List<ProviderQuery>();
            title = string.Empty;
            joblocation = new List<string>();
            interviewLocation = new List<string>();
            providerLocation = new List<string>();
            category = new List<string>();
            skills = new List<string>();
            salary = new List<string>();
            jobposted = new List<DateTime?>();
            duration = new List<string>();
            jobtype = new List<string>();
        }
    }

    public class ProviderQuery
    {
        public string name { get; set; }
        public List<string> locations { get; set; }
        public List<string> titiles { get; set; }

        public ProviderQuery()
        {
            name = string.Empty;
            locations = new List<string>();
            titiles = new List<string>();
        }
    }
}

