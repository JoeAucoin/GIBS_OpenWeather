<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="View.ascx.cs" Inherits="GIBS.Modules.GIBS_OpenWeather.View"   %>



<style>
    /* Basic styling for the weather module */
    .gibs-openweathermap {
        font-family: Arial, sans-serif;
        color: #333;
        max-width: 800px;
        margin: 0 auto;
        padding: 15px;
        border: 1px solid #ddd;
        border-radius: 8px;
        background-color: #fff;
        box-shadow: 0 2px 4px rgba(0,0,0,0.1);
    }
    .gibs-openweathermap h3 {
        color: #0056b3;
        border-bottom: 1px solid #eee;
        padding-bottom: 5px;
        margin-top: 20px;
    }
    .gibs-openweathermap ul li {
        list-style: none;
        padding: 10px;
    }

     .gibs-openweathermap li:before {
        color: #e65100;
        content: "\27bd \0020";
        padding-right: 6px;
    }

    .gibs-openweathermap li {
        margin-bottom: 10px;
        padding: 10px;
        background-color: #fff;
        border: 1px solid #e0e0e0;
        border-radius: 5px;
        position: relative; /* For icon positioning */
    }
    .gibs-openweathermap li img {
        vertical-align: middle;
        margin-left: 10px;
    }
    .gibs-openweathermap .currentConditions {
        font-size: 1.2em;
        font-weight: bold;
        color: #3498db;
    }
    .gibs-openweathermap .thickhr {
        border: 0;
        height: 2px;
        background-color: orange;
        margin: 20px 0;
    }
    .gibs-openweathermap .alert-item {
        background-color: #ffe0b2; /* Light orange for alerts */
        border-color: #ff9800;
        margin-bottom: 10px;
        padding: 10px;
        border-radius: 5px;
    }
    .gibs-openweathermap .alert-item h4 {
        color: #e65100; /* Darker orange */
        margin-top: 0;
        margin-bottom: 5px;
    }
    .gibs-openweathermap .alert-item p {
        margin-bottom: 5px;
    }
    .gibs-openweathermap .chart-container {
        position: relative;
        height: 300px; /* Fixed height for chart */
        width: 100%;
        margin-top: 20px;
    }
     .gibs-openweathermap .weatherDate {
        font-size: 1.3em;
        font-weight: bold;
        color: #e65100; /* Darker orange */
 }
    
</style>

<div class="gibs-openweathermap">
    <h2 id="locationDisplay" runat="server">Loading Location...</h2>

   
    <div id="weatherOverview" runat="server">
        <p>Loading weather overview...</p>
    </div>

    <h3 id="weatherAlertsTitle" runat="server">Weather Alerts</h3>
    <div id="weatherAlertsContainer" runat="server">
        <p>Loading weather alerts...</p>
    </div>

    <h3>Hourly Temperature and Wind Forecast</h3>
    <div id="hourlyForecastChartContainer" runat="server" class="chart-container">
        <p>Loading hourly forecast chart...</p>
    </div>

    <div id="hourlyWindChartContainer" runat="server" class="chart-container">
    </div>


    <h3>Current Conditions</h3>
    <div id="currentWeather" runat="server">
        <p>Loading current weather...</p>
    </div>

   <hr class="thickhr">

       <h3>Daily Temperature and Wind Forecast</h3>
  <div id="dailyTempChartContainer" runat="server" class="chart-container">
      <p>Loading daily forecast chart...</p>
  </div>

  <div id="dailyWindChartContainer" runat="server" class="chart-container">
  </div>



    <h3>Daily Forecast</h3>
    <div id="dailyForecast" runat="server">
        <p>Loading daily forecast...</p>
    </div>


</div>


<div class="owcredits"><cite>Weather data provided by <a href="https://openweathermap.org/" target="_blank">OpenWeather</a></cite></div>