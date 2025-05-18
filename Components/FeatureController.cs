/*
' Copyright (c) 2025 GIBS.com
'  All rights reserved.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
' DEALINGS IN THE SOFTWARE.
' 
*/

//using System.Xml;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Search;
using System.Collections.Generic;

namespace GIBS.Modules.GIBS_OpenWeather.Components
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The Controller class for GIBS_OpenWeather
    /// 
    /// The FeatureController class is defined as the BusinessController in the manifest file (.dnn)
    /// DotNetNuke will poll this class to find out which Interfaces the class implements. 
    /// 
    /// The IPortable interface is used to import/export content from a DNN module
    /// 
    /// The ISearchable interface is used by DNN to index the content of a module
    /// 
    /// The IUpgradeable interface allows module developers to execute code during the upgrade 
    /// process for a module.
    /// 
    /// Below you will find stubbed out implementations of each, uncomment and populate with your own data
    /// </summary>
    /// -----------------------------------------------------------------------------

    //uncomment the interfaces to add the support.
    public class FeatureController //: IPortable, ISearchable, IUpgradeable
    {


        #region Optional Interfaces

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// ExportModule implements the IPortable ExportModule Interface
        /// </summary>
        /// <param name="ModuleID">The Id of the module to be exported</param>
        /// -----------------------------------------------------------------------------
        //public string ExportModule(int ModuleID)
        //{
        //string strXML = "";

        //List<GIBS_OpenWeatherInfo> colGIBS_OpenWeathers = GetGIBS_OpenWeathers(ModuleID);
        //if (colGIBS_OpenWeathers.Count != 0)
        //{
        //    strXML += "<GIBS_OpenWeathers>";

        //    foreach (GIBS_OpenWeatherInfo objGIBS_OpenWeather in colGIBS_OpenWeathers)
        //    {
        //        strXML += "<GIBS_OpenWeather>";
        //        strXML += "<content>" + DotNetNuke.Common.Utilities.XmlUtils.XMLEncode(objGIBS_OpenWeather.Content) + "</content>";
        //        strXML += "</GIBS_OpenWeather>";
        //    }
        //    strXML += "</GIBS_OpenWeathers>";
        //}

        //return strXML;

        //	throw new System.NotImplementedException("The method or operation is not implemented.");
        //}

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// ImportModule implements the IPortable ImportModule Interface
        /// </summary>
        /// <param name="ModuleID">The Id of the module to be imported</param>
        /// <param name="Content">The content to be imported</param>
        /// <param name="Version">The version of the module to be imported</param>
        /// <param name="UserId">The Id of the user performing the import</param>
        /// -----------------------------------------------------------------------------
        //public void ImportModule(int ModuleID, string Content, string Version, int UserID)
        //{
        //XmlNode xmlGIBS_OpenWeathers = DotNetNuke.Common.Globals.GetContent(Content, "GIBS_OpenWeathers");
        //foreach (XmlNode xmlGIBS_OpenWeather in xmlGIBS_OpenWeathers.SelectNodes("GIBS_OpenWeather"))
        //{
        //    GIBS_OpenWeatherInfo objGIBS_OpenWeather = new GIBS_OpenWeatherInfo();
        //    objGIBS_OpenWeather.ModuleId = ModuleID;
        //    objGIBS_OpenWeather.Content = xmlGIBS_OpenWeather.SelectSingleNode("content").InnerText;
        //    objGIBS_OpenWeather.CreatedByUser = UserID;
        //    AddGIBS_OpenWeather(objGIBS_OpenWeather);
        //}

        //	throw new System.NotImplementedException("The method or operation is not implemented.");
        //}

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// GetSearchItems implements the ISearchable Interface
        /// </summary>
        /// <param name="ModInfo">The ModuleInfo for the module to be Indexed</param>
        /// -----------------------------------------------------------------------------
        //public DotNetNuke.Services.Search.SearchItemInfoCollection GetSearchItems(DotNetNuke.Entities.Modules.ModuleInfo ModInfo)
        //{
        //SearchItemInfoCollection SearchItemCollection = new SearchItemInfoCollection();

        //List<GIBS_OpenWeatherInfo> colGIBS_OpenWeathers = GetGIBS_OpenWeathers(ModInfo.ModuleID);

        //foreach (GIBS_OpenWeatherInfo objGIBS_OpenWeather in colGIBS_OpenWeathers)
        //{
        //    SearchItemInfo SearchItem = new SearchItemInfo(ModInfo.ModuleTitle, objGIBS_OpenWeather.Content, objGIBS_OpenWeather.CreatedByUser, objGIBS_OpenWeather.CreatedDate, ModInfo.ModuleID, objGIBS_OpenWeather.ItemId.ToString(), objGIBS_OpenWeather.Content, "ItemId=" + objGIBS_OpenWeather.ItemId.ToString());
        //    SearchItemCollection.Add(SearchItem);
        //}

        //return SearchItemCollection;

        //	throw new System.NotImplementedException("The method or operation is not implemented.");
        //}

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// UpgradeModule implements the IUpgradeable Interface
        /// </summary>
        /// <param name="Version">The current version of the module</param>
        /// -----------------------------------------------------------------------------
        //public string UpgradeModule(string Version)
        //{
        //	throw new System.NotImplementedException("The method or operation is not implemented.");
        //}

        #endregion

    }

}
