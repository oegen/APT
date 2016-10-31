<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ProjectHeader.ascx.vb" Inherits="Controls_Layout_ProjectHeader" %>
<%@ Register Src="~/Controls/Layout/ContentHeader.ascx" TagPrefix="oegen" TagName="ContentHeader" %>
<%@ Register Src="~/Controls/Layout/MessagePanel.ascx" TagPrefix="oegen" TagName="MessagePanel" %>

<oegen:ContentHeader ID="ctrlContentHeader" runat="server" ShowOpenCloseMessage="true" />
<oegen:MessagePanel ID="ctrlMessagePanel" runat="server" />