<%@ Control Language="VB" AutoEventWireup="false" CodeFile="UserRoles.ascx.vb" Inherits="Controls_Admin_Users_UserRoles" %>

<fieldset> 
    <legend>User Roles</legend>
</fieldset>

<div class="user-roles-wrap">
    <div class="user-roles">
        <asp:ListBox ID="lbAvailableRoles" runat="server" CssClass="user-roles-listbox" SelectionMode="Multiple">
        </asp:ListBox>
    </div>
        

    <div class="user-roles">
        <asp:ListBox ID="lbCurrentRoles" runat="server" CssClass="user-roles-listbox" SelectionMode="Multiple">
        </asp:ListBox>
    </div>
    <div class="user-roles-btns"><asp:LinkButton ID="lnkAdd" CssClass="addKeywords" runat="server">Add <span>+</span></asp:LinkButton></div>
    <div class="user-roles-btns"><asp:LinkButton ID="lnkRemove" CssClass="addKeywords" runat="server">Remove <span>-</span></asp:LinkButton></div>
</div>

<br /><br /><br /><br /><br /><br /><br /><br /><br /><br />