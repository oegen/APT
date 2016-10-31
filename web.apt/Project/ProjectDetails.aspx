<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ProjectDetails.aspx.vb" Inherits="Project_ProjectDetails" Title="Project Details" MasterPageFile="~/MasterPages/TabPageWithoutAjax.master" %>
<%@ Register Src="~/Controls/Project/ProjectDetails.ascx" TagName="ProjectDetails" TagPrefix="oegen" %>
<%@ MasterType VirtualPath="~/MasterPages/TabPageWithoutAjax.master" %>

<asp:Content ID="content" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    
    <oegen:ProjectDetails ID="ctrlProjectDetails" runat="server" />
    
</asp:Content>


