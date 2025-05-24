using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GIBS.Modules.GIBS_OpenWeather.Components
{
    public class WeatherData
    {
        public double lat { get; set; }
        public double lon { get; set; }
        public string timezone { get; set; }
        public int timezone_offset { get; set; }
        public Current current { get; set; }
        public Daily[] daily { get; set; }

        // ... existing properties (lat, lon, timezone, current, daily) ...
        public Hourly[] hourly { get; set; } // Add this line
        public Alert[] alerts { get; set; }   // Add this line
    }

    public class Current
    {
        public int dt { get; set; }
        public int sunrise { get; set; }
        public int sunset { get; set; }
        public double temp { get; set; }
        public double feels_like { get; set; }
        public int pressure { get; set; }
        public int humidity { get; set; }
        public double dew_point { get; set; }
        public double uvi { get; set; }
        public int clouds { get; set; }
        public int visibility { get; set; }
        public double wind_speed { get; set; }
        public double wind_gust { get; set; }
        public int wind_deg { get; set; }
        public Weather[] weather { get; set; }
    }

    public class Weather
    {
        public int id { get; set; }
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
    }

    public class Daily
    {
        public int dt { get; set; }
        public int sunrise { get; set; }
        public int sunset { get; set; }
        public int moonrise { get; set; }
        public int moonset { get; set; }
        public double moon_phase { get; set; }
        public string summary { get; set; }
        public Temp temp { get; set; }
        public FeelsLike feels_like { get; set; }
        public int pressure { get; set; }
        public int humidity { get; set; }
        public double dew_point { get; set; }
        public double wind_speed { get; set; }
        public int wind_deg { get; set; }
        public double wind_gust { get; set; }
        public Weather[] weather { get; set; }
        public int clouds { get; set; }
        public double pop { get; set; }
        public double? rain { get; set; }
        public double uvi { get; set; }
    }

    public class Temp
    {
        public double day { get; set; }
        public double min { get; set; }
        public double max { get; set; }
        public double night { get; set; }
        public double eve { get; set; }
        public double morn { get; set; }
    }

    public class FeelsLike
    {
        public double day { get; set; }
        public double night { get; set; }
        public double eve { get; set; }
        public double morn { get; set; }
    }

    //public class LocationData
    //{
    //    public string name { get; set; }
    //    public LocalNames local_names { get; set; }
    //    public double lat { get; set; }
    //    public double lon { get; set; }
    //    public string country { get; set; }
    //    public string state { get; set; }
    //}

    //public class LocalNames
    //{
    //    [JsonProperty("en")]
    //    public string En { get; set; }
    //}
    public class WeatherOverview
    {
        public double lat { get; set; }
        public double lon { get; set; }
        public string tz { get; set; }
        public DateTime date { get; set; }
        public string units { get; set; }
        public string weather_overview { get; set; }
    }

    // In GIBS.Modules.GIBS_OpenWeather.Components namespace
    // Add these classes or ensure they exist:

    public class Hourly
    {
        public int dt { get; set; }
        public double temp { get; set; }
        public double feels_like { get; set; }
        public int pressure { get; set; }
        public int humidity { get; set; }
        public double dew_point { get; set; }
        public double uvi { get; set; }
        public int clouds { get; set; }
        public int visibility { get; set; }
        public double wind_speed { get; set; }
        public int wind_deg { get; set; }
        public double wind_gust { get; set; }
        public Weather[] weather { get; set; }
        public double pop { get; set; } // Probability of precipitation
        public Rain rain { get; set; } // Nested object for rain, might be null
        public Snow snow { get; set; } // Nested object for snow, might be null
    }

    public class Rain
    {
        [JsonProperty("1h")]
        public double _1h { get; set; } // Rain volume for last hour, mm
    }

    public class Snow
    {
        [JsonProperty("1h")]
        public double _1h { get; set; } // Snow volume for last hour, mm
    }

    public class Alert
    {
        public string sender_name { get; set; }
        public string _event { get; set; } // Use _event because 'event' is a C# keyword
        public int start { get; set; }
        public int end { get; set; }
        public string description { get; set; }
        public string[] tags { get; set; }
    }



}