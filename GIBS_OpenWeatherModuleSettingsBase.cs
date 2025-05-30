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
using DotNetNuke.Services.Localization;
using System;
using System.Web.UI;

namespace GIBS.Modules.GIBS_OpenWeather
{
    public class GIBS_OpenWeatherModuleSettingsBase : ModuleSettingsBase
    {
        public string ApiKey
        {
            get
            {
                if (Settings.Contains("apiKey"))
                    return Settings["apiKey"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateTabModuleSetting(TabModuleId, "apiKey", value.ToString());
            }
        }

        public string City
        {
            get
            {
                if (Settings.Contains("city"))
                    return Settings["city"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateTabModuleSetting(TabModuleId, "city", value.ToString());
            }
        }

        public string State
        {
            get
            {
                if (Settings.Contains("state"))
                    return Settings["state"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateTabModuleSetting(TabModuleId, "state", value.ToString());
            }
        }

        public string CountryCode
        {
            get
            {
                if (Settings.Contains("countryCode"))
                    return Settings["countryCode"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateTabModuleSetting(TabModuleId, "countryCode", value.ToString());
            }
        }
        public string Latitude
        {
            get
            {
                if (Settings.Contains("latitude"))
                    return Settings["latitude"].ToString();
                return "";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateTabModuleSetting(TabModuleId, "latitude", value.ToString());
            }
        }

        public string Longitude
        {
            get
            {
                if (Settings.Contains("longitude"))
                    return Settings["longitude"].ToString();
                return "10:00 AM";
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateTabModuleSetting(TabModuleId, "longitude", value.ToString());
            }
        }

        public int MapZoom
        {
            get
            {
                if (Settings.Contains("MapZoom"))
                    return Convert.ToInt16(Settings["MapZoom"]);
                return 12;
            }
            set
            {
                var mc = new ModuleController();
                mc.UpdateTabModuleSetting(TabModuleId, "MapZoom", value.ToString());
            }
        }



    }
}