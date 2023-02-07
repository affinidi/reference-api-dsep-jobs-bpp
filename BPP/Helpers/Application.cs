using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Beckn.Models;


namespace bpp.Helpers
{
    [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public enum StateEnum
    {
        [EnumMember(Value = "Screening")]
        Screening = 0,

        [EnumMember(Value = "Accpeted")]
        Accpeted = 1,

        [EnumMember(Value = "Rejected")]
        Rejected = 2,

        [EnumMember(Value = "Selected")]
        Selected = 3,

        [EnumMember(Value = "Onboarded")]
        Onboarded = 4
    }
    public class Application
    {

        public string id { get; set; }
        public string jobid { get; set; }
        public string jobTitle { get; set; }
        public string transactionid { get; set; }
        public string orderId { get; set; }
        //public List<Document> docs { get; set; }
        public Person person { get; set; }
        public Contact contact { get; set; }
        public StateEnum state { get; set; }
        public Application(string jobid)
        {
            transactionid = string.Empty;
            orderId = string.Empty;
            this.jobid = jobid;
            id = string.Empty;
            // docs = new List<Document>();
            person = new Person();
            contact = new Contact();
            state = StateEnum.Screening;
        }
    }


}

