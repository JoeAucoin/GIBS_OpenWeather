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
             &nbsp;&nbsp;<asp:Label ID="ShowMessage" runat="server" Text="Click link to get Lat/Long for your City" CssClass="messageError"></asp:Label><br />&nbsp;
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
		
         <div class="dnnFormItem">
     <dnn:label ID="lblMapZoom" runat="server" ControlName="ddlMapZoom">
     </dnn:label>
     <asp:DropDownList ID="ddlMapZoom" runat="server">
         <asp:ListItem Text="1" Value="1"></asp:ListItem>
         <asp:ListItem Text="2" Value="2"></asp:ListItem>
         <asp:ListItem Text="3" Value="3"></asp:ListItem>
         <asp:ListItem Text="4" Value="4"></asp:ListItem>
         <asp:ListItem Text="5" Value="5"></asp:ListItem>
         <asp:ListItem Text="6" Value="6"></asp:ListItem>
         <asp:ListItem Text="7" Value="7"></asp:ListItem>
         <asp:ListItem Text="8" Value="8"></asp:ListItem>
         <asp:ListItem Text="9" Value="9"></asp:ListItem>
         <asp:ListItem Text="10" Value="10"></asp:ListItem>
         <asp:ListItem Text="11" Value="11"></asp:ListItem>
         <asp:ListItem Text="12" Value="12"></asp:ListItem>
         <asp:ListItem Text="13" Value="13"></asp:ListItem>
         <asp:ListItem Text="14" Value="14"></asp:ListItem>
         <asp:ListItem Text="15" Value="15"></asp:ListItem>
         <asp:ListItem Text="16" Value="16"></asp:ListItem>
         <asp:ListItem Text="17" Value="17"></asp:ListItem>
         <asp:ListItem Text="18" Value="18"></asp:ListItem>
     </asp:DropDownList>
 </div>
		
    </fieldset>