using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using bpp.Models;
using CodeBeautify;
using Newtonsoft.Json.Converters;
using search.Models;

namespace bpp.Helpers
{

    [JsonConverter(typeof(StringEnumConverter))]
    public enum StateEnum
    {
        [EnumMember(Value = "Screening")]
        Screening = 0,

        [EnumMember(Value = "Accepted")]
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
        public List<Document> docs { get; set; }
        public Person person { get; set; }
        public Contact contact { get; set; }
        public StateEnum state { get; set; }
        //public Application()
        //{
        //    //transactionid = string.Empty;
        //    //orderId = string.Empty;
        //    //jobid = string.Empty;
        //    //id = string.Empty;
        //    //docs = new List<Document>();
        //    //person = new Person() { Tags= new List<TagGroup>() { new TagGroup() { _List= new List<Tag>()} } };
        //    //contact = new Contact();
        //}
    }


}

