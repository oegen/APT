<%@ Control Language="VB" AutoEventWireup="false" CodeFile="AddEditUser.ascx.vb" Inherits="Controls_Admin_AddEditUser" %>
<%@ Register Src="~/Controls/Admin/Users/LDAPUserSearch.ascx" TagName="LDAPUserSearch" TagPrefix="oegen"  %>
<%@ Register Src="~/Controls/Generic/SimpleTextBox.ascx" TagPrefix="oegen" TagName="SimpleTextBox" %>
<%@ Register Src="~/Controls/Generic/NumericTextBox.ascx" TagPrefix="oegen" TagName="NumericTextBox"%>

<asp:LinkButton CssClass="adminLink" ID="lnkLDAPPopulate" runat="server">Populate via LDAP</asp:LinkButton>

<oegen:LDAPUserSearch ID="ctrlLdapUserSearch" runat="server" Visible="false" />

<asp:Panel ID="pnlUserEntry" runat="server">

    <fieldset> 
    <legend>Basic Details</legend>
        <ol> 
            <li>
                <oegen:SimpleTextBox ID="txtTitle" runat="server" LabelText="Title" ValidationGroup="vldUser" MaxLength="50" /> 
            </li>
            <li>
                <oegen:SimpleTextBox ID="txtForename" runat="server" LabelText="Forename" ValidationGroup="vldUser" MaxLength="20" /> 
            </li>
            <li>
                <oegen:SimpleTextBox ID="txtSurname" runat="server" LabelText="Surname" ValidationGroup="vldUser" MaxLength="20" /> 
            </li>
            <li>
                <asp:Label ID="lblUsername" runat="server" AssociatedControlID="txtUsername" EnableViewState="false">Username<em>*</em></asp:Label>
                <asp:TextBox ID="txtUsername" runat="server" MaxLength="60"></asp:TextBox>
                <asp:RequiredFieldValidator ID="vldUsername" runat="server" Display="Dynamic" ErrorMessage="*"
                    ControlToValidate="txtUsername" ValidationGroup="vldUser"></asp:RequiredFieldValidator>
                <asp:CustomValidator ID="cstmVldUsername" runat="server" Display="Dynamic" ErrorMessage="*"
                    ControlToValidate="txtUsername" ValidationGroup="vldUser"></asp:CustomValidator>
            </li>
            <li>
                <asp:Label ID="lblEmail" runat="server" AssociatedControlID="txtEmail" EnableViewState="false">E-mail<em>*</em></asp:Label>
                <asp:TextBox ID="txtEmail" runat="server" MaxLength="256"></asp:TextBox>
                <asp:RegularExpressionValidator ID="regexEmail" runat="server" Display="Dynamic" ErrorMessage="Valid email address required" 
                    ControlToValidate="txtEmail" ValidationGroup="vldUser"
                    ValidationExpression="^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$">
                </asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="vldEmail" runat="server" Display="Dynamic" ErrorMessage="*"
                    ControlToValidate="txtEmail" ValidationGroup="vldUser"></asp:RequiredFieldValidator>
                <asp:CustomValidator ID="cstmVldEmail" runat="server" Display="Dynamic" ErrorMessage="*"
                    ControlToValidate="txtEmail" ValidationGroup="vldUser"></asp:CustomValidator>
            </li>
        </ol> 
    </fieldset>

    <div id="submit_btn" class="btn btn_orange">
        <asp:LinkButton ID="lnkAddUser" runat="server" ValidationGroup="vldUser">Save</asp:LinkButton><span></span>
       
    </div> 

    <asp:Label ID="lblError" runat="server" ForeColor="Red" Visible="false" EnableViewState="false"></asp:Label>
     
</asp:Panel>

<br /><br />

