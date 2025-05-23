/*
' Copyright (c) 2025  GIBS.com
'  All rights reserved.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
' DEALINGS IN THE SOFTWARE.
' 
*/

using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Security;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.Localization;
using GIBS.Modules.GIBS_OpenWeather.Components;
using System;
using System.Threading.Tasks;
using System.Web.UI;

namespace GIBS.Modules.GIBS_OpenWeather
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The View class displays the content
    /// 
    /// Typically your view control would be used to display content or functionality in your module.
    /// 
    /// View may be the only control you have in your project depending on the complexity of your module
    /// 
    /// Because the control inherits from GIBS_OpenWeatherModuleBase you have access to any custom properties
    /// defined there, as well as properties from DNN such as PortalId, ModuleId, TabId, UserId and many more.
    /// 
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class View : GIBS_OpenWeatherModuleBase  //, IActionable
    {

        protected System.Web.UI.HtmlControls.HtmlGenericControl currentWeather;
        protected System.Web.UI.HtmlControls.HtmlGenericControl dailyForecast;

        public string _latitude = "";
        public string _longitude = "";
        public string _apiKey = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    LoadSettings();
                    //  Page.RegisterAsyncTask(new PageAsyncTask(LoadWeatherData));
                    if (_apiKey.ToString().Length > 10)
                    {
                        LoadWeatherData();
                    }
                }
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }


        private void LoadWeatherData()
        {
            // Replace with your actual API key and coordinates, preferably from a configuration source
            string apiKey = _apiKey.ToString();
            double latitude = Double.Parse(_latitude.ToString());   //     41.6821; // Chatham, MA
            double longitude = Double.Parse(_longitude.ToString());     //-69.9597; // Chatham, MA

            WeatherProvider provider = new WeatherProvider(apiKey, latitude, longitude);
            WeatherData weatherData = provider.GetWeatherData();
            WeatherOverview overviewData = provider.GetWeatherOverview();
            LocationData locationData = provider.GetLocationData();

            if (overviewData != null)
            {
                weatherOverview.InnerHtml = $"<h3>{locationData.name}, {locationData.state}</h3>";
                weatherOverview.InnerHtml += $"<p>{overviewData.weather_overview}</p>";
            }
            else
            {
                weatherOverview.InnerHtml = "<p>Failed to retrieve weather overview.</p>";
            }

            if (weatherData != null)
            {
                DisplayCurrentWeather(weatherData.current);
                DisplayDailyForecast(weatherData.daily);
            }
            else
            {
                currentWeather.InnerHtml = "<p>Failed to retrieve weather data.</p>";
                dailyForecast.InnerHtml = "";
            }
        }

        private void DisplayCurrentWeather(Current current)
        {
            if (current != null)
            {
                string weatherHtml = "<p>";
                if (current.weather != null && current.weather.Length > 0)
                {
                    weatherHtml += $"<img src=\"https://openweathermap.org/img/wn/{current.weather[0].icon}@4x.png\" alt=\"{current.weather[0].description}\" title=\"{current.weather[0].description}\" align=\"right\">";
                }

                weatherHtml += $"<span class=\"currentConditions\">{current.weather[0].description.ToUpper()}</span><br />";
                weatherHtml += $"<b>Temperature:</b> {current.temp}°F (Feels like: {current.feels_like}°F)<br />"; 
                weatherHtml += $"<b>Humidity:</b> {current.humidity}%<br />";
                weatherHtml += $"<b>Pressure:</b> {current.pressure} hPa<br />";
                weatherHtml += $"<b>Wind:</b> {current.wind_speed} mph, {current.wind_deg}°<br />";
                if (current.wind_gust != 0) // Check if wind_gust is not the default value (assuming 0 is not a valid gust)
                {
                    weatherHtml += $"<b>Wind Gust:</b> {current.wind_gust} mph<br />";
                }
                weatherHtml += $"<b>Clouds:</b> {current.clouds}%<br />";
                weatherHtml += $"<b>Visibility:</b> {current.visibility} meters<br />";
                weatherHtml += $"<b>UV Index:</b> {current.uvi}<br />";
                DateTime sunrise = DateTimeOffset.FromUnixTimeSeconds(current.sunrise).LocalDateTime;
                DateTime sunset = DateTimeOffset.FromUnixTimeSeconds(current.sunset).LocalDateTime;
                weatherHtml += $"<b>Sunrise:</b> {sunrise.ToLocalTime().ToString("h:mm tt")}<br />";
                weatherHtml += $"<b>Sunset:</b> {sunset.ToLocalTime().ToString("h:mm tt")}</p>";

                currentWeather.InnerHtml = weatherHtml;
            }
            else
            {
                currentWeather.InnerHtml = "<p>No current weather data available.</p>";
            }
        }

        private void DisplayDailyForecast(Daily[] daily)
        {
            if (daily != null && daily.Length > 0)
            {
                string forecastHtml = "<ul>";
                foreach (var day in daily)
                {
                    DateTime forecastDate = DateTimeOffset.FromUnixTimeSeconds(day.dt).LocalDateTime.Date;
                    string dayOfWeek = forecastDate.ToString("dddd");
                    string iconUrl = "";
                    string description = "";
                    string iconimage = "";
                    if (day.weather != null && day.weather.Length > 0)
                    {
                        iconUrl = $"https://openweathermap.org/img/wn/{day.weather[0].icon}@2x.png";
                        description = day.weather[0].description;
                        iconimage = $"<img src =\"{iconUrl}\" alt=\"{description}\" title=\"{description}\" align=\"right\" style=\"rightimg\">";
                    }

                    forecastHtml += $"<li><strong>{dayOfWeek} ({forecastDate.ToString("M/d")})</strong> {iconimage}";
                    forecastHtml += $"<br><b>Forecast:</b> {description.ToUpper()}";
                    forecastHtml += $"<br><b>High:</b> {day.temp.max}°F / Low: {day.temp.min}°F";
                    forecastHtml += $"<br><b>Day:</b> {day.temp.day}°F / Night: {day.temp.night}°F";
                    forecastHtml += $"<br><b>Morning:</b> {day.temp.morn}°F / Evening: {day.temp.eve}°F";
                    forecastHtml += $"<br><b>Feels Like:</b> Day {day.feels_like.day}°F / Night {day.feels_like.night}°F / Morn {day.feels_like.morn}°F / Eve {day.feels_like.eve}°F";
                    
                    forecastHtml += $"<br><b>Precipitation Probability:</b> {(day.pop * 100):F0}%";
                    if (day.rain.HasValue)
                    {

                        double rainInInches = day.rain.Value / 25.4; // Conversion from mm to inches
                        forecastHtml += $"<br><b>Rain:</b> {day.rain:F2} mm ({rainInInches:F2} in)";
                    }
                    forecastHtml += $"<br><b>Humidity:</b> {day.humidity}%";
                    forecastHtml += $"<br><b>Wind:</b> {day.wind_speed} mph, {day.wind_deg}° (Gust: {day.wind_gust} mph)";
                    forecastHtml += $"<br><b>Clouds:</b> {day.clouds}%";
                    forecastHtml += $"<br><b>UV Index:</b> {day.uvi}";
                    DateTime sunrise = DateTimeOffset.FromUnixTimeSeconds(day.sunrise).LocalDateTime.ToLocalTime();
                    DateTime sunset = DateTimeOffset.FromUnixTimeSeconds(day.sunset).LocalDateTime.ToLocalTime();
                    DateTime moonrise = DateTimeOffset.FromUnixTimeSeconds(day.moonrise).LocalDateTime.ToLocalTime();
                    DateTime moonset = DateTimeOffset.FromUnixTimeSeconds(day.moonset).LocalDateTime.ToLocalTime();
                    forecastHtml += $"<br><b>Sunrise:</b> {sunrise.ToString("h:mm tt")}, Sunset: {sunset.ToString("h:mm tt")}";
                    forecastHtml += $"<br><b>Moonrise:</b> {moonrise.ToString("h:mm tt")}, Moonset: {moonset.ToString("h:mm tt")}, Moon Phase: {day.moon_phase}";
                    forecastHtml += $"<br><b>Summary:</b> {day.summary}";
                    forecastHtml += "</li><hr class=\"thickhr\">";
                }
                forecastHtml += "</ul>";
                dailyForecast.InnerHtml = forecastHtml;
            }
            else
            {
                dailyForecast.InnerHtml = "<p>No daily forecast data available.</p>";
            }
        }

        public void LoadSettings()
        {
            try
            {
                if (Settings.Contains("apiKey"))
                {
                    _apiKey = Settings["apiKey"].ToString();

                }

                if (Settings.Contains("latitude"))
                {
                    _latitude = Settings["latitude"].ToString();

                }

                if (Settings.Contains("longitude"))
                {
                    _longitude = Settings["longitude"].ToString();

                }


            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }


        //public ModuleActionCollection ModuleActions
        //{
        //    get
        //    {
        //        var actions = new ModuleActionCollection
        //            {
        //                {
        //                    GetNextActionID(), Localization.GetString("EditModule", LocalResourceFile), "", "", "",
        //                    EditUrl(), false, SecurityAccessLevel.Edit, true, false
        //                }
        //            };
        //        return actions;
        //    }
        //}
    }
}