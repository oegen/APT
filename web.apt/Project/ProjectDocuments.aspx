<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ProjectDocuments.aspx.vb" Inherits="Project_ProjectDocuments" Title="Project Documents" MasterPageFile="~/MasterPages/TabPageWithoutAjax.master" %>
<%@ Register Src="~/Controls/Project/DocumentOverview.ascx" TagPrefix="oegen" TagName="DocumentOverview" %>


<asp:Content ID="content" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="tabswrap">
        <div id="tabs-inner" class="group">  
            <oegen:DocumentOverview ID="ctrlDocumentOverview" runat="server" />
        </div>
    </div>    
</asp:Content>
