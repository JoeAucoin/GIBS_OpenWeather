<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="View.ascx.cs" Inherits="GIBS.Modules.GIBS_OpenWeather.View"   %>


<div class="gibs-openweathermap">


   
    <div id="weatherOverview" runat="server">
        <p>Configure Module Settings...</p>
    </div>

    <hr class="thickhr">

    <h3>Today's Weather</h3>
    <div id="currentWeather" runat="server">
        <p>Configure Module Settings...</p>
    </div>

    <hr class="thickhr">

    <h3>8 Day Forecast</h3>
    <div id="dailyForecast" runat="server">
        <p>Configure Module Settings...</p>
    </div>
</div>


<div class="owcredits"><cite>Weather data provided by <a href="https://openweathermap.org/" target="_blank">OpenWeather</a></cite></div>