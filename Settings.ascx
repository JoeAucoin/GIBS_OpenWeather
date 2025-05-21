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
            <dnn:label ID="lblCountryCode" runat="server" />
            <asp:TextBox ID="txtCountryCode" Text="US" runat="server" />
        </div>

         <div class="dnnFormItem">
            <dnn:label ID="lblLookupLatLong" runat="server" suffix=":" />
             <asp:LinkButton ID="LinkButtonLookupLatLong" runat="server" OnClick="LinkButtonLookupLatLong_Click">Lookup Lat/Long</asp:LinkButton>
             &nbsp;&nbsp;<asp:Label ID="ShowMessage" runat="server" Text="" CssClass="messageError"></asp:Label><br />&nbsp;
        </div>

<div class="dnnFormItem" id="divLocationSelect" runat="server" visible="false">
        <dnn:label ID="lblLocationSelect" runat="server" Text="Select Location" />
        <asp:DropDownList ID="CorrectLocation" runat="server" AutoPostBack="True"
            OnSelectedIndexChanged="CorrectLocation_SelectedIndexChanged" Visible="true">
        </asp:DropDownList>
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