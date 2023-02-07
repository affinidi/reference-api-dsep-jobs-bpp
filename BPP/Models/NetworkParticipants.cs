using System;
namespace bpp.Models
{
    public class NetworkParticipant
    {
        public DateTime created { get; set; }
        public DateTime valid_from { get; set; }
        public string type { get; set; }
        public string signing_public_key { get; set; }
        public string subscriber_id { get; set; }
        public string unique_key_id { get; set; }
        public DateTime valid_until { get; set; }
        public string subscriber_url { get; set; }
        public string pub_key_id { get; set; }
        public string encr_public_key { get; set; }
        public DateTime updated { get; set; }
        public string status { get; set; }

    }
}

