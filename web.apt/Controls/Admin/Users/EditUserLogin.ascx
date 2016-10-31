<%@ Control Language="VB" AutoEventWireup="false" CodeFile="EditUserLogin.ascx.vb" Inherits="Controls_Admin_Users_EditUserLogin" %>
<%@ Register Src="~/Controls/Generic/SimpleTextBox.ascx" TagPrefix="oegen" TagName="SimpleTextBox" %>

<fieldset> 
<ol> 
   <li><label for="lblName">Name</label><asp:Label ID="lblName" runat="server" EnableViewState="false"></asp:Label></li>
   <li><oegen:SimpleTextBox ID="txtUsername" runat="server" LabelText="Username" ValidationGroup="vldSave" MaxLength="60" /></li>
   <li><oegen:SimpleTextBox ID="txtPassword" runat="server" LabelText="Password" ValidationGroup="vldSave" MaxLength="60" /></li>
   <li id="liDateCreated" runat="server" visible="false"><asp:Label ID="Label1" runat="server" AssociatedControlID="lblDateCreated">Date Created</asp:Label><asp:Label ID="lblDateCreated" runat="server"></asp:Label></li>
   <li id="liDateModified" runat="server" visible="false"><asp:Label ID="Label2" runat="server" AssociatedControlID="lblDateModified">Date Modified</asp:Label><asp:Label ID="lblDateModified" runat="server"></asp:Label></li>
   <li id="liDateLastLoggedIn" runat="server" visible="false"><asp:Label ID="Label3" runat="server" AssociatedControlID="lblDateLastLoggedIn">Date Last Logged In</asp:Label><asp:Label ID="lblDateLastLoggedIn" runat="server"></asp:Label></li>
</ol> 
</fieldset>

<div id="submit_btn" class="btn btn_orange">
    <asp:LinkButton ID="lnkSave" runat="server" ValidationGroup="vldSave">Save</asp:LinkButton><span></span>
</div> 

<asp:Label ID="lblError" runat="server" ForeColor="Red" Visible="false"></asp:Label>




