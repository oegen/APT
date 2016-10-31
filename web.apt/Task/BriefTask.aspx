<%@ Page Language="VB" AutoEventWireup="false" CodeFile="BriefTask.aspx.vb" Inherits="Task_BriefTask" Title="Project Brief Completion" MasterPageFile="~/MasterPages/TabPageWithoutAjax.master" %>
<%@ Register Src="~/Controls/Tasks/BriefTask.ascx" TagName="BriefTask" TagPrefix="oegen"  %>
<%@ MasterType VirtualPath="~/MasterPages/TabPageWithoutAjax.master" %>
<%@ Register Src="~/Controls/Layout/ContentTitle.ascx" TagName="ContentTitle" TagPrefix="oegen" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <div class="tabswrap">

        <div id="tabs-inner" class="group">  

            <span class="tabs-titles">
                <oegen:ContentTitle ID="ctrlContentTitle" runat="server" Title="Your Task" ToolTipText="Form Information to make it easier for the user. Contrary to popular belief, Lorem Ipsum is not simply random text. It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old. Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of the word in classical literature, discovered the undoubtable source."></oegen:ContentTitle>
            </span>

         <oegen:BriefTask ID="ctrlBriefTask" runat="server"></oegen:BriefTask>

        </div>

    </div>

</asp:Content>
