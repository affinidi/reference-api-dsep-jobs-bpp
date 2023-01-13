using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace bpp.Helpers
{
    public  class STDCodeHelper
    {
        public List<StdCity> cities;
        public STDCodeHelper()
        {
            cities = new List<StdCity>();
            cities.AddRange(Newtonsoft.Json.JsonConvert.DeserializeObject<List<StdCity>>(File.ReadAllText("cities.json")));
        }

        public string GetCity(string v)
        {
            string city = string.Empty;
            try
            {
                city= cities.Where(x => x.stdcode.Contains(v)).Select(c => c.city).ToArray().FirstOrDefault();
            }
            catch (Exception e)
            {
                Console.WriteLine("error while reading std code mapping");
                Console.WriteLine(e.Message);
            }
            return city;
        }
    }
}

