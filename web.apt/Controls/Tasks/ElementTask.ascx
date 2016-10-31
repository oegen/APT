<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ElementTask.ascx.vb" Inherits="Controls_Tasks_ElementTask" %>
<%@ Register Src="~/Controls/Project/DocumentOverview.ascx" TagName="documentOverview" TagPrefix="oegen" %>

<section id="tasks" class="group">
                                   
    <div id="apt-list" class="tasks">
        <ul>
            <li>
                <span class="apt-list-yourtask-title"><asp:Label ID="lblTaskName" runat="server"></asp:Label></span>
                <span class="apt-list-description"><asp:Label ID="lblTaskDescription" runat="server">Has this task been completed?</asp:Label></span>

                <span class="apt-list-yourtask-title"><asp:Label ID="lblElementName" runat="server">Element</asp:Label></span>
                <span class="apt-list-description"><asp:Label ID="lblElementDescription" runat="server"></asp:Label></span>

                <span class="apt-list-yourtask-title">Comment</span>
                <span class="apt-list-description"><asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine"></asp:TextBox></span>

                <asp:PlaceHolder ID="phError" runat="server" Visible="false">
                    <asp:Label ID="lblErrorMessage" runat="server" ForeColor="Red"></asp:Label>
                </asp:PlaceHolder>
            </li>

            <li>
                <ul class="page-subnav task-subnav">
                    <li><asp:LinkButton ID="lnkElement" runat="server" CssClass="round"><ins><span class="system-buttons view-tasks">Go to elements</span></ins></asp:LinkButton></li>
                    <li><asp:LinkButton ID="lnkTimesheet" runat="server" CssClass="round"><ins><span class="system-buttons view-history">Go to timesheet</span></ins></asp:LinkButton></li>
                    <li><asp:LinkButton ID="lnkProject" runat="server" CssClass="round"><ins><span class="system-buttons view-tasks">Go to project</span></ins></asp:LinkButton></li>
                    <li><asp:LinkButton ID="lnkDocumentUpload" runat="server" CssClass="round"><ins><span class="system-buttons upload-document">Upload Document</span></ins></asp:LinkButton></li>
                    <li><asp:LinkButton ID="lnkComplete" runat="server" CssClass="round"><ins><span class="system-buttons complete">Task Complete</span></ins></asp:LinkButton></li>
                </ul>
            </li>
        </ul>
    </div>

    <oegen:documentOverview ID="ctrlUploadDocument" runat="server" Visible="false" />

    <asp:Label ID="lblUploadSuccess" runat="server" Visible="false">File uploaded successfully.</asp:Label>
                                        
</section> 













