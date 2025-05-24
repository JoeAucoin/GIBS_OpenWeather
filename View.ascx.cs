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
using System.Linq;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using DotNetNuke.Common;

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

      //  protected HtmlGenericControl weatherOverview; // Already existing
        protected HtmlGenericControl currentWeather;  // Already existing
        protected HtmlGenericControl dailyForecast;   // Already existing
      //  protected HtmlGenericControl hourlyForecastChartContainer; // New container for the chart
     //   protected HtmlGenericControl weatherAlertsContainer; // New container for alerts
     //   protected HtmlGenericControl locationDisplay; // New container for location name

        public string _latitude = "";
        public string _longitude = "";
        public string _apiKey = "";


        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Page.ClientScript.RegisterClientScriptInclude(this.GetType(), "ChartJS", ("https://cdn.jsdelivr.net/npm/chart.js"));

          //  DotNetNuke.Web.Client.ClientResourceManagement.ClientResourceManager.RegisterScript(this.Page, "https://translate.google.com/translate_a/element.js?cb=googleTranslateElementInit", FileOrder.Js.DefaultPriority, "DnnPageHeaderProvider");

        }

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
                DisplayHourlyForecast(weatherData.hourly);
                DisplayWeatherAlerts(weatherData.alerts);
                locationDisplay.Visible = false;
            }
            else
            {
                currentWeather.InnerHtml = "<p>Failed to retrieve weather data.</p>";
                dailyForecast.InnerHtml = "";
                hourlyForecastChartContainer.InnerHtml = ""; // Clear if no data
                weatherAlertsContainer.InnerHtml = ""; // Clear if no data
                weatherAlertsTitle.Visible = false;
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
                // Convert meters to miles for visibility
                double visibilityInMiles = current.visibility * 0.000621371;
                weatherHtml += $"<b>Visibility:</b> {current.visibility} meters ({visibilityInMiles:F2} miles)<br />"; // Display both

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
                    // Convert wind degrees to compass direction for daily forecast
                    string dailyWindDirection = GetCompassDirection(day.wind_deg);
                    forecastHtml += $"<br><b>Wind:</b> {day.wind_speed} mph, {dailyWindDirection} at {day.wind_deg}° (Gust: {day.wind_gust} mph)";

                   // forecastHtml += $"<br><b>Wind:</b> {day.wind_speed} mph, {day.wind_deg}° (Gust: {day.wind_gust} mph)";
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

        private void DisplayHourlyForecast(Hourly[] hourly)
        {
            if (hourly != null && hourly.Length > 0)
            {
                // Prepare data for Chart.js
                var labels = new System.Text.StringBuilder();
                var temperatures = new System.Text.StringBuilder();

                labels.Append("[");
                temperatures.Append("[");

                // Take up to 24 hours for a reasonable chart size
                foreach (var hour in hourly.Take(24))
                {
                    DateTime hourlyTime = DateTimeOffset.FromUnixTimeSeconds(hour.dt).LocalDateTime;
                    labels.Append($"'{hourlyTime.ToString("h tt")}',");
                    temperatures.Append($"{hour.temp},");
                }

                // Remove trailing commas and close arrays
                if (labels.Length > 1) labels.Length--; // Remove last comma
                labels.Append("]");
                if (temperatures.Length > 1) temperatures.Length--; // Remove last comma
                temperatures.Append("]");

                // Generate HTML for the canvas and JavaScript for Chart.js
                string canvasId = $"hourlyTempChart_{ModuleId}";

                // Generate HTML for the canvas and JavaScript for Chart.js
                string chartHtml = $@"
                    <canvas id='{canvasId}'></canvas>
                    <script>
                        var ctx = document.getElementById('{canvasId}').getContext('2d');
                        var hourlyTempChart = new Chart(ctx, {{
                            type: 'line',
                            data: {{
                                labels: {labels.ToString()},
                                datasets: [{{
                                    label: 'Temperature (°F)',
                                    data: {temperatures.ToString()},
                                    borderColor: 'rgba(75, 192, 192, 1)',
                                    backgroundColor: 'rgba(75, 192, 192, 0.2)',
                                    borderWidth: 1,
                                    fill: true
                                }}]
                            }},
                            options: {{
                                responsive: true,
                                maintainAspectRatio: false,
                                scales: {{
                                    x: {{
                                        title: {{
                                            display: true,
                                            text: 'Time'
                                        }}
                                    }},
                                    y: {{
                                        beginAtZero: false,
                                        title: {{
                                            display: true,
                                            text: 'Temperature (°F)'
                                        }}
                                    }}
                                }}
                            }}
                        }});
                    </script>
                ";
                hourlyForecastChartContainer.InnerHtml = chartHtml;
            }
            else
            {
                hourlyForecastChartContainer.InnerHtml = "<p>No hourly forecast data available.</p>";
            }
        }

        private void DisplayWeatherAlerts(Alert[] alerts)
        {
            if (alerts != null && alerts.Length > 0)
            {
                string alertsHtml = "<ul>";
                foreach (var alert in alerts)
                {
                    DateTime startTime = DateTimeOffset.FromUnixTimeSeconds(alert.start).LocalDateTime.ToLocalTime();
                    DateTime endTime = DateTimeOffset.FromUnixTimeSeconds(alert.end).LocalDateTime.ToLocalTime();

                    alertsHtml += $"<li class='alert-item'>";
                    alertsHtml += $"<h4>{alert._event}</h4>"; // Use _event as 'event' is a C# keyword
                    alertsHtml += $"<p><b>Sender:</b> {alert.sender_name}</p>";
                    alertsHtml += $"<p><b>Period:</b> {startTime.ToString("M/d h:mm tt")} - {endTime.ToString("M/d h:mm tt")}</p>";
                    alertsHtml += $"<p>{alert.description}</p>";
                    if (alert.tags != null && alert.tags.Length > 0)
                    {
                        alertsHtml += $"<p><b>Tags:</b> {string.Join(", ", alert.tags)}</p>";
                    }
                    alertsHtml += $"</li>";
                }
                alertsHtml += "</ul>";
                weatherAlertsContainer.InnerHtml = alertsHtml;
            }
            else
            {
                weatherAlertsTitle.Visible = false;
                weatherAlertsContainer.InnerHtml = "<p style=\"text-align:center;\">No weather alerts at this time.</p>";
            }
        }



        private string GetCompassDirection(int degrees)
        {
            string[] directions = { "N", "NNE", "NE", "ENE", "E", "ESE", "SE", "SSE", "S", "SSW", "SW", "WSW", "W", "WNW", "NW", "NNW" };
            // Normalize degrees to be within 0-360
            degrees = (degrees + 360) % 360;
            // Calculate index into the directions array
            int index = (int)Math.Round((double)degrees / 22.5);
            // Ensure index is within bounds (0-15)
            index = index % 16;
            return directions[index];
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