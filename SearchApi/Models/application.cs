using System;
using System.Collections.Generic;
using bpp.Models;

namespace bpp.Helpers
{
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

