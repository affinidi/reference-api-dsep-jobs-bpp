using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace BAP.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    
    public class AAddress
    {
        [JsonProperty("@type")]
        public string type { get; set; }
        public string name { get; set; }
        public string streetAddress { get; set; }
        public string addressLocality { get; set; }
        public string addressRegion { get; set; }
        public string addressCountry { get; set; }
        public string postalCode { get; set; }
    }

    public class BaseSalary
    {
        [JsonProperty("@type")]
        public string type { get; set; }
        public string currency { get; set; }
        public Value value { get; set; }
    }

    public class HiringOrganization
    {
        [JsonProperty("@type")]
        public string type { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public AAddress address { get; set; }
    }

    public class Identifier
    {
        [JsonProperty("@type")]
        public string type { get; set; }
        public string name { get; set; }
        public string value { get; set; }
    }

    public class InCodeSet
    {
        [JsonProperty("@type")]
        public string type { get; set; }
        public string name { get; set; }
        public string dateModified { get; set; }
        public string url { get; set; }
    }

    public class JobLocation
    {
        [JsonProperty("@type")]
        public string type { get; set; }
        public string name { get; set; }
        public AAddress address { get; set; }
    }

    public class OccupationalCategory
    {
        [JsonProperty("@type")]
        public string type { get; set; }
        public InCodeSet inCodeSet { get; set; }
        public string codeValue { get; set; }
        public string name { get; set; }
        public string url { get; set; }
    }

    public class Job
    {
        public string id { get; set; }
        [JsonProperty("@context")]
        public string context { get; set; }

        [JsonProperty("@type")]
        public string type { get; set; }
        public string title { get; set; }
        public string datePosted { get; set; }
        public DateTime validThrough { get; set; }
        public string description { get; set; }
        public HiringOrganization hiringOrganization { get; set; }
        public JobLocation jobLocation { get; set; }
        public List<string> employmentType { get; set; }
        public BaseSalary baseSalary { get; set; }
        public Identifier identifier { get; set; }
        public OccupationalCategory occupationalCategory { get; set; }
        public List<string> responsibilities { get; set; }
        public List<string> skills { get; set; }
    }

    public class Value
    {
        [JsonProperty("@type")]
        public string type { get; set; }
        public int minValue { get; set; }
        public int maxValue { get; set; }
        public string unitText { get; set; }
    }




}

