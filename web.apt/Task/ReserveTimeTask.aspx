<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ReserveTimeTask.aspx.vb" Inherits="Task_ReserveTimeTask" Title="Reserve Time" MasterPageFile="~/MasterPages/TabPageWithoutAjax.master" %>
<%@ Register Src="~/Controls/Reserve Time/ReserveTime.ascx" TagName="ReserveTime" TagPrefix="oegen" %>
<%@ MasterType VirtualPath="~/MasterPages/TabPageWithoutAjax.master" %>
<%@ Register Src="~/Controls/Layout/ContentTitle.ascx" TagName="ContentTitle" TagPrefix="oegen" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <div class="tabswrap">

        <div id="tabs-inner" class="group">  

            <span class="tabs-titles">
                <oegen:ContentTitle ID="ctrlContentTitle" runat="server" Title="Reserve Time" ToolTipText="Form Information to make it easier for the user. Contrary to popular belief, Lorem Ipsum is not simply random text. It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old. Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of the word in classical literature, discovered the undoubtable source."></oegen:ContentTitle>
            </span>

            <oegen:ReserveTime ID="ctrlReserveTime" runat="server" Editable="true" />

        </div>

    </div>

</asp:Content>