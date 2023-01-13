using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace BAP.Models
{
	public class EUAPayload
	{
        public  List<string> locations { get; set; }
        public string transactionId { get; set; }
        public string jobid { get; set; }
        public List<Credential> credentials { get; set; }
        public List<string> skills { get; set; }
        public string provider { get; set; }
        public string title { get; set; }

    }
}

