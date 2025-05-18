using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GIBS.Modules.GIBS_OpenWeather.Components
{
    public class LocalNames
    {
        // Add properties for different languages if needed
        [JsonProperty("en")]
        public string En { get; set; }
        // Example for another language:
        // [JsonProperty("fr")]
        // public string Fr { get; set; }
    }

    public class LocationData
    {
        public string name { get; set; }
        public LocalNames local_names { get; set; }
        public double lat { get; set; }
        public double lon { get; set; }
        public string country { get; set; }
        public string state { get; set; }
    }
}