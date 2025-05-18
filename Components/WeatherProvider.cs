using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
//using System.Threading.Tasks;
using Newtonsoft.Json; // Make sure you have this package installed via NuGet

namespace GIBS.Modules.GIBS_OpenWeather.Components
{
    public class WeatherProvider
    {
        private readonly string _apiKey;
        private readonly double _latitude;
        private readonly double _longitude;
       
        private const string ApiUrl = "https://api.openweathermap.org/data/3.0/onecall";
        private const string ApiOverviewUrl = "https://api.openweathermap.org/data/3.0/onecall/overview"; // Added this line
        private const string ApiGeoReverseUrl = "http://api.openweathermap.org/geo/1.0/reverse";


        public WeatherProvider(string apiKey, double latitude, double longitude)
        {
            _apiKey = apiKey;
            _latitude = latitude;
            _longitude = longitude;
            
        }

        public WeatherData GetWeatherData()
        {
            using (HttpClient client = new HttpClient())
            {
                string url = $"{ApiUrl}?lat={_latitude}&lon={_longitude}&exclude=minutely,hourly,alerts&appid={_apiKey}&units=imperial";
                try
                {
                    HttpResponseMessage response = client.GetAsync(url).Result;
                    response.EnsureSuccessStatusCode();
                    string json = response.Content.ReadAsStringAsync().Result;
                    WeatherData data = JsonConvert.DeserializeObject<WeatherData>(json);
                    return data;
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine($"Error (GetWeatherData): {ex.Message}");
                    return null;
                }
                catch (JsonException ex)
                {
                    Console.WriteLine($"Error (GetWeatherData - JSON): {ex.Message}");
                    return null;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error (GetWeatherData - General): {ex.Message}");
                    return null;
                }
            }
        }

        public WeatherOverview GetWeatherOverview()
        {
            using (HttpClient client = new HttpClient())
            {
                string url = $"{ApiOverviewUrl}?lat={_latitude}&lon={_longitude}&units=imperial&appid={_apiKey}";
                try
                {
                    HttpResponseMessage response = client.GetAsync(url).Result;
                    response.EnsureSuccessStatusCode();
                    string json = response.Content.ReadAsStringAsync().Result;
                    WeatherOverview overviewData = JsonConvert.DeserializeObject<WeatherOverview>(json);
                    return overviewData;
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine($"Error (GetWeatherOverview): {ex.Message}");
                    return null;
                }
                catch (JsonException ex)
                {
                    Console.WriteLine($"Error (GetWeatherOverview - JSON): {ex.Message}");
                    return null;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error (GetWeatherOverview - General): {ex.Message}");
                    return null;
                }
            }
        }

        public LocationData GetLocationData()
        {
            using (HttpClient client = new HttpClient())
            {
                string url = $"{ApiGeoReverseUrl}?lat={_latitude}&lon={_longitude}&limit=1&appid={_apiKey}";
                try
                {
                    HttpResponseMessage response = client.GetAsync(url).Result;
                    response.EnsureSuccessStatusCode();
                    string json = response.Content.ReadAsStringAsync().Result;
                    List<LocationData> locationList = JsonConvert.DeserializeObject<List<LocationData>>(json);
                    if (locationList != null && locationList.Count > 0)
                    {
                        return locationList[0];
                    }
                    return null;
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine($"Error (GetLocationData): {ex.Message}");
                   // Exceptions.ProcessModuleLoadException(this, $"Error (GetLocationData): {ex.Message}");
                    return null;
                }
                catch (JsonException ex)
                {
                    Console.WriteLine($"Error (GetLocationData - JSON): {ex.Message}");
                    return null;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error (GetLocationData - General): {ex.Message}");
                    return null;
                }
            }
        }
        public static void Main(string[] args)
        {
            string apiKey = "YOUR_API_KEY";
            double latitude = 41.6821;
            double longitude = -69.9597;

            WeatherProvider provider = new WeatherProvider(apiKey, latitude, longitude);
            WeatherData weatherData = provider.GetWeatherData();

            if (weatherData != null)
            {
                Console.WriteLine($"Current Temperature: {weatherData.current.temp}°F");
                Console.WriteLine($"Description: {weatherData.current.weather[0].description}");
                Console.WriteLine($"Daily Summary: {weatherData.daily[0].summary}");
            }
            else
            {
                Console.WriteLine("Failed to retrieve weather data.");
            }

            WeatherOverview overview = provider.GetWeatherOverview();
            if (overview != null)
            {
                Console.WriteLine($"\nWeather Overview: {overview.weather_overview}");
            }
            else
            {
                Console.WriteLine("Failed to retrieve weather overview.");
            }

            LocationData location = provider.GetLocationData();
            if (location != null)
            {
                Console.WriteLine($"\nLocation: {location.name}, {location.state}, {location.country}");
            }
            else
            {
                Console.WriteLine("Failed to retrieve location data.");
            }
        }
    }
}