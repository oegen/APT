<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Login.aspx.vb" Inherits="Login"%>
<%@ Register Src="~/Controls/Generic/SimpleTextBox.ascx" TagPrefix="oegen" TagName="SimpleTextBox" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link rel="stylesheet" href="App_Themes/_default/main.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="login-container" class="group">
        <div id="login-apt-logo">
            <asp:Image ID="imgLogo" runat="server" CssClass="login-apt-logo" 
                AlternateText="" ImageUrl="~/App_Themes/_default/images/apt-logo.gif" />
        </div>
        <div id="login-content-wrap">
            <h1>Log in</h1>
            <h2>Please log in using your username and password</h2>
                
            <div class="login-form">
                
                <!--  LOGIN FORM -->                 <asp:Panel ID="pnlLogin" runat="server" DefaultButton="lnkLogin">                                    <ol>
                        <li><oegen:SimpleTextBox ID="txtUsername" runat="server" LabelText="Username" ValidationGroup="vldLogin" /></li>
                        <li><oegen:SimpleTextBox ID="txtPassword" runat="server" LabelText="Password" ValidationGroup="vldLogin" TextMode="Password" /></li>
                    </ol>
                    
                    <div class="colFull" style="margin-left:5px">
                        <div class="btn btn_orange">                            <asp:LinkButton ID="lnkLogin" runat="server" ValidationGroup="vldLogin">Login</asp:LinkButton><span></span>                        </div>                    </div>                </asp:Panel>                              <br /><br />                <asp:Label ID="lblError" runat="server" EnableViewState="false" ForeColor="Red"></asp:Label>
                <!-- END LOGIN FORM -->             </div>   
        </div>
    </div>
    </form>
</body>
</html>
