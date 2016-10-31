<%@ Control Language="VB" AutoEventWireup="false" CodeFile="DocumentOverview.ascx.vb" Inherits="Controls_Project_DocumentOverview" %>
<%@ Register Src="~/Controls/Project/DocumentListing.ascx" TagName="DocumentListing" TagPrefix="oegen" %>
<%@ Register Src="~/Controls/Layout/ContentTitle.ascx" TagName="ContentTitle" TagPrefix="oegen" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

        <span class="tabs-titles"><oegen:ContentTitle ID="ctrlContentTitle" runat="server" Title="Document Overview" ToolTipText="Upload any documents related to the project here!"></oegen:ContentTitle></span>
                        
        <div class="aptform gen-form">

            <oegen:DocumentListing ID="ctlDocumentListing" runat="server" /><br />
    
            <h3>Filter by category:</h3>
            <asp:DropDownList ID="ddlProjectCategory" runat="server" AutoPostBack="true"></asp:DropDownList>
            
            <div class="upload-file">
                <h3>Select a file to upload:</h3>
                <asp:ScriptManager runat="server" ID="asd"></asp:ScriptManager>
                <ul class="page-subnav" style="border:none">
                    <li><telerik:RadUpload ID="fuDocument" runat="server" Skin="Vista" /></li>
                    <li><asp:Button ID="btnUploadDocument" CssClass="upload_btn" runat="server" Text="Upload File" /></li>
                    <li><asp:Label ID="lblError" ForeColor="Red" runat="server" EnableViewState="false"></asp:Label></li>
                </ul>
            </div>



        </div>



