<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ProjectRoleGlobalChange.ascx.vb" Inherits="Controls_Admin_ProjectRoleGlobalChange_ProjectRoleGlobalChange" %>
<%@ Register Src="~/Controls/Generic/SimpleDropDownList.ascx" TagPrefix="oegen" TagName="SimpleDropdownList" %>
<%@ Register Src="~/Controls/Generic/UserDisplayAndSelect.ascx" TagName="UserBox" TagPrefix="oegen" %>

<fieldset> 
<ol> 
   <li>
    <oegen:SimpleDropdownList ID="ddlProjectRoles" runat="server" LabelText="Role" ValidationGroup="vldSave" />
   </li>
   <li>
    <label>Old User</label>
    <oegen:UserBox ID="ctrlOldUser" runat="server" LabelText="Old User" />
   </li>
   <li>
    <label>New User</label>
    <oegen:UserBox ID="ctrlNewUser" runat="server" LabelText="New User" />
   </li>  
</ol> 
</fieldset>

<div id="submit_btn" class="btn btn_orange">
    <asp:LinkButton ID="lnkSave" runat="server" ValidationGroup="vldSave">Save</asp:LinkButton><span></span>
</div> 

<asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>