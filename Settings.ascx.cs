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
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.Localization;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Web.UI.WebControls;

namespace GIBS.Modules.GIBS_OpenWeather
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The Settings class manages Module Settings
    /// 
    /// Typically your settings control would be used to manage settings for your module.
    /// There are two types of settings, ModuleSettings, and TabModuleSettings.
    /// 
    /// ModuleSettings apply to all "copies" of a module on a site, no matter which page the module is on. 
    /// 
    /// TabModuleSettings apply only to the current module on the current page, if you copy that module to
    /// another page the settings are not transferred.
    /// 
    /// If you happen to save both TabModuleSettings and ModuleSettings, TabModuleSettings overrides ModuleSettings.
    /// 
    /// Below we have some examples of how to access these settings but you will need to uncomment to use.
    /// 
    /// Because the control inherits from GIBS_OpenWeatherSettingsBase you have access to any custom properties
    /// defined there, as well as properties from DNN such as PortalId, ModuleId, TabId, UserId and many more.
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class Settings : GIBS_OpenWeatherModuleSettingsBase
    {
        #region Base Method Implementations

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// LoadSettings loads the settings from the Database and displays them
        /// </summary>
        /// -----------------------------------------------------------------------------
        public override void LoadSettings()
        {
            try
            {
                if (Page.IsPostBack == false)
                {
                    //Check for existing settings and use those on this page
                    //Settings["SettingName"]

                    /* uncomment to load saved settings in the text boxes
                    if(Settings.Contains("Setting1"))
                        txtSetting1.Text = Settings["Setting1"].ToString();

                    */

                    if (Settings.Contains("apiKey"))
                        txtApiKey.Text = Settings["apiKey"].ToString();

                    if (Settings.Contains("city"))
                        txtCity.Text = Settings["city"].ToString();

                    if (Settings.Contains("state"))
                        txtState.Text = Settings["state"].ToString();

                    if (Settings.Contains("countryCode"))
                        txtCountryCode.Text = Settings["countryCode"].ToString();

                    if (Settings.Contains("latitude"))
                        txtLatitude.Text = Settings["latitude"].ToString();

                    if (Settings.Contains("longitude"))
                        txtLongitude.Text = Settings["longitude"].ToString();

                   

                }
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// UpdateSettings saves the modified settings to the Database
        /// </summary>
        /// -----------------------------------------------------------------------------
        public override void UpdateSettings()
        {
            try
            {
                var modules = new ModuleController();

                modules.UpdateModuleSetting(ModuleId, "apiKey", txtApiKey.Text);
                modules.UpdateModuleSetting(ModuleId, "city", txtCity.Text);
                modules.UpdateModuleSetting(ModuleId, "state", txtState.Text);
                modules.UpdateModuleSetting(ModuleId, "countryCode", txtCountryCode.Text);
                modules.UpdateModuleSetting(ModuleId, "latitude", txtLatitude.Text);
                modules.UpdateModuleSetting(ModuleId, "longitude", txtLongitude.Text);
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        #endregion

        protected void LinkButtonLookupLatLong_Click(object sender, EventArgs e)
        {
            string city = txtCity.Text.Trim();
            string state = txtState.Text.Trim();
            string countryCode = txtCountryCode.Text.Trim();
            string apiKey = txtApiKey.Text.Trim();

            if (string.IsNullOrEmpty(city))
            {
                ShowMessage.Text = Localization.GetString("CityRequired", LocalResourceFile);
                return;
            }

            if (string.IsNullOrEmpty(apiKey))
            {
                ShowMessage.Text = Localization.GetString("ApiKeyRequired", LocalResourceFile);
                return;
            }

            // Construct the Geocoding API URL with limit=5.
            string geocodingApiUrl = $"https://api.openweathermap.org/geo/1.0/direct?q={city},{state},{countryCode}&limit=10&appid={apiKey}";

            try
            {
                // Make the API call.
                using (WebClient client = new WebClient())
                {
                    string responseJson = client.DownloadString(geocodingApiUrl);

                    // Parse the JSON response.
                    JArray response = JArray.Parse(responseJson);

                    if (response.Count > 0)
                    {
                        if (response.Count == 1)
                        {
                            // Extract latitude and longitude.
                            double latitude = (double)response[0]["lat"];
                            double longitude = (double)response[0]["lon"];

                            // Update the text boxes.
                            txtLatitude.Text = latitude.ToString();
                            txtLongitude.Text = longitude.ToString();
                            ShowMessage.Text = Localization.GetString("LatLongFound", LocalResourceFile);
                        }
                        else
                        {
                            // Clear any previous dropdown.
                            CorrectLocation.Items.Clear();
                            CorrectLocation.Visible = true;
                            divLocationSelect.Visible = true;
                            // Add a default item
                            CorrectLocation.Items.Add(new ListItem(Localization.GetString("SelectLocation", LocalResourceFile), "-1"));

                            // Iterate through the results and add them to the dropdown.
                            foreach (JObject result in response)
                            {
                                string locationName = (string)result["name"];
                                string resultState = (string)result["state"];
                                string resultCountry = (string)result["country"];
                                double latitude = (double)result["lat"];
                                double longitude = (double)result["lon"];
                                string display = $"{locationName}, {resultState}, {resultCountry} ({latitude}, {longitude})";
                                string value = $"{latitude},{longitude}";
                                CorrectLocation.Items.Add(new ListItem(display, value));
                            }
                            ShowMessage.Text = Localization.GetString("MultipleLocationsFound", LocalResourceFile);
                        }
                    }
                    else
                    {
                        ShowMessage.Text = Localization.GetString("LocationNotFound", LocalResourceFile);
                    }
                }
            }
            catch (WebException ex)
            {
                // Handle API errors (e.g., invalid API key, network issues).
                var errorMessage = ex.Message;
                if (ex.Response != null)
                {
                    using (var reader = new System.IO.StreamReader(ex.Response.GetResponseStream()))
                    {
                        errorMessage = reader.ReadToEnd();
                    }
                }
                ShowMessage.Text = string.Format(Localization.GetString("GeocodingError", LocalResourceFile), errorMessage);
            }
            catch (JsonException ex)
            {
                // Handle JSON parsing errors.
                ShowMessage.Text = string.Format(Localization.GetString("InvalidJsonResponse", LocalResourceFile), ex.Message);
            }
            catch (Exception ex)
            {
                // Handle other unexpected errors.
                ShowMessage.Text = string.Format(Localization.GetString("UnexpectedError", LocalResourceFile), ex.Message);
            }
        }

        protected void CorrectLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CorrectLocation.SelectedValue != "-1")
            {
                string[] latLong = CorrectLocation.SelectedValue.Split(',');
                txtLatitude.Text = latLong[0];
                txtLongitude.Text = latLong[1];
                CorrectLocation.Visible = false;
                divLocationSelect.Visible = false;
                ShowMessage.Text = Localization.GetString("LocationSelected", LocalResourceFile);
            }
            else
            {
                txtLatitude.Text = "";
                txtLongitude.Text = "";
            }
        }

    }
}