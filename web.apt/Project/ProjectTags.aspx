<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ProjectTags.aspx.vb" Inherits="Project_ProjectTags" Title="Project Tags" MasterPageFile="~/MasterPages/TabPageWithoutAjax.master" %>
<%@ Register Src="~/Controls/Project/ProjectTags.ascx" TagPrefix="oegen" TagName="ProjectTags" %>
<%@ MasterType VirtualPath="~/MasterPages/TabPageWithoutAjax.master" %>

<asp:Content ID="content" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <span class="tabs-titles"><oegen:ProjectTags ID="ctrlProjectTags" runat="server" /></span>
          
</asp:Content>

