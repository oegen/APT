<%@ Control Language="VB" AutoEventWireup="false" CodeFile="BriefTask.ascx.vb" Inherits="Controls_Tasks_BriefTask" %>
<%@ Register Src="~/Controls/Project/DocumentOverview.ascx" TagName="documentOverview" TagPrefix="oegen" %>

<section id="tasks" class="group">
                                   
    <div id="apt-list" class="tasks">
        <ul>
            <li>
                <span class="apt-list-yourtask-title"><asp:Literal ID="ltlTaskName" runat="server"></asp:Literal></span>
                <span class="apt-list-description"><asp:Literal ID="ltlTaskDescription" runat="server"></asp:Literal></span>

                <asp:Panel ID="pnlElementTask" runat="server" Visible="false">

                    <span class="apt-list-yourtask-title"><asp:Literal ID="lblElementName" runat="server"></asp:Literal></span>

                    <span class="apt-list-description"><asp:Literal ID="lblElementDescription" runat="server"></asp:Literal></span>

                </asp:Panel>

                <span class="apt-list-yourtask-title">Comment</span>

                <span class="apt-list-description"><asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine"></asp:TextBox></span>

                <asp:PlaceHolder ID="phError" runat="server" Visible="false">
                    <asp:Label ID="lblErrorMessage" runat="server" ForeColor="Red"></asp:Label>
                </asp:PlaceHolder>
            </li>

            <li>
                <ul class="page-subnav task-subnav">
                    <li><asp:LinkButton ID="lnkViewProject" runat="server" CssClass="round"><ins><span class="system-buttons edit">Edit Project Information</span></ins></asp:LinkButton></li>
                    <li><asp:LinkButton ID="lnkViewElements" runat="server" CssClass="round"><ins><span class="system-buttons edit">Add / Edit Elements</span></ins></asp:LinkButton></li>
                    <li><asp:LinkButton ID="lnkUploadDocument" runat="server" CssClass="round"><ins><span class="system-buttons upload-document">Upload Document</span></ins></asp:LinkButton></li>                    <li><asp:LinkButton ID="lnkComplete" runat="server" CssClass="round"><ins><span class="system-buttons complete">Task Complete</span></ins></asp:LinkButton></li>
                </ul>
            </li>
        </ul>
    </div>

    <oegen:documentOverview ID="ctrlUploadDocument" runat="server" Visible="false" />

    <asp:Label ID="lblNotification" runat="server" Visible="false">File uploaded successfully.</asp:Label>
                                        
</section> 
