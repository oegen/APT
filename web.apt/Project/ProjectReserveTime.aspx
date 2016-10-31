<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ProjectReserveTime.aspx.vb" Inherits="Project_ProjectReserveTime" MasterPageFile="~/MasterPages/TabPageWithoutAjax.master" %>
<%@ Register Src="~/Controls/Reserve Time/ReserveTime.ascx" TagName="ReserveTime" TagPrefix="oegen" %>
<%@ MasterType VirtualPath="~/MasterPages/TabPageWithoutAjax.master" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <oegen:ReserveTime ID="ctrlReserveTime" runat="server"></oegen:ReserveTime>

</asp:Content>
