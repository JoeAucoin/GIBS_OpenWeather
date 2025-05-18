<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Settings.ascx.cs" Inherits="GIBS.Modules.GIBS_OpenWeather.Settings" %>


<%@ Register TagName="label" TagPrefix="dnn" Src="~/controls/labelcontrol.ascx" %>

	<h2 id="dnnSitePanel-BasicSettings" class="dnnFormSectionHead"><a href="#" class="dnnSectionExpanded"><%=LocalizeString("BasicSettings")%></a></h2>
	<fieldset>
        <div class="dnnFormItem">
            <dnn:Label ID="lblApiKey" runat="server" /> 
 
            <asp:TextBox ID="txtApiKey" runat="server" />
        </div>
        <div class="dnnFormItem">
            <dnn:label ID="lblCity" runat="server" />
            <asp:TextBox ID="txtCity" runat="server" />
        </div>
		
        <div class="dnnFormItem">
            <dnn:label ID="lblState" runat="server" />
            <asp:TextBox ID="txtState" runat="server" />
        </div>

        <div class="dnnFormItem">
            <dnn:label ID="lblLatitude" runat="server" />
            <asp:TextBox ID="txtLatitude" runat="server" />
        </div>
		<div class="dnnFormItem">
            <dnn:label ID="lblLongitude" runat="server" />
            <asp:TextBox ID="txtLongitude" runat="server" />
        </div>
		
		
    </fieldset>